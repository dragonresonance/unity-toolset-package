#if ENABLE_LOCALIZER


using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks;
using DragonResonance.Logging;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.Networking;
using UnityEngine;


namespace DragonResonance.Localizer
{
	public partial class Localizer	// Online
	{
		#region Publics

			public static async UniTask RetrieveOnlineData()
			{
				//	Fetch online Resource assets
				#if UNITY_EDITOR	// Only during development
					static IUniTaskAsyncEnumerable<(TextAsset, string)> FetchResourceSources() =>
						FetchSources(_settings.ResourceSources,
							GetUrl: resourceSource => resourceSource.Url,
							GetSource: resourceSource => resourceSource.FileAsset);

					await foreach ((TextAsset, string) source in FetchResourceSources())
						await File.WriteAllTextAsync(
							UnityEditor.AssetDatabase.GetAssetPath(source.Item1),
							contents:source.Item2);

					UnityEditor.AssetDatabase.Refresh();
				#endif

				//	Fetch online Streaming assets
				static IUniTaskAsyncEnumerable<(string, string)> FetchStreamingSources() =>
					FetchSources(_settings.StreamingSources,
						GetUrl: streamingSource => streamingSource.Url,
						GetSource: streamingSource => streamingSource.FilePath);

				await foreach ((string, string) source in FetchStreamingSources())
					await File.WriteAllTextAsync(
						Path.Join(Application.streamingAssetsPath, source.Item1),
						contents:source.Item2);
			}

		#endregion


		#region Privates

			private static IUniTaskAsyncEnumerable<(T, string)> FetchSources<TSource, T>(
				IEnumerable<TSource> sources,
				Func<TSource, string> GetUrl,
				Func<TSource, T> GetSource)
			{
				if (sources == null)
					return UniTaskAsyncEnumerable.Empty<(T, string)>();

				// ReSharper disable once UnusedParameter.Local
				return UniTaskAsyncEnumerable.Create<(T, string)>(async (writer, token) =>
				{
					foreach (TSource source in sources) {
						Log.Info($"Retrieving source {source} ...");
						using UnityWebRequest request = UnityWebRequest.Get(GetUrl(source));

						try {
							await request.SendWebRequest();
							if (request.result == UnityWebRequest.Result.Success)
								await writer.YieldAsync((GetSource(source), request.downloadHandler.text));
							else
								Log.Error($"Error {request.result} fetching the resource \"{source}\"");
						}
						catch (Exception exception) {
							Log.Exception(exception, $"Exception fetching source {source}");
						}
					}
				});
			}

		#endregion
	}
}


#endif


/*                                                                                                                */
/*       `7MM"""Mq.`7MM"""Mq.       db     `7MM"""YMM  `7MN.   `7MF'     db     `7MM"""Mq. `7MMF' .M"""bgd        */
/*         MM   `MM. MM   `MM.     ;MM:      MM    `7    MMN.    M      ;MM:      MM   `MM.  MM  ,MI    "Y        */
/*         MM   ,M9  MM   ,M9     ,V^MM.     MM   d      M YMb   M     ,V^MM.     MM   ,M9   MM  `MMb.            */
/*         MMmmdM9   MMmmdM9     ,M  `MM     MMmmMM      M  `MN. M    ,M  `MM     MMmmdM9    MM    `YMMNq.        */
/*         MM        MM  YM.     AbmmmqMA    MM   Y  ,   M   `MM.M    AbmmmqMA    MM  YM.    MM  .     `MM        */
/*         MM        MM   `Mb.  A'     VML   MM     ,M   M     YMM   A'     VML   MM   `Mb.  MM  Mb     dM        */
/*       .JMML.    .JMML. .JMM.AMA.   .AMMA.JMMmmmmMMM .JML.    YM .AMA.   .AMMA.JMML. .JMM.JMML.P"Ybmmd"         */
/*                                                                                                                */
/*                 Licensed under the Apache License, Version 2.0.  See LICENSE.md for more info.                 */
/*                                     Copyright © 2026. All rights reserved.                                     */
/*                                                                                                                */
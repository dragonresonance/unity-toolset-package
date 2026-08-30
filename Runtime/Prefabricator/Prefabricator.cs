#if ENABLE_PREFABRICATOR


using Cysharp.Threading.Tasks;
using DragonResonance.Logging;
using UnityEngine.Scripting;
using UnityEngine;


namespace DragonResonance.Prefabricator
{
	[Preserve]
	public class Prefabricator
	{
		private static PrefabricatorSettings _settings = null;
		private static readonly UniTaskCompletionSource _starting = new();
		private static readonly UniTaskCompletionSource _spawning = new();


		#region Events

			[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
			private static void Initialize() => OnStartup();

			private static async void OnStartup()
			{
				Log.Info("Starting up...");
				_settings = await PrefabricatorSettings.GetInstanceAsync();
				_starting.TrySetResult();
				Log.Info("Started!");

				await Spawn();
			}

		#endregion


		#region Publics

			public static async UniTask Spawn()
			{
				Log.Info("Spawning...");
				await _starting.Task;

				foreach (SPrefabricable item in _settings.Items) {
					if (item.Prefab == null) {
						Log.Warning("An empty prefab entry was skipped!");
						continue;
					}

					for (int itemIndex = 0; itemIndex < item.Amount; itemIndex++) {
						GameObject instance = Object.Instantiate(item.Prefab);
						instance.name = item.Prefab.name;

						if (item.Persistent)
							Object.DontDestroyOnLoad(instance);
					}
				}

				_spawning.TrySetResult();
				Log.Info("Spawned!");
			}

		#endregion


		#region Properties

			public static UniTaskCompletionSource Starting => _starting;
			public static UniTaskCompletionSource Spawning => _spawning;

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
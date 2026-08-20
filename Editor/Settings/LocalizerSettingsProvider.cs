#if UNITY_EDITOR


using DragonResonance.Databases;
using DragonResonance.Editor.Building;
using DragonResonance.Extensions;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if ENABLE_LOCALIZER
using DragonResonance.Localizer;
#endif


namespace DragonResonance.Editor.Settings
{
#if ENABLE_LOCALIZER
	public class LocalizerSettingsProvider : AScriptableSettingsProvider<LocalizerSettings>
#else
	public class LocalizerSettingsProvider : AScriptableSettingsProvider
#endif
	{
		private const string SettingsPath = "Project/Praenaris/Localizer";
		private const string BuildDefinition = "ENABLE_LOCALIZER";


		private Vector2 _dataViewScroll = Vector2.zero;
		private float _dataCellWidth = 240f;


		#region Constructors

			[SettingsProvider]
			public static SettingsProvider Create() => new LocalizerSettingsProvider(SettingsPath, SettingsScope.Project);

			public LocalizerSettingsProvider(string path, SettingsScope scope) : base(path, scope) { }

		#endregion


		#region Inheritables

			protected override void OnBeforeGUI(string searchContext)
			{
				#if ENABLE_LOCALIZER
					if (!EditorGUILayout.Toggle("Enabled", true))
						BuildDefines.SetDefinitionState(BuildDefinition, false);
				#else
					if (EditorGUILayout.Toggle("Enabled", false))
						BuildDefines.SetDefinitionState(BuildDefinition, true);
				#endif
			}

			protected override void OnAfterGUI(string searchContext)
			{
				#if ENABLE_LOCALIZER
					HeaderedSheet<string> sheet = Localizer.Localizer.DataSheet;
					GUIStyle dataCellStyle = new(UnityEngine.GUI.skin.textArea) { wordWrap = true };

					EditorGUILayout.LabelField("Data", EditorStyles.boldLabel);
					if ((sheet?.Data == null) || (sheet.Data.IsEmpty()))
						EditorGUILayout.LabelField("No data, only available on runtime", EditorStyles.centeredGreyMiniLabel);
					else {
						_dataCellWidth = EditorGUILayout.Slider("Cell size", _dataCellWidth, 4f, 400f);
						_dataViewScroll = EditorGUILayout.BeginScrollView(_dataViewScroll, true, true);
						{
							foreach (List<string> row in sheet.Data) {
								EditorGUILayout.BeginHorizontal();
								{
									foreach (string column in row) {
										EditorGUILayout.TextArea(column, dataCellStyle, GUILayout.Width(_dataCellWidth), GUILayout.ExpandHeight(true));
									}
								}
								EditorGUILayout.EndHorizontal();
							}
						}
						EditorGUILayout.EndScrollView();
					}
				#endif
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
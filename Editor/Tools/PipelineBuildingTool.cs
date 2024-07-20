#if UNITY_EDITOR


using DragonResonance.Logging;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;




namespace DragonResonance.Editor.Tools
{
	[CreateAssetMenu(menuName = "Dragon Resonance/Pipeline Building Asset", fileName = "New Pipeline Building Asset")]
	public class PipelineBuildingTool : ScriptableObject
	{
		[Header("Configuration")]
		[SerializeField] private SBuildConfiguration[] _buildConfigurations = { };




		#region Publics


			[ContextMenu(nameof(BuildPipeline))]
			public void BuildPipeline()
			{
				HLogger.LogEmphasis($"Building pipeline ...", typeof(PipelineBuildingTool));
				{
					foreach (SBuildConfiguration configuration in _buildConfigurations)
						if (configuration.included)
							Build(configuration);
				}
				HLogger.LogEmphasis("Pipeline finished!", typeof(PipelineBuildingTool));
			}


			public void Build(int index)
			{
				Build(_buildConfigurations[index]);
			}

			public void Build(SBuildConfiguration configuration)
			{
				HLogger.Log($"Building {configuration.alias} ...", typeof(PipelineBuildingTool));
				{
					UnityEditor.BuildPipeline.BuildPlayer(new BuildPlayerOptions() {
						scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(scene => scene.path).ToArray(),
						target = configuration.target,
						subtarget = (int)configuration.subtarget,
						options = configuration.options,
						locationPathName = configuration.locationPathName,
						extraScriptingDefines = configuration.extraScriptingDefines,
					});
				}
				HLogger.Log("Building finished!", typeof(PipelineBuildingTool));
			}


		#endregion




		#region Properties


			public IEnumerable<SBuildConfiguration> BuildConfigurations => _buildConfigurations;


		#endregion
	}




	[CustomEditor(typeof(PipelineBuildingTool))]
	public class PipelineBuildingToolEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			PipelineBuildingTool pipelineBuildingTool = (PipelineBuildingTool)base.target;


			EditorGUILayout.LabelField("Pipeline Summary", EditorStyles.boldLabel);
			int order = 0;
			foreach (SBuildConfiguration configuration in pipelineBuildingTool.BuildConfigurations)
				if (configuration.included)
					EditorGUILayout.LabelField($"{++order}. {configuration.alias}");

			if (GUILayout.Button("Execute Building Pipeline"))
				pipelineBuildingTool.BuildPipeline();
		}
	}
}


#endif




/*       ________________________________________________________________       */
/*           _________   _______ ________  _______  _______  ___    _           */
/*           |        \ |______/ |______| |  _____ |       | |  \   |           */
/*           |________/ |     \_ |      | |______| |_______| |   \__|           */
/*           ______ _____ _____ _____ __   _ _____ __   _ _____ _____           */
/*           |____/ |____ [___  |   | | \  | |___| | \  | |     |____           */
/*           |    \ |____ ____] |___| |  \_| |   | |  \_| |____ |____           */
/*       ________________________________________________________________       */
/*                                                                              */
/*           David Tabernero M.  <https://github.com/davidtabernerom>           */
/*           Dragon Resonance    <https://github.com/dragonresonance>           */
/*                  Copyright © 2021-2024. All rights reserved.                 */
/*                Licensed under the Apache License, Version 2.0.               */
/*                         See LICENSE.md for more info.                        */
/*       ________________________________________________________________       */
/*                                                                              */
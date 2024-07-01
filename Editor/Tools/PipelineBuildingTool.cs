#if UNITY_EDITOR


using PossumScream.Enhancements;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;




namespace PossumScream.Editor.Tools
{
	[CreateAssetMenu(menuName = "Dragon Resonance/Pipeline Building Asset", fileName = "New Pipeline Building Asset")]
	public class PipelineBuildingTool : ScriptableObject
	{
		[Header("Configuration")]
		[SerializeField] private SBuildConfiguration[] _buildConfigurations = { };




		#region Controls


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




/*                                                                                            */
/*          ______                               _______                                      */
/*          \  __ \____  ____________  ______ ___\  ___/_____________  ____  ____ ___         */
/*          / /_/ / __ \/ ___/ ___/ / / / __ \__ \\__ \/ ___/ ___/ _ \/ __ \/ __ \__ \        */
/*         / ____/ /_/ /__  /__  / /_/ / / / / / /__/ / /__/ /  / ___/ /_/ / / / / / /        */
/*        /_/    \____/____/____/\____/_/ /_/ /_/____/\___/_/   \___/\__/_/_/ /_/ /__\        */
/*                                                                                            */
/*        Licensed under the Apache License, Version 2.0. See LICENSE.md for more info        */
/*        David Tabernero M. @ PossumScream                      Copyright © 2021-2024        */
/*        GitLab - GitHub: possumscream                            All rights reserved        */
/*        - - - - - - - - - - - - -                                  - - - - - - - - -        */
/*                                                                                            */
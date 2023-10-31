#if UNITY_EDITOR


using PossumScream.Enhancements;
using System.Linq;
using System;
using UnityEditor;
using UnityEngine;




namespace PossumScream.Editor.Tools
{
	[CreateAssetMenu(menuName = "Building", fileName = "New PlayerBuild Asset")]
	public class PlayerBuildTool : ScriptableObject
	{
		[Header("Configuration")]
		[SerializeField] private BuildPlayerOptionsSet[] _buildPlayerOptionsSets = {};




		#region Controls


			public void BuildPlayer(int optionsSetIndex)
			{
				BuildPlayerOptionsSet optionsSet = this._buildPlayerOptionsSets[optionsSetIndex];


				HLogger.LogInfo($"Building the {optionsSet.target} player...", typeof(PlayerBuildTool));
				{
					BuildPipeline.BuildPlayer(new BuildPlayerOptions() {
						scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(scene => scene.path).ToArray(),
						target = optionsSet.target,
						subtarget = (int)optionsSet.subtarget,
						options = optionsSet.options,
						extraScriptingDefines = optionsSet.extraScriptingDefines,
						locationPathName = optionsSet.locationPathName,
					});
				}
				HLogger.LogInfo("Done!", typeof(PlayerBuildTool));
			}


		#endregion




		#region Properties


			public BuildPlayerOptionsSet[] buildPlayerOptionsSets => this._buildPlayerOptionsSets;


		#endregion
	}




	[Serializable]
	public struct BuildPlayerOptionsSet
	{
		public string alias;
		public BuildTarget target;
		public StandaloneBuildSubtarget subtarget;
		public BuildOptions options;
		public string locationPathName;
		public string[] extraScriptingDefines;
	}




	[CustomEditor(typeof(PlayerBuildTool))]
	public class PlayerBuildToolEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			PlayerBuildTool playerBuildTool = base.target as PlayerBuildTool;


			base.OnInspectorGUI();

			EditorGUILayout.LabelField("Build", EditorStyles.boldLabel);
			for (int optionsSetIndex = 0; optionsSetIndex < playerBuildTool!.buildPlayerOptionsSets.Length; optionsSetIndex++) {
				BuildPlayerOptionsSet optionsSet = playerBuildTool.buildPlayerOptionsSets[optionsSetIndex];
				if (GUILayout.Button(optionsSet.alias))
					playerBuildTool.BuildPlayer(optionsSetIndex);
			}
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
/*        David Tabernero M. @ PossumScream                      Copyright © 2021-2023        */
/*        GitLab - GitHub: possumscream                            All rights reserved        */
/*        -------------------------                                  -----------------        */
/*                                                                                            */
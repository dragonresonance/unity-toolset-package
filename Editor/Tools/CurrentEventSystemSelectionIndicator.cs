#if UNITY_EDITOR


using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine;




namespace PossumScream.Editor.Tools
{
	public class CurrentEventSystemSelectionIndicator : EditorWindow
	{
		#region Events


			private void OnInspectorUpdate()
			{
				Repaint();
			}


			private void OnGUI()
			{
				if (Application.isPlaying) {
					if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null) {
						EditorGUILayout.ObjectField("Current selection", EventSystem.current.currentSelectedGameObject, typeof(GameObject), true);
					}
					else {
						EditorGUILayout.LabelField("No GameObject selected.");
					}
				}
				else {
					EditorGUILayout.LabelField("The application must be playing to display the selected GameObject.");
				}
			}


		#endregion




		#region Controls


			[MenuItem("Window/PossumScream/Current EventSystem Selection Indicator")]
			public static void createWindow()
			{
				GetWindow<CurrentEventSystemSelectionIndicator>("Current EventSystem Selection Indicator");
			}


		#endregion
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
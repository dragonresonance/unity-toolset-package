#if UNITY_EDITOR


using PossumScream.Enhancements;
using UnityEditor;
using UnityEngine;
using UnityObject = UnityEngine.Object;




namespace PossumScream.Editor.Optimization
{
	public static class ScaleChecker
	{
		private static int allZeroScaleCount = 0;
		private static int troublesomeScaleCount = 0;




		#region Controls


			[MenuItem("Tools/PossumScream/Optimization/Check scene for troublesome scales")]
			public static void CheckSceneForTroublesomeScales()
			{
				HLogger.LogInfo("Checking scene for troublesome scales...", typeof(ScaleChecker));
				{
					Transform[] sceneTransforms = UnityObject.FindObjectsOfType<Transform>(true);


					foreach (Transform transform in sceneTransforms) {
						Vector3 transformLocalScale = transform.localScale;


						if (checkForAllZeroScale(transformLocalScale)) {
							HLogger.LogInfo($"Has an <color={HLogger.InfoColor}><b>all-zero</b></color> scale", transform);
							allZeroScaleCount++;
							continue;
						}

						if (!checkForAllOneScale(transformLocalScale)) {
							HLogger.LogWarning($"Has a <color={HLogger.WarningColor}><b>troublesome</b></color> scale <b>→ [ x:<color={HLogger.WarningColor}>{transformLocalScale.x}</color>, y:<color={HLogger.WarningColor}>{transformLocalScale.y}</color>, z:<color={HLogger.WarningColor}>{transformLocalScale.z}</color> ]</b>", transform);
							troublesomeScaleCount++;
							continue;
						}
					}

					HLogger.LogEmphasis($"Analyzed {sceneTransforms.Length} scene scales", typeof(ScaleChecker));
					HLogger.LogEmphasis($"Detected {allZeroScaleCount} all-zero scales", typeof(ScaleChecker));
					HLogger.LogEmphasis($"Detected {troublesomeScaleCount} troublesome scales", typeof(ScaleChecker));
				}
				HLogger.LogInfo("Done!", typeof(ScaleChecker));
			}


		#endregion




		#region Actions


			private static bool checkForAllZeroScale(Vector3 scale)
			{
				return (Mathf.Approximately(scale.x, 0f) &&
				        Mathf.Approximately(scale.y, 0f) &&
				        Mathf.Approximately(scale.z, 0f));
			}


			private static bool checkForAllOneScale(Vector3 scale)
			{
				return (Mathf.Approximately(scale.x, 1f) &&
				        Mathf.Approximately(scale.y, 1f) &&
				        Mathf.Approximately(scale.z, 1f));
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
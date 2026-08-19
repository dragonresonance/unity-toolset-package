#if UNITY_EDITOR


using UnityEditor;
using UnityEngine;


namespace DragonResonance.Editor.Settings
{
	public class PraenarisEditorSettingsProvider : SettingsProvider
	{
		private const string SettingsPath = "Project/Praenaris";
		private const string BannerGUID = "9a60bf40f7c4aa74d94ea7c32361af13";
		private const int LargePadding = 60;
		private const int MediumPadding = 36;
		private const int SmallPadding = 12;


		private PraenarisEditorSettings _settings = null;
		private readonly Texture2D _bannerImage = null;


		#region Constructors

			[SettingsProvider]
			public static SettingsProvider Create() => new PraenarisEditorSettingsProvider(SettingsPath, SettingsScope.Project);

			public PraenarisEditorSettingsProvider(string path, SettingsScope scope) : base(path, scope)
			{
				string bannerPath = AssetDatabase.GUIDToAssetPath(BannerGUID);
				_bannerImage = AssetDatabase.LoadAssetAtPath<Texture2D>(bannerPath);
			}

		#endregion


		#region Publics

			public override void OnGUI(string searchContext)
			{
				_settings = PraenarisEditorSettings.instance;

				Rect bannerRect = GUILayoutUtility.GetAspectRect((float)_bannerImage.width / _bannerImage.height);
				GUIStyle fullSection = new() { padding = new RectOffset(MediumPadding, MediumPadding, MediumPadding, MediumPadding) };
				GUIStyle separatedSection = new() { padding = new RectOffset(MediumPadding, MediumPadding, SmallPadding, SmallPadding) };
				GUIStyle copyrightSection = new() { padding = new RectOffset(LargePadding, LargePadding, SmallPadding, SmallPadding) };

				EditorGUI.DrawTextureTransparent(bannerRect, _bannerImage);

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.BeginVertical(fullSection);
				{
					{
						EditorGUILayout.LabelField("Links", EditorStyles.whiteLargeLabel);
						EditorGUILayout.BeginHorizontal(separatedSection);
						{
							if (GUILayout.Button("Website"))
								Application.OpenURL("https://www.praenaris.com/");
							if (GUILayout.Button("GitHub"))
								Application.OpenURL("https://github.com/Praenaris");
							if (GUILayout.Button("GitLab"))
								Application.OpenURL("https://gitlab.com/Praenaris");
						}
						EditorGUILayout.EndHorizontal();
					}
					GUILayout.Space(MediumPadding);
					{
						EditorGUILayout.LabelField("Bug reports & Feature requests", EditorStyles.whiteLargeLabel);
						EditorGUILayout.BeginHorizontal(separatedSection);
						{
							if (GUILayout.Button("Submit DevKit issue"))
								Application.OpenURL("https://github.com/Praenaris/Unity-DevKit/issues/new/choose");
							if (GUILayout.Button("Submit ToolSet issue"))
								Application.OpenURL("https://github.com/Praenaris/Unity-ToolSet/issues/new/choose");
						}
						EditorGUILayout.EndHorizontal();
					}
					GUILayout.Space(MediumPadding);
					{
						EditorGUILayout.LabelField("Developers", EditorStyles.whiteLargeLabel);
						{
							EditorGUILayout.BeginVertical(separatedSection);
							{
								EditorGUILayout.LabelField("David Tabernero M.", EditorStyles.largeLabel);
								EditorGUILayout.BeginHorizontal();
								{
									if (GUILayout.Button("Website"))
										Application.OpenURL("https://tabernero.dev/");
									if (GUILayout.Button("X"))
										Application.OpenURL("https://x.com/davidtabernerom");
									if (GUILayout.Button("GitHub"))
										Application.OpenURL("https://github.com/davidtabernerom");
									if (GUILayout.Button("LinkedIn"))
										Application.OpenURL("https://www.linkedin.com/in/davidtabernerom/");
								}
								EditorGUILayout.EndHorizontal();
								if (EditorGUILayout.LinkButton("Buy David a Ko-Fi!"))
									Application.OpenURL("https://ko-fi.com/davidtabernerom");
							}
							EditorGUILayout.EndVertical();
						}
					}
					GUILayout.Space(MediumPadding);
					{
						EditorGUILayout.BeginVertical(copyrightSection);
						{
							GUICenteredLabel("Copyright © 2026. All rights reserved.");
							GUICenteredLabel("Licensed under the Apache License, Version 2.0.");
							GUICenteredLink("See LICENSE.md for more info.", "https://github.com/Praenaris/Unity-ToolSet/blob/master/LICENSE.md");
						}
						EditorGUILayout.EndVertical();
					}
					//_settings.EditorTestBool = EditorGUILayout.Toggle("Editor Test Bool", _settings.EditorTestBool);
					//_settings.EditorTestString = EditorGUILayout.TextField("Editor Test String", _settings.EditorTestString);
				}
				EditorGUILayout.EndVertical();
				if (EditorGUI.EndChangeCheck())
					_settings.Save();
			}

		#endregion


		#region Privates

			private void GUICenteredLabel(string text)
			{
				EditorGUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				EditorGUILayout.LabelField(text, GUILayout.Width(EditorStyles.label.CalcSize(new GUIContent(text)).x));
				GUILayout.FlexibleSpace();
				EditorGUILayout.EndHorizontal();
			}

			private void GUICenteredLink(string text, string url)
			{
				EditorGUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				if (EditorGUILayout.LinkButton(text))
					Application.OpenURL(url);
				GUILayout.FlexibleSpace();
				EditorGUILayout.EndHorizontal();
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
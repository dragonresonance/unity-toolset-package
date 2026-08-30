#if UNITY_EDITOR && ENABLE_PREFABRICATOR


using DragonResonance.Prefabricator;
using UnityEditor;
using UnityEngine;


namespace DragonResonance.Editor.Drawers
{
	[CustomPropertyDrawer(typeof(SPrefabricable))]
	public class SPrefabricableDrawer : PropertyDrawer
	{
		private const float SPACING = 8f;
		private const float AMOUNT_WIDTH = 40f;
		private const float PERSISTENT_WIDTH = 16f;

		private static readonly GUIContent AmountLabel = new("Amount ");
		private static readonly GUIContent PersistentLabel = new("Persist ");


		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			{
				SerializedProperty prefabProperty = property.FindPropertyRelative(nameof(SPrefabricable.Prefab));
				SerializedProperty amountProperty = property.FindPropertyRelative(nameof(SPrefabricable.Amount));
				SerializedProperty persistentProperty = property.FindPropertyRelative(nameof(SPrefabricable.Persistent));

				Object prefab = prefabProperty.objectReferenceValue;
				GUIContent prefixLabel = (prefab == null) ? label : new GUIContent(prefab.name, label.tooltip);

				position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), prefixLabel);

				float lineHeight = EditorGUIUtility.singleLineHeight;
				float amountLabelWidth = EditorStyles.label.CalcSize(AmountLabel).x;
				float persistentLabelWidth = EditorStyles.label.CalcSize(PersistentLabel).x;

				float persistentPositionX = position.xMax - PERSISTENT_WIDTH;
				float persistentLabelPositionX = persistentPositionX - persistentLabelWidth;
				float amountPositionX = persistentLabelPositionX - SPACING - AMOUNT_WIDTH;
				float amountLabelPositionX = amountPositionX - amountLabelWidth;

				Rect prefabRect = new(position.x, position.y, amountLabelPositionX - SPACING - position.x, lineHeight);
				Rect amountLabelRect = new(amountLabelPositionX, position.y, amountLabelWidth, lineHeight);
				Rect amountRect = new(amountPositionX, position.y, AMOUNT_WIDTH, lineHeight);
				Rect persistentLabelRect = new(persistentLabelPositionX, position.y, persistentLabelWidth, lineHeight);
				Rect persistentRect = new(persistentPositionX, position.y, PERSISTENT_WIDTH, lineHeight);

				int indentLevel = EditorGUI.indentLevel;
				EditorGUI.indentLevel = 0;
				{
					EditorGUI.PropertyField(prefabRect, prefabProperty, GUIContent.none);
					EditorGUI.LabelField(amountLabelRect, AmountLabel);
					EditorGUI.PropertyField(amountRect, amountProperty, GUIContent.none);
					EditorGUI.LabelField(persistentLabelRect, PersistentLabel);
					EditorGUI.PropertyField(persistentRect, persistentProperty, GUIContent.none);
				}
				EditorGUI.indentLevel = indentLevel;
			}
			EditorGUI.EndProperty();
		}


		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorGUIUtility.singleLineHeight;
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
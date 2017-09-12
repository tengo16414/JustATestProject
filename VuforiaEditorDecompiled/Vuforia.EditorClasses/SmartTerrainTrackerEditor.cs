using System;
using UnityEditor;
using UnityEngine;

namespace Vuforia.EditorClasses
{
	internal class SmartTerrainTrackerEditor : ConfigurationEditor
	{
		private SerializedProperty mAutoInitTracker;

		private SerializedProperty mAutoStartTracker;

		private SerializedProperty mAutoInitBuilder;

		private SerializedProperty mSceneUnitsToMillimeter;

		public override string Title
		{
			get
			{
				return "Smart Terrain Tracker";
			}
		}

		public override void FindSerializedProperties(SerializedObject serializedObject)
		{
			this.mAutoInitTracker = serializedObject.FindProperty("smartTerrainTracker.autoInitTracker");
			this.mAutoStartTracker = serializedObject.FindProperty("smartTerrainTracker.autoStartTracker");
			this.mAutoInitBuilder = serializedObject.FindProperty("smartTerrainTracker.autoInitBuilder");
			this.mSceneUnitsToMillimeter = serializedObject.FindProperty("smartTerrainTracker.sceneUnitsToMillimeter");
		}

		public override void DrawInspectorGUI()
		{
			bool expr_0A = SceneManager.Instance.ExtendedTrackingEnabledOnATarget();
			if (expr_0A)
			{
				EditorGUILayout.HelpBox("Smart Terrain cannot be enabled at the same time as Extended Tracking.", 1);
			}
			GUI.enabled = !expr_0A;
			EditorGUILayout.PropertyField(this.mAutoInitTracker, new GUIContent("Start Automatically"), new GUILayoutOption[0]);
			this.mAutoStartTracker.set_boolValue(this.mAutoInitTracker.get_boolValue());
			this.mAutoInitBuilder.set_boolValue(this.mAutoInitTracker.get_boolValue());
			if (this.mAutoInitTracker.get_boolValue())
			{
				EditorGUILayout.HelpBox("Enter a scale factor that defines how a scene unit needs to be scaled to be in real world millimeters.\nE.g. if 1 scene unit should be 100mm in the real word, set this scale value to 100.0", 0);
				EditorGUILayout.PropertyField(this.mSceneUnitsToMillimeter, new GUIContent("Scene unit in mm"), new GUILayoutOption[0]);
			}
			GUI.enabled = true;
		}
	}
}

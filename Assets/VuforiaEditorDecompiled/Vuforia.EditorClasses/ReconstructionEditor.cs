using System;
using UnityEditor;
using UnityEngine;

namespace Vuforia.EditorClasses
{
	[CustomEditor(typeof(ReconstructionAbstractBehaviour), true)]
	public class ReconstructionEditor : Editor
	{
		private SerializedProperty mMaximumExtentEnabled;

		private SerializedProperty mMaximumExtent;

		private SerializedProperty mAutomaticStart;

		private SerializedProperty mNavMeshUpdates;

		private SerializedProperty mNavMeshPadding;

		public static void EditorConfigureTarget(ReconstructionAbstractBehaviour rb, SerializedObject serializedObject = null)
		{
			if (rb == null)
			{
				Debug.LogError("ReconstructionAbstractBehaviour parameter is null !");
				return;
			}
			if (VuforiaUtilities.GetPrefabType(rb) == 1)
			{
				return;
			}
			if (!SceneManager.Instance.SceneInitialized)
			{
				SceneManager.Instance.InitScene();
			}
			if (serializedObject == null)
			{
				serializedObject = new SerializedObject(rb);
			}
			if (!EditorApplication.get_isPlaying())
			{
				serializedObject.Update();
				SerializedProperty serializedProperty = serializedObject.FindProperty("mInitializedInEditor");
				if (!serializedProperty.get_boolValue())
				{
					Debug.Log("Reconstruction added to scene, enabling SmartTerrainTracker");
					VuforiaAbstractConfiguration expr_6F = VuforiaAbstractConfigurationEditor.LoadConfigurationObject();
					expr_6F.SmartTerrainTracker.AutoInitAndStartTracker = true;
					expr_6F.SmartTerrainTracker.AutoInitBuilder = true;
					using (serializedObject.Edit())
					{
						serializedObject.FindProperty("mMaximumExtentEnabled").set_boolValue(false);
						serializedProperty.set_boolValue(true);
					}
				}
			}
		}

		public void OnEnable()
		{
			this.mMaximumExtentEnabled = base.get_serializedObject().FindProperty("mMaximumExtentEnabled");
			this.mMaximumExtent = base.get_serializedObject().FindProperty("mMaximumExtent");
			this.mAutomaticStart = base.get_serializedObject().FindProperty("mAutomaticStart");
			this.mNavMeshUpdates = base.get_serializedObject().FindProperty("mNavMeshUpdates");
			this.mNavMeshPadding = base.get_serializedObject().FindProperty("mNavMeshPadding");
			ReconstructionEditor.EditorConfigureTarget((ReconstructionAbstractBehaviour)base.get_target(), null);
		}

		public void OnSceneGUI()
		{
			if (!EditorApplication.get_isPlaying())
			{
				ReconstructionAbstractBehaviour reconstructionAbstractBehaviour = (ReconstructionAbstractBehaviour)base.get_target();
				if (reconstructionAbstractBehaviour.transform.localScale != Vector3.one)
				{
					Debug.LogWarning("You currently cannot scale the smart terrain object");
					reconstructionAbstractBehaviour.transform.localScale = Vector3.one;
				}
			}
		}

		public override void OnInspectorGUI()
		{
			base.DrawDefaultInspector();
			VuforiaUtilities.DisableGuiForPrefab(base.get_target());
			using (base.get_serializedObject().Edit())
			{
				bool expr_28 = SceneManager.Instance.ExtendedTrackingEnabledOnATarget();
				if (expr_28)
				{
					EditorGUILayout.HelpBox("Smart Terrain cannot be enabled at the same time as Extended Tracking.", 1);
				}
				bool enabled = GUI.enabled;
				GUI.enabled = !expr_28;
				EditorGUILayout.PropertyField(this.mAutomaticStart, new GUIContent("Start Automatically"), new GUILayoutOption[0]);
				GUI.enabled = enabled;
				if (((ReconstructionAbstractBehaviour)base.get_target()).GetComponent<ReconstructionFromTargetAbstractBehaviour>() != null)
				{
					EditorGUILayout.HelpBox("Set the checkbox below to create a navigation mesh for the primary surface. A padding around props can be defined below.", 0);
					EditorGUILayout.PropertyField(this.mNavMeshUpdates, new GUIContent("Create Nav Mesh"), new GUILayoutOption[0]);
					if (this.mNavMeshUpdates.get_boolValue())
					{
						EditorGUILayout.PropertyField(this.mNavMeshPadding, new GUIContent("Nav Mesh Padding"), new GUILayoutOption[0]);
					}
					EditorGUILayout.HelpBox("Define the maximum area of smart terrain with an axis-aligned rectangle around the smart terrain center", 0);
					EditorGUILayout.PropertyField(this.mMaximumExtentEnabled, new GUIContent("Define Max Primary Surface Area"), new GUILayoutOption[0]);
					if (this.mMaximumExtentEnabled.get_boolValue())
					{
						this.mMaximumExtent.set_rectValue(EditorGUILayout.RectField("Maximum Area", this.mMaximumExtent.get_rectValue(), new GUILayoutOption[0]));
					}
				}
			}
			GUI.enabled = true;
		}
	}
}

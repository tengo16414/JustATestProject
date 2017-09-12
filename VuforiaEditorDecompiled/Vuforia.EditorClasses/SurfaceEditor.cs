using System;
using UnityEditor;
using UnityEngine;

namespace Vuforia.EditorClasses
{
	[CustomEditor(typeof(SurfaceAbstractBehaviour), true)]
	public class SurfaceEditor : Editor
	{
		private SerializedSmartTerrainTrackable mSerializedObject;

		public static void EditorConfigureTarget(SurfaceAbstractBehaviour surface, SerializedSmartTerrainTrackable serializedObject)
		{
			if (surface == null)
			{
				Debug.LogError("SurfaceAbstractBehaviour parameter is null !");
				return;
			}
			if (VuforiaUtilities.GetPrefabType(surface) == 1)
			{
				return;
			}
			if (!SceneManager.Instance.SceneInitialized)
			{
				SceneManager.Instance.InitScene();
			}
			using (serializedObject.Edit())
			{
				if (!EditorApplication.get_isPlaying())
				{
					serializedObject.InitializedInEditor = true;
				}
			}
		}

		public void OnEnable()
		{
			SurfaceAbstractBehaviour arg_22_0 = (SurfaceAbstractBehaviour)base.get_target();
			this.mSerializedObject = new SerializedSmartTerrainTrackable(base.get_serializedObject());
			SurfaceEditor.EditorConfigureTarget(arg_22_0, this.mSerializedObject);
		}

		public override void OnInspectorGUI()
		{
			base.DrawDefaultInspector();
			VuforiaUtilities.DisableGuiForPrefab(base.get_target());
			using (this.mSerializedObject.Edit())
			{
				EditorGUILayout.HelpBox("The mesh filter and collider selected below will be automatically updated with new mesh revisions of the primary smart terrain surface. Set them to None to ignoremesh updates.", 0);
				EditorGUILayout.PropertyField(this.mSerializedObject.MeshFilterToUpdateProperty, new GUIContent("MeshFilter to update"), new GUILayoutOption[0]);
				EditorGUILayout.PropertyField(this.mSerializedObject.MeshColliderToUpdateProperty, new GUIContent("MeshCollider to update"), new GUILayoutOption[0]);
			}
			if (GUI.changed)
			{
				SceneManager.Instance.SceneUpdated();
			}
		}
	}
}

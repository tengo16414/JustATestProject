using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Vuforia.EditorClasses
{
	[CustomEditor(typeof(VuforiaAbstractConfiguration))]
	public class VuforiaAbstractConfigurationEditor : Editor
	{
		private System.Collections.Generic.List<ConfigurationEditor> mSectionEditors;

		private const string mAssetsPath = "Assets";

		private const string mResourcesPath = "Resources";

		private const string mName = "VuforiaConfiguration.asset";

		private void OnEnable()
		{
			this.mSectionEditors = new System.Collections.Generic.List<ConfigurationEditor>
			{
				new GenericVuforiaConfigurationEditor(),
				new DigitalEyewearConfigurationEditor(),
				new DatabaseLoadEditor(),
				new VideoBackgroundEditor(),
				new SmartTerrainTrackerEditor(),
				new DeviceTrackerEditor(),
				new WebCamEditor()
			};
			using (System.Collections.Generic.List<ConfigurationEditor>.Enumerator enumerator = this.mSectionEditors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					enumerator.Current.FindSerializedProperties(base.get_serializedObject());
				}
			}
		}

		public override void OnInspectorGUI()
		{
			using (base.get_serializedObject().Edit())
			{
				foreach (ConfigurationEditor current in this.mSectionEditors)
				{
					bool flag = this.BeginSection(current.Title, current.Foldout);
					if (flag != current.Foldout)
					{
						current.SetFoldout(flag);
					}
					if (current.Foldout)
					{
						try
						{
							current.DrawInspectorGUI();
						}
						catch (ExitGUIException)
						{
						}
						catch (System.Exception ex)
						{
							Debug.LogError(string.Concat(new string[]
							{
								"Error in ",
								current.Title,
								" editor: ",
								ex.Message,
								"\n",
								ex.StackTrace
							}));
						}
					}
					this.EndSection();
				}
			}
		}

		[MenuItem("Vuforia/Configuration")]
		public static void Init()
		{
			Selection.set_activeObject(VuforiaAbstractConfigurationEditor.LoadConfigurationObject());
		}

		public static VuforiaAbstractConfiguration LoadConfigurationObject()
		{
			string text = "Assets/Resources/VuforiaConfiguration.asset";
			VuforiaAbstractConfiguration vuforiaAbstractConfiguration = AssetDatabase.LoadAssetAtPath(text, typeof(VuforiaAbstractConfiguration)) as VuforiaAbstractConfiguration;
			if (vuforiaAbstractConfiguration == null)
			{
				vuforiaAbstractConfiguration = VuforiaAbstractConfiguration.CreateInstance();
				if (vuforiaAbstractConfiguration != null)
				{
					if (!AssetDatabase.IsValidFolder("Assets/Resources"))
					{
						AssetDatabase.CreateFolder("Assets", "Resources");
					}
					AssetDatabase.CreateAsset(vuforiaAbstractConfiguration, text);
					AssetDatabase.SaveAssets();
				}
			}
			return vuforiaAbstractConfiguration;
		}

		private bool BeginSection(string title, bool foldout)
		{
			EditorStyles.get_foldout().fontStyle = FontStyle.Bold;
			return EditorGUILayout.Foldout(foldout, title);
		}

		private void EndSection()
		{
			EditorGUILayout.Space();
		}
	}
}

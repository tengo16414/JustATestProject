using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Vuforia.EditorClasses
{
	[CustomEditor(typeof(TextRecoAbstractBehaviour), true)]
	public class TextRecoEditor : Editor
	{
		private SerializedProperty mWordListFile;

		private SerializedProperty mCustomWordListFile;

		private SerializedProperty mAdditionalCustomWords;

		private SerializedProperty mFilterMode;

		private SerializedProperty mFilterListFile;

		private SerializedProperty mAdditionalFilterWords;

		private SerializedProperty mWordPrefabCreationMode;

		private SerializedProperty mMaximumWordInstances;

		public static void EditorConfigureTarget(TextRecoAbstractBehaviour trb)
		{
			if (trb == null)
			{
				Debug.LogError("TextRecoAbstractBehaviour parameter is null !");
				return;
			}
			if (VuforiaUtilities.GetPrefabType(trb) == 1)
			{
				return;
			}
			if (!SceneManager.Instance.SceneInitialized)
			{
				SceneManager.Instance.InitScene();
			}
		}

		public void OnEnable()
		{
			TextRecoEditor.EditorConfigureTarget((TextRecoAbstractBehaviour)base.get_target());
			this.mWordListFile = base.get_serializedObject().FindProperty("mWordListFile");
			this.mCustomWordListFile = base.get_serializedObject().FindProperty("mCustomWordListFile");
			this.mAdditionalCustomWords = base.get_serializedObject().FindProperty("mAdditionalCustomWords");
			this.mFilterMode = base.get_serializedObject().FindProperty("mFilterMode");
			this.mFilterListFile = base.get_serializedObject().FindProperty("mFilterListFile");
			this.mAdditionalFilterWords = base.get_serializedObject().FindProperty("mAdditionalFilterWords");
			this.mWordPrefabCreationMode = base.get_serializedObject().FindProperty("mWordPrefabCreationMode");
			this.mMaximumWordInstances = base.get_serializedObject().FindProperty("mMaximumWordInstances");
		}

		public override void OnInspectorGUI()
		{
			VuforiaUtilities.DisableGuiForPrefab(base.get_target());
			base.DrawDefaultInspector();
			using (base.get_serializedObject().Edit())
			{
				TextConfigData textConfigData = ConfigDataManager.Instance.GetTextConfigData();
				if (textConfigData.NumDictionaries > 1)
				{
					EditorGUILayout.HelpBox("The list of words the TextTracker can detect and track.\nThe word list is loaded from a binary file and can be extended by a list of custom words.", 1);
					string[] array = new string[textConfigData.NumDictionaries];
					string[] array2 = new string[textConfigData.NumDictionaries];
					textConfigData.CopyDictionaryNamesAndFiles(array, array2, 0);
					int num = VuforiaUtilities.GetIndexFromString(this.mWordListFile.get_stringValue(), array2);
					if (num < 0)
					{
						num = 0;
					}
					int num2 = EditorGUILayout.Popup("Word List", num, array, new GUILayoutOption[0]);
					this.mWordListFile.set_stringValue(array2[num2]);
					if (num2 == 0)
					{
						GUI.enabled = false;
					}
				}
				else
				{
					if (GUILayout.Button("No word list available. \nPlease copy it from the TextRecognition sample app. \nPress here to go to the download page for sample apps!", new GUILayoutOption[0]))
					{
						SceneManager.Instance.GoToSampleAppPage();
					}
					GUI.enabled = false;
				}
				int expr_D8 = textConfigData.NumWordLists;
				string[] array3 = new string[expr_D8];
				string[] array4 = new string[expr_D8];
				textConfigData.CopyWordListNamesAndFiles(array3, array4, 0);
				int num3 = VuforiaUtilities.GetIndexFromString(this.mCustomWordListFile.get_stringValue(), array4);
				if (num3 < 0)
				{
					num3 = 0;
				}
				int num4 = EditorGUILayout.Popup("Additional Word File", num3, array3, new GUILayoutOption[0]);
				if (num4 != num3)
				{
					if (num4 != 0)
					{
						TextRecoEditor.TestValidityOfWordListFile(array4[num4]);
					}
					this.mCustomWordListFile.set_stringValue(array4[num4]);
				}
				EditorGUILayout.LabelField("Additional Words:", new GUILayoutOption[0]);
				EditorGUILayout.HelpBox("Write one word per line. Open compound words can be specified using whitespaces.", 0);
				this.mAdditionalCustomWords.set_stringValue(EditorGUILayout.TextArea(this.mAdditionalCustomWords.get_stringValue(), new GUILayoutOption[0]));
				EditorGUILayout.Space();
				EditorGUILayout.Space();
				EditorGUILayout.HelpBox("The filter list allows to specify subset of words that will be detected and tracked.", 1);
				EditorGUILayout.PropertyField(this.mFilterMode, new GUIContent("Filter Mode"), new GUILayoutOption[0]);
				if (this.mFilterMode.get_enumValueIndex() != 0)
				{
					int num5 = VuforiaUtilities.GetIndexFromString(this.mFilterListFile.get_stringValue(), array4);
					if (num5 < 0)
					{
						num5 = 0;
					}
					int num6 = EditorGUILayout.Popup("Filter List File", num5, array3, new GUILayoutOption[0]);
					if (num6 != num5)
					{
						if (num6 != 0)
						{
							TextRecoEditor.TestValidityOfWordListFile(array4[num6]);
						}
						this.mFilterListFile.set_stringValue(array4[num6]);
					}
					EditorGUILayout.LabelField("Additional Filter Words:", new GUILayoutOption[0]);
					EditorGUILayout.HelpBox("Write one word per line. Open compound words can be specified using whitespaces.", 0);
					this.mAdditionalFilterWords.set_stringValue(EditorGUILayout.TextArea(this.mAdditionalFilterWords.get_stringValue(), new GUILayoutOption[0]));
				}
				EditorGUILayout.HelpBox("It is possible to use Word Prefabs to define augmentations for detected words. Each Word Prefab can be instantiated up to a maximum number.", 1);
				if (EditorGUILayout.Toggle("Use Word Prefabs", this.mWordPrefabCreationMode.get_enumValueIndex() == 1, new GUILayoutOption[0]))
				{
					this.mWordPrefabCreationMode.set_enumValueIndex(1);
					EditorGUILayout.PropertyField(this.mMaximumWordInstances, new GUIContent("Max Simultaneous Words"), new GUILayoutOption[0]);
				}
				else
				{
					this.mWordPrefabCreationMode.set_enumValueIndex(0);
				}
			}
			GUI.enabled = true;
		}

		public void OnSceneGUI()
		{
			Component arg_30_0 = (TextRecoAbstractBehaviour)base.get_target();
			GUIStyle gUIStyle = new GUIStyle
			{
				alignment = TextAnchor.LowerRight,
				fontSize = 18,
				normal = 
				{
					textColor = Color.white
				}
			};
			Handles.Label(arg_30_0.transform.position, "Text\nRecognition", gUIStyle);
		}

		private static void TestValidityOfWordListFile(string file)
		{
			string text = "Assets/StreamingAssets/" + file;
			bool flag = false;
			bool flag2 = true;
			System.IO.StreamReader streamReader = System.IO.File.OpenText(text);
			System.IO.Stream baseStream = streamReader.BaseStream;
			int i = 0;
			while (i < 7)
			{
				int num = baseStream.ReadByte();
				if (num != (int)"UTF-8\n\n"[i])
				{
					if (i == 0 && num == 239 && baseStream.ReadByte() == 187 && baseStream.ReadByte() == 191)
					{
						Debug.LogWarning("Only UTF-8 documents without BOM are supported.");
						flag2 = false;
						flag = true;
						break;
					}
					if (i == 5 && num == 13)
					{
						Debug.LogWarning("Only UTF-8 documents without CARRIAGE RETURN are supported.");
						flag2 = false;
						flag = true;
						break;
					}
					Debug.LogWarning("Not a valid UTF-8 encoded file. It needs to start with the header \"UTF-8\" followed by an empty line.");
					flag2 = false;
					break;
				}
				else
				{
					i++;
				}
			}
			int num2 = 0;
			while (flag2 && !streamReader.EndOfStream)
			{
				string text2 = streamReader.ReadLine();
				if (text2.Length == 0)
				{
					Debug.LogWarning("There is an empty line in your word list.");
					flag2 = false;
				}
				else if (text2.Length < 2)
				{
					Debug.LogWarning("The word " + text2 + " is too short for Text-Reco.");
					flag2 = false;
				}
				else if (text2.Length > 45)
				{
					Debug.LogWarning("The word " + text2 + " is too long for Text-Reco.");
					flag2 = false;
				}
				else
				{
					string text3 = text2;
					for (int j = 0; j < text3.Length; j++)
					{
						char c = text3[j];
						if ((c < 'A' || c > 'Z') && (c < 'a' || c > 'z') && c != '-' && c != '\'' && c != ' ')
						{
							Debug.LogWarning("The word " + text2 + " is not supported because of character " + c.ToString());
							flag2 = false;
						}
					}
				}
				num2++;
				if (num2 > 10000)
				{
					Debug.LogWarning("The maximum number of words is " + 10000 + ".");
					flag2 = false;
				}
			}
			streamReader.Close();
			if (!flag2 & flag)
			{
				TextRecoEditor.ConvertEOLAndEncodingIfUserWantsTo(text);
			}
		}

		private static void ConvertEOLAndEncodingIfUserWantsTo(string file)
		{
			if (EditorUtility.DisplayDialog("Wrong Line Endings", "Would you like to automatically convert the line endings and/or remove the BOM of the selected word list file?", "Yes", "No"))
			{
				string contents = System.IO.File.ReadAllText(file).Replace("\r\n", "\n");
				System.IO.File.WriteAllText(file, contents, new System.Text.UTF8Encoding(false));
			}
		}
	}
}

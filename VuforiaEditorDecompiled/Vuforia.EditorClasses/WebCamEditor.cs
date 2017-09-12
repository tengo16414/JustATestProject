using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Vuforia.EditorClasses
{
	internal class WebCamEditor : ConfigurationEditor
	{
		private const string NO_CAMERAS_TEXT = "NO CAMERAS FOUND";

		private static WebCamProfile sWebCamProfiles;

		private static string[] sWebCamDeviceNames = new string[0];

		private static bool sWebCamDevicesReadOnce = false;

		private static System.DateTime sLastDeviceListRefresh = System.DateTime.Now - System.TimeSpan.FromDays(1.0);

		private SerializedProperty mDeviceNameSetInEditor;

		private SerializedProperty mFlipHorizontally;

		private SerializedProperty mTurnOffWebCam;

		private SerializedProperty mRenderTextureLayer;

		private static System.TimeSpan DeviceListRefreshInterval
		{
			get
			{
				if (Application.platform != RuntimePlatform.OSXEditor)
				{
					return System.TimeSpan.FromMilliseconds(500.0);
				}
				return System.TimeSpan.FromMilliseconds(5000.0);
			}
		}

		public override string Title
		{
			get
			{
				return "Webcam";
			}
		}

		public override void FindSerializedProperties(SerializedObject serializedObject)
		{
			this.mDeviceNameSetInEditor = serializedObject.FindProperty("webcam.deviceNameSetInEditor");
			this.mFlipHorizontally = serializedObject.FindProperty("webcam.flipHorizontally");
			this.mTurnOffWebCam = serializedObject.FindProperty("webcam.turnOffWebCam");
			this.mRenderTextureLayer = serializedObject.FindProperty("webcam.renderTextureLayer");
		}

		public override void DrawInspectorGUI()
		{
			EditorGUILayout.PropertyField(this.mTurnOffWebCam, new GUIContent("Disable Vuforia Play Mode"), new GUILayoutOption[0]);
			if (!this.mTurnOffWebCam.get_boolValue())
			{
				if (!VuforiaRuntimeUtilities.CheckNativePluginSupport())
				{
					EditorGUILayout.HelpBox("An error occurred while trying to enable Vuforia play mode", 3);
				}
				int num = 0;
				string[] deviceNames = this.GetDeviceNames();
				for (int i = 0; i < deviceNames.Length; i++)
				{
					if (deviceNames[i] != null && deviceNames[i].Equals(this.mDeviceNameSetInEditor.get_stringValue()))
					{
						num = i;
					}
				}
				if (WebCamEditor.sWebCamProfiles == null)
				{
					WebCamEditor.sWebCamProfiles = new WebCamProfile();
				}
				if (deviceNames[num].Equals("NO CAMERAS FOUND"))
				{
					EditorGUILayout.HelpBox("No camera connected!\nTo run your application using Play Mode, please connect a webcam to your computer.", 2);
				}
				else if (!WebCamEditor.sWebCamProfiles.ProfileAvailable(deviceNames[num]))
				{
					EditorGUILayout.HelpBox(string.Concat(new string[]
					{
						"No webcam profile has been found for your webcam model: '",
						deviceNames[num],
						"'.\nA default profile will be used. \n\nWebcam profiles ensure that Play Mode performs well with your webcam. \nYou can create a custom profile for your camera by editing  '",
						System.IO.Path.Combine(Application.dataPath, "Editor/QCAR/WebcamProfiles/profiles.xml"),
						"'."
					}), 2);
				}
				EditorGUILayout.Space();
				EditorGUILayout.BeginHorizontal(new GUILayoutOption[0]);
				EditorGUILayout.PrefixLabel("Camera Device");
				int num2 = EditorGUILayout.Popup(num, deviceNames, new GUILayoutOption[0]);
				if (num2 != num && !deviceNames[num2].Equals("NO CAMERAS FOUND"))
				{
					this.mDeviceNameSetInEditor.set_stringValue(deviceNames[num2]);
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.PropertyField(this.mFlipHorizontally, new GUIContent("Flip Horizontally"), new GUILayoutOption[0]);
				EditorGUILayout.Space();
				EditorGUILayout.HelpBox("Here you can enter the index of the layer that will be used internally for our render to texture functionality. the ARCamera will be configured to not draw this layer.", 0);
				EditorGUILayout.PropertyField(this.mRenderTextureLayer, new GUIContent("Render Texture Layer"), new GUILayoutOption[0]);
			}
		}

		private string[] GetDeviceNames()
		{
			if ((!EditorApplication.get_isPlaying() || !WebCamEditor.sWebCamDevicesReadOnce) && System.DateTime.Now - WebCamEditor.DeviceListRefreshInterval > WebCamEditor.sLastDeviceListRefresh)
			{
				try
				{
					WebCamDevice[] devices = WebCamTexture.devices;
					int num = WebCamTexture.devices.Length;
					if (num > 0)
					{
						WebCamEditor.sWebCamDeviceNames = new string[num];
						for (int i = 0; i < num; i++)
						{
							WebCamEditor.sWebCamDeviceNames[i] = devices[i].name;
						}
					}
					else
					{
						WebCamEditor.sWebCamDeviceNames = new string[1];
						WebCamEditor.sWebCamDeviceNames[0] = "NO CAMERAS FOUND";
					}
				}
				catch (System.Exception arg_83_0)
				{
					Debug.Log(arg_83_0.Message);
				}
				WebCamEditor.sWebCamDevicesReadOnce = true;
				WebCamEditor.sLastDeviceListRefresh = System.DateTime.Now;
			}
			return WebCamEditor.sWebCamDeviceNames;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;

namespace Vuforia.EditorClasses
{
	[InitializeOnLoad]
	public class UnityPlayModeEditorUtility : IPlayModeEditorUtility
	{
		static UnityPlayModeEditorUtility()
		{
			PlayModeEditorUtility.Instance = new UnityPlayModeEditorUtility();
		}

		public void DisplayDialog(string title, string message, string ok)
		{
			EditorUtility.DisplayDialog(title, message, ok);
		}

		public WebCamProfile.ProfileCollection LoadAndParseWebcamProfiles(string path)
		{
			System.Collections.Generic.Dictionary<string, WebCamProfile.ProfileData> dictionary = new System.Collections.Generic.Dictionary<string, WebCamProfile.ProfileData>();
			WebCamProfile.ProfileData defaultProfile;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(path);
				foreach (XmlNode xmlNode in xmlDocument.GetElementsByTagName("webcam"))
				{
					dictionary[xmlNode.Attributes["deviceName"].Value.ToLower()] = this.ParseConfigurationEntry(xmlNode);
				}
				defaultProfile = this.ParseConfigurationEntry(xmlDocument.GetElementsByTagName("default")[0]);
			}
			catch (System.Exception ex)
			{
				string text = "Exception occurred when trying to parse web cam profile file: " + ex.Message;
				EditorUtility.DisplayDialog("Error occurred!", text, "Ok");
				Debug.LogError(text);
				defaultProfile = new WebCamProfile.ProfileData
				{
					RequestedFPS = 30,
					RequestedTextureSize = new VuforiaRenderer.Vec2I(640, 480),
					ResampledTextureSize = new VuforiaRenderer.Vec2I(640, 0)
				};
			}
			return new WebCamProfile.ProfileCollection(defaultProfile, dictionary);
		}

		public void RestartPlayMode()
		{
			EditorApplication.update = (EditorApplication.CallbackFunction)System.Delegate.Combine(EditorApplication.update, new EditorApplication.CallbackFunction(UnityPlayModeEditorUtility.CheckToStartPlayMode));
			EditorApplication.set_isPlaying(false);
		}

		public void ShowErrorInMouseOverWindow(string errorMsg)
		{
			EditorWindow.get_mouseOverWindow().ShowNotification(new GUIContent(errorMsg));
		}

		private WebCamProfile.ProfileData ParseConfigurationEntry(XmlNode cameraNode)
		{
			foreach (XmlNode xmlNode in cameraNode.ChildNodes)
			{
				string value = "undefined";
				if (Application.platform == RuntimePlatform.WindowsEditor)
				{
					value = "windows";
				}
				if (Application.platform == RuntimePlatform.OSXEditor)
				{
					value = "osx";
				}
				if (xmlNode.Name.Equals(value))
				{
					int v = 0;
					string valueOfChildNodeByName = UnityPlayModeEditorUtility.GetValueOfChildNodeByName(xmlNode, "resampledTextureHeight");
					if (!valueOfChildNodeByName.Equals(string.Empty))
					{
						v = int.Parse(valueOfChildNodeByName);
					}
					WebCamProfile.ProfileData profileData = default(WebCamProfile.ProfileData);
					profileData.RequestedTextureSize = new VuforiaRenderer.Vec2I(int.Parse(UnityPlayModeEditorUtility.GetValueOfChildNodeByName(xmlNode, "requestedTextureWidth")), int.Parse(UnityPlayModeEditorUtility.GetValueOfChildNodeByName(xmlNode, "requestedTextureHeight")));
					profileData.ResampledTextureSize = new VuforiaRenderer.Vec2I(int.Parse(UnityPlayModeEditorUtility.GetValueOfChildNodeByName(xmlNode, "resampledTextureWidth")), v);
					profileData.RequestedFPS = 30;
					profileData = profileData;
					return profileData;
				}
			}
			throw new System.Exception("Could not parse webcam profile: " + cameraNode.InnerXml);
		}

		private static string GetValueOfChildNodeByName(XmlNode parentNode, string name)
		{
			foreach (XmlNode xmlNode in parentNode.ChildNodes)
			{
				if (xmlNode.Name.Equals(name))
				{
					return xmlNode.InnerXml;
				}
			}
			return string.Empty;
		}

		private static void CheckToStartPlayMode()
		{
			if (!EditorApplication.get_isPlaying())
			{
				EditorApplication.update = (EditorApplication.CallbackFunction)System.Delegate.Remove(EditorApplication.update, new EditorApplication.CallbackFunction(UnityPlayModeEditorUtility.CheckToStartPlayMode));
				EditorApplication.set_isPlaying(true);
				Debug.LogWarning("Restarted Play Mode because scripts have been recompiled.");
			}
		}
	}
}

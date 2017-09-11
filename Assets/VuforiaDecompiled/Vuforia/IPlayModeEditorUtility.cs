using System;

namespace Vuforia
{
	internal interface IPlayModeEditorUtility
	{
		void DisplayDialog(string title, string message, string ok);

		WebCamProfile.ProfileCollection LoadAndParseWebcamProfiles(string path);

		void RestartPlayMode();

		void ShowErrorInMouseOverWindow(string error);
	}
}

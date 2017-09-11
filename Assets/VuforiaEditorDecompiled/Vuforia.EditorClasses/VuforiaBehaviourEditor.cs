using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Vuforia.EditorClasses
{
	[CustomEditor(typeof(VuforiaAbstractBehaviour), true)]
	public class VuforiaBehaviourEditor : Editor
	{
		private const string NOTE_CAM_INVALID = "The selected camera needs to be upgraded to be used for Augmented Reality. Please click the button below to do that.";

		private const string BUTTON_CONFIG_CAM = "Add Vuforia Components";

		private const string TOOL_TIP_CONFIG_PRIMARY_CAM = "Adds the VideoBackgroundBehaviour, HideExcessBehaviour and a background plane.";

		private const string TOOL_TIP_CONFIG_SECONDARY_CAM = "Adds the VideoBackgroundBehaviour and HideExcessBehaviour.";

		private VuforiaAbstractBehaviour mBhvr;

		private SerializedProperty mWorldCenterMode;

		private SerializedProperty mWorldCenter;

		private SerializedProperty mCentralAnchorPoint;

		private SerializedProperty mParentAnchorPoint;

		private SerializedProperty mPrimaryCamera;

		private SerializedProperty mSecondaryCamera;

		private SerializedProperty mWereBindingFieldsExposed;

		private Camera PrimaryCamera
		{
			get
			{
				return (Camera)this.mPrimaryCamera.get_objectReferenceValue();
			}
			set
			{
				this.mPrimaryCamera.set_objectReferenceValue(value);
			}
		}

		private Camera SecondaryCamera
		{
			get
			{
				return (Camera)this.mSecondaryCamera.get_objectReferenceValue();
			}
			set
			{
				this.mSecondaryCamera.set_objectReferenceValue(value);
			}
		}

		private Transform CentralAnchorPoint
		{
			get
			{
				return (Transform)this.mCentralAnchorPoint.get_objectReferenceValue();
			}
			set
			{
				this.mCentralAnchorPoint.set_objectReferenceValue(value);
			}
		}

		private Transform ParentAnchorPoint
		{
			get
			{
				return (Transform)this.mParentAnchorPoint.get_objectReferenceValue();
			}
			set
			{
				this.mParentAnchorPoint.set_objectReferenceValue(value);
			}
		}

		private bool WereBindingFieldsExposed
		{
			get
			{
				return this.mWereBindingFieldsExposed.get_boolValue();
			}
			set
			{
				this.mWereBindingFieldsExposed.set_boolValue(value);
			}
		}

		public void OnEnable()
		{
			this.mBhvr = (VuforiaAbstractBehaviour)base.get_target();
			this.mWorldCenterMode = base.get_serializedObject().FindProperty("mWorldCenterMode");
			this.mWorldCenter = base.get_serializedObject().FindProperty("mWorldCenter");
			this.mCentralAnchorPoint = base.get_serializedObject().FindProperty("mCentralAnchorPoint");
			this.mParentAnchorPoint = base.get_serializedObject().FindProperty("mParentAnchorPoint");
			this.mPrimaryCamera = base.get_serializedObject().FindProperty("mPrimaryCamera");
			this.mSecondaryCamera = base.get_serializedObject().FindProperty("mSecondaryCamera");
			this.mWereBindingFieldsExposed = base.get_serializedObject().FindProperty("mWereBindingFieldsExposed");
		}

		public override void OnInspectorGUI()
		{
			VuforiaUtilities.DisableGuiForPrefab(base.get_target());
			base.DrawDefaultInspector();
			using (base.get_serializedObject().Edit())
			{
				bool flag = true;
				bool showCameraAnchors = true;
				bool flag2 = true;
				if (VuforiaUtilities.GetPrefabType(base.get_target()) != 1)
				{
					flag2 = false;
					VuforiaAbstractConfiguration expr_3C = VuforiaAbstractConfigurationEditor.LoadConfigurationObject();
					VuforiaAbstractConfiguration.DigitalEyewearConfiguration digitalEyewear = expr_3C.DigitalEyewear;
					object arg_74_0 = digitalEyewear.EyewearType == DigitalEyewearARController.EyewearType.VideoSeeThrough && digitalEyewear.StereoFramework > DigitalEyewearARController.StereoFramework.Vuforia;
					bool flag3 = digitalEyewear.EyewearType == DigitalEyewearARController.EyewearType.OpticalSeeThrough && digitalEyewear.SeeThroughConfiguration == DigitalEyewearARController.SeeThroughConfiguration.HoloLens;
					object expr_74 = arg_74_0;
					flag = ((expr_74 | flag3) != null);
					showCameraAnchors = (expr_74 != null);
					bool autoInitAndStartTracker = expr_3C.DeviceTracker.AutoInitAndStartTracker;
					int num = 3;
					if (autoInitAndStartTracker && this.mWorldCenterMode.get_intValue() != num)
					{
						this.mWorldCenterMode.set_intValue(num);
					}
					else if (!autoInitAndStartTracker && this.mWorldCenterMode.get_intValue() == num)
					{
						this.mWorldCenterMode.set_intValue(2);
					}
					if (flag && !this.WereBindingFieldsExposed)
					{
						this.WereBindingFieldsExposed = true;
						if (this.CentralAnchorPoint == null && (this.mWorldCenterMode.get_intValue() == 1 || this.mWorldCenterMode.get_intValue() == 0))
						{
							this.mWorldCenterMode.set_intValue(2);
						}
					}
					else if (!flag && this.WereBindingFieldsExposed)
					{
						this.WereBindingFieldsExposed = false;
					}
				}
				VuforiaARController.WorldCenterMode worldCenterMode = this.DrawWorldCenterModeInspector();
				if (flag2 || worldCenterMode == VuforiaARController.WorldCenterMode.SPECIFIC_TARGET)
				{
					this.DrawWorldCenterInspector();
				}
				if (flag)
				{
					this.DrawDigitalEyewearInspector(worldCenterMode, showCameraAnchors);
				}
			}
			if (GUILayout.Button("Open Vuforia configuration", new GUILayoutOption[0]))
			{
				VuforiaAbstractConfigurationEditor.Init();
			}
		}

		private VuforiaARController.WorldCenterMode DrawWorldCenterModeInspector()
		{
			int intValue = this.mWorldCenterMode.get_intValue();
			EditorGUILayout.PropertyField(this.mWorldCenterMode, new GUIContent("World Center Mode"), new GUILayoutOption[0]);
			if (this.mWorldCenterMode.get_intValue() != intValue)
			{
				VuforiaAbstractConfiguration vuforiaAbstractConfiguration = VuforiaAbstractConfigurationEditor.LoadConfigurationObject();
				if (this.mWorldCenterMode.get_intValue() == 3)
				{
					vuforiaAbstractConfiguration.DeviceTracker.AutoInitAndStartTracker = true;
					PlayModeEditorUtility.Instance.ShowErrorInMouseOverWindow("Device tracker will start automatically. See Device Tracker Configuration.");
				}
				else if (intValue == 3)
				{
					vuforiaAbstractConfiguration.DeviceTracker.AutoInitAndStartTracker = false;
					PlayModeEditorUtility.Instance.ShowErrorInMouseOverWindow("Device tracker won't be started. See Device Tracker Configuration.");
				}
			}
			return (VuforiaARController.WorldCenterMode)this.mWorldCenterMode.get_intValue();
		}

		private void DrawWorldCenterInspector()
		{
			bool flag = !EditorUtility.IsPersistent(base.get_target());
			TrackableBehaviour trackableBehaviour = (TrackableBehaviour)EditorGUILayout.ObjectField("World Center", this.mWorldCenter.get_objectReferenceValue(), typeof(TrackableBehaviour), flag, new GUILayoutOption[0]);
			if (trackableBehaviour is WordAbstractBehaviour)
			{
				trackableBehaviour = null;
				EditorWindow.get_focusedWindow().ShowNotification(new GUIContent("Word behaviours cannot be selected as world center."));
			}
			else if (trackableBehaviour is PropAbstractBehaviour)
			{
				trackableBehaviour = null;
				EditorWindow.get_focusedWindow().ShowNotification(new GUIContent("Prop Behaviours cannot be selected as world center - please select the SmartTerrain object instead."));
			}
			this.mWorldCenter.set_objectReferenceValue(trackableBehaviour);
		}

		private void DrawDigitalEyewearInspector(VuforiaARController.WorldCenterMode worldCenterMode, bool showCameraAnchors)
		{
			EditorGUILayout.PropertyField(this.mCentralAnchorPoint, new GUIContent("Central Anchor Point"), new GUILayoutOption[0]);
			if (showCameraAnchors && worldCenterMode != VuforiaARController.WorldCenterMode.CAMERA && worldCenterMode != VuforiaARController.WorldCenterMode.DEVICE_TRACKING)
			{
				EditorGUILayout.HelpBox("Using world center mode " + worldCenterMode + " when binding to a camera controlled by another SDK is non-trivial. Please consult the developer guide for more information", 1);
				UnityEngine.Object objectReferenceValue = this.mParentAnchorPoint.get_objectReferenceValue();
				EditorGUILayout.PropertyField(this.mParentAnchorPoint, new GUIContent("Parent Anchor Point"), new GUILayoutOption[0]);
				if (this.ParentAnchorPoint != objectReferenceValue && this.ParentAnchorPoint != null && this.CentralAnchorPoint != null && this.ParentAnchorPoint != this.CentralAnchorPoint && !this.CentralAnchorPoint.IsChildOf(this.ParentAnchorPoint))
				{
					EditorWindow.get_focusedWindow().ShowNotification(new GUIContent(this.ParentAnchorPoint.gameObject.name + " is not a parent of " + this.CentralAnchorPoint.gameObject.name + "!"));
					this.ParentAnchorPoint = this.CentralAnchorPoint;
				}
			}
			else
			{
				this.ParentAnchorPoint = this.CentralAnchorPoint;
			}
			if (showCameraAnchors)
			{
				EditorGUILayout.PropertyField(this.mPrimaryCamera, new GUIContent("Left Camera"), new GUILayoutOption[0]);
				if (this.PrimaryCamera != null && !VuforiaARController.IsValidPrimaryCamera(this.PrimaryCamera))
				{
					EditorGUILayout.HelpBox("The selected camera needs to be upgraded to be used for Augmented Reality. Please click the button below to do that.", 2);
					if (GUILayout.Button(new GUIContent("Add Vuforia Components", "Adds the VideoBackgroundBehaviour, HideExcessBehaviour and a background plane."), new GUILayoutOption[0]))
					{
						this.AddPrimaryCameraComponents(this.PrimaryCamera, this.mBhvr);
					}
					EditorGUILayout.Space();
				}
				EditorGUILayout.PropertyField(this.mSecondaryCamera, new GUIContent("Right Camera"), new GUILayoutOption[0]);
				if (this.SecondaryCamera != null && !VuforiaARController.IsValidSecondaryCamera(this.SecondaryCamera))
				{
					EditorGUILayout.HelpBox("The selected camera needs to be upgraded to be used for Augmented Reality. Please click the button below to do that.", 2);
					if (GUILayout.Button(new GUIContent("Add Vuforia Components", "Adds the VideoBackgroundBehaviour and HideExcessBehaviour."), new GUILayoutOption[0]))
					{
						this.AddSecondaryCameraComponents(this.SecondaryCamera, this.mBhvr);
						return;
					}
				}
			}
			else if (PlayerSettings.get_defaultInterfaceOrientation() != 3)
			{
				PlayerSettings.set_defaultInterfaceOrientation(3);
				Debug.Log("Setting Default Orientation to Landscape Left");
			}
		}

		private void AddPrimaryCameraComponents(Camera targetCamera, MonoBehaviour vuforiaBehaviour)
		{
			this.AddSecondaryCameraComponents(targetCamera, vuforiaBehaviour);
			if (targetCamera.GetComponentInChildren<BackgroundPlaneAbstractBehaviour>() == null)
			{
				BackgroundPlaneAbstractBehaviour[] componentsInChildren = vuforiaBehaviour.GetComponentsInChildren<BackgroundPlaneAbstractBehaviour>(true);
				if (componentsInChildren.Length != 0)
				{
					GameObject gameObject = componentsInChildren[0].gameObject;
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					if (gameObject2 != null)
					{
						gameObject2.name = gameObject.name;
						gameObject2.transform.parent = targetCamera.transform;
						gameObject2.transform.localPosition = targetCamera.transform.localPosition;
						gameObject2.transform.localRotation = targetCamera.transform.localRotation;
						gameObject2.transform.localScale = targetCamera.transform.localScale;
					}
				}
			}
		}

		private void AddSecondaryCameraComponents(Camera targetCamera, MonoBehaviour vuforiaBehaviour)
		{
			if (targetCamera.GetComponent<VideoBackgroundAbstractBehaviour>() == null)
			{
				VideoBackgroundAbstractBehaviour[] componentsInChildren = vuforiaBehaviour.GetComponentsInChildren<VideoBackgroundAbstractBehaviour>(true);
				if (componentsInChildren.Length != 0 && ComponentUtility.CopyComponent(componentsInChildren[0]))
				{
					ComponentUtility.PasteComponentAsNew(targetCamera.gameObject);
				}
			}
			if (targetCamera.GetComponent<HideExcessAreaAbstractBehaviour>() == null)
			{
				HideExcessAreaAbstractBehaviour[] componentsInChildren2 = vuforiaBehaviour.GetComponentsInChildren<HideExcessAreaAbstractBehaviour>(true);
				if (componentsInChildren2.Length != 0 && ComponentUtility.CopyComponent(componentsInChildren2[0]))
				{
					ComponentUtility.PasteComponentAsNew(targetCamera.gameObject);
				}
			}
		}
	}
}

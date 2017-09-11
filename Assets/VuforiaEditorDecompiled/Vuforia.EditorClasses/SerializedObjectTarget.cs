using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Vuforia.EditorClasses
{
	public class SerializedObjectTarget : SerializedDataSetTrackable
	{
		private readonly SerializedProperty mAspectRatioXY;

		private readonly SerializedProperty mAspectRatioXZ;

		private readonly SerializedProperty mShowBoundingBox;

		private readonly SerializedProperty mBBoxMin;

		private readonly SerializedProperty mBBoxMax;

		private readonly SerializedProperty mPreviewImage;

		private readonly SerializedProperty mLength;

		private readonly SerializedProperty mWidth;

		private readonly SerializedProperty mHeight;

		public SerializedProperty AspectRatioXYProperty
		{
			get
			{
				return this.mAspectRatioXY;
			}
		}

		public float AspectRatioXY
		{
			get
			{
				return this.mAspectRatioXY.get_floatValue();
			}
			set
			{
				this.mAspectRatioXY.set_floatValue(value);
			}
		}

		public SerializedProperty AspectRatioXZProperty
		{
			get
			{
				return this.mAspectRatioXZ;
			}
		}

		public float AspectRatioXZ
		{
			get
			{
				return this.mAspectRatioXZ.get_floatValue();
			}
			set
			{
				this.mAspectRatioXZ.set_floatValue(value);
			}
		}

		public SerializedProperty ShowBoundingBoxProperty
		{
			get
			{
				return this.mShowBoundingBox;
			}
		}

		public bool ShowBoundingBox
		{
			get
			{
				return this.mShowBoundingBox.get_boolValue();
			}
			set
			{
				this.mShowBoundingBox.set_boolValue(value);
			}
		}

		public SerializedProperty BBoxMinProperty
		{
			get
			{
				return this.mBBoxMin;
			}
		}

		public Vector3 BBoxMin
		{
			get
			{
				return this.mBBoxMin.get_vector3Value();
			}
			set
			{
				this.mBBoxMin.set_vector3Value(value);
			}
		}

		public SerializedProperty BBoxMaxProperty
		{
			get
			{
				return this.mBBoxMax;
			}
		}

		public Vector3 BBoxMax
		{
			get
			{
				return this.mBBoxMax.get_vector3Value();
			}
			set
			{
				this.mBBoxMax.set_vector3Value(value);
			}
		}

		public SerializedProperty PreviewImageProperty
		{
			get
			{
				return this.mPreviewImage;
			}
		}

		public Texture2D PreviewImage
		{
			get
			{
				return this.mPreviewImage.get_objectReferenceValue() as Texture2D;
			}
			set
			{
				this.mPreviewImage.set_objectReferenceValue(value);
			}
		}

		public SerializedProperty LengthProperty
		{
			get
			{
				return this.mLength;
			}
		}

		public float Length
		{
			get
			{
				return this.mLength.get_floatValue();
			}
			set
			{
				this.mLength.set_floatValue(value);
			}
		}

		public SerializedProperty WidthProperty
		{
			get
			{
				return this.mWidth;
			}
		}

		public float Width
		{
			get
			{
				return this.mWidth.get_floatValue();
			}
			set
			{
				this.mWidth.set_floatValue(value);
			}
		}

		public SerializedProperty HeightProperty
		{
			get
			{
				return this.mHeight;
			}
		}

		public float Height
		{
			get
			{
				return this.mHeight.get_floatValue();
			}
			set
			{
				this.mHeight.set_floatValue(value);
			}
		}

		public SerializedObjectTarget(SerializedObject target) : base(target)
		{
			this.mAspectRatioXY = target.FindProperty("mAspectRatioXY");
			this.mAspectRatioXZ = target.FindProperty("mAspectRatioXZ");
			this.mShowBoundingBox = target.FindProperty("mShowBoundingBox");
			this.mBBoxMin = target.FindProperty("mBBoxMin");
			this.mBBoxMax = target.FindProperty("mBBoxMax");
			this.mPreviewImage = target.FindProperty("mPreviewImage");
			this.mLength = target.FindProperty("mLength");
			this.mWidth = target.FindProperty("mWidth");
			this.mHeight = target.FindProperty("mHeight");
		}

		public System.Collections.Generic.List<ObjectTargetAbstractBehaviour> GetBehaviours()
		{
			System.Collections.Generic.List<ObjectTargetAbstractBehaviour> list = new System.Collections.Generic.List<ObjectTargetAbstractBehaviour>();
			UnityEngine.Object[] targetObjects = this.mSerializedObject.get_targetObjects();
			for (int i = 0; i < targetObjects.Length; i++)
			{
				UnityEngine.Object @object = targetObjects[i];
				list.Add((ObjectTargetAbstractBehaviour)@object);
			}
			return list;
		}
	}
}

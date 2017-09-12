using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Vuforia.EditorClasses
{
	public class SerializedVuMark : SerializedDataSetTrackable
	{
		private readonly SerializedProperty mAspectRatio;

		private readonly SerializedProperty mWidth;

		private readonly SerializedProperty mHeight;

		private readonly SerializedProperty mPreviewImage;

		private readonly SerializedProperty mIdType;

		private readonly SerializedProperty mIdLength;

		private readonly SerializedProperty mTrackingFromRuntimeAppearance;

		private readonly SerializedProperty mBoundingBox;

		private readonly SerializedProperty mOrigin;

		public SerializedProperty AspectRatioProperty
		{
			get
			{
				return this.mAspectRatio;
			}
		}

		public float AspectRatio
		{
			get
			{
				return this.mAspectRatio.get_floatValue();
			}
			set
			{
				this.mAspectRatio.set_floatValue(value);
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
				return this.mWidth.get_floatValue();
			}
			set
			{
				this.mWidth.set_floatValue(value);
			}
		}

		public SerializedProperty PreviewImageProperty
		{
			get
			{
				return this.mPreviewImage;
			}
		}

		public string PreviewImage
		{
			get
			{
				return this.mPreviewImage.get_stringValue();
			}
			set
			{
				this.mPreviewImage.set_stringValue(value);
			}
		}

		public SerializedProperty IdTypeProperty
		{
			get
			{
				return this.mIdType;
			}
		}

		public InstanceIdType IdType
		{
			get
			{
				return (InstanceIdType)this.mIdType.get_enumValueIndex();
			}
			set
			{
				this.mIdType.set_enumValueIndex((int)value);
			}
		}

		public SerializedProperty IdLengthProperty
		{
			get
			{
				return this.mIdLength;
			}
		}

		public int IdLength
		{
			get
			{
				return this.mIdLength.get_intValue();
			}
			set
			{
				this.mIdLength.set_intValue(value);
			}
		}

		public SerializedProperty BoundingBoxProperty
		{
			get
			{
				return this.mBoundingBox;
			}
		}

		public Rect BoundingBox
		{
			get
			{
				return this.mBoundingBox.get_rectValue();
			}
			set
			{
				this.mBoundingBox.set_rectValue(value);
			}
		}

		public SerializedProperty OriginProperty
		{
			get
			{
				return this.mOrigin;
			}
		}

		public Vector2 Origin
		{
			get
			{
				return this.mOrigin.get_vector2Value();
			}
			set
			{
				this.mOrigin.set_vector2Value(value);
			}
		}

		public SerializedProperty TrackingFromRuntimeAppearanceProperty
		{
			get
			{
				return this.mTrackingFromRuntimeAppearance;
			}
		}

		public bool TrackingFromRuntimeAppearance
		{
			get
			{
				return this.mTrackingFromRuntimeAppearance.get_boolValue();
			}
			set
			{
				this.mTrackingFromRuntimeAppearance.set_boolValue(value);
			}
		}

		public SerializedVuMark(SerializedObject target) : base(target)
		{
			this.mAspectRatio = target.FindProperty("mAspectRatio");
			this.mWidth = this.mSerializedObject.FindProperty("mWidth");
			this.mHeight = this.mSerializedObject.FindProperty("mHeight");
			this.mPreviewImage = this.mSerializedObject.FindProperty("mPreviewImage");
			this.mIdType = this.mSerializedObject.FindProperty("mIdType");
			this.mIdLength = this.mSerializedObject.FindProperty("mIdLength");
			this.mTrackingFromRuntimeAppearance = this.mSerializedObject.FindProperty("mTrackingFromRuntimeAppearance");
			this.mBoundingBox = this.mSerializedObject.FindProperty("mBoundingBox");
			this.mOrigin = this.mSerializedObject.FindProperty("mOrigin");
		}

		public System.Collections.Generic.List<VuMarkAbstractBehaviour> GetBehaviours()
		{
			System.Collections.Generic.List<VuMarkAbstractBehaviour> list = new System.Collections.Generic.List<VuMarkAbstractBehaviour>();
			UnityEngine.Object[] targetObjects = this.mSerializedObject.get_targetObjects();
			for (int i = 0; i < targetObjects.Length; i++)
			{
				UnityEngine.Object @object = targetObjects[i];
				list.Add((VuMarkAbstractBehaviour)@object);
			}
			return list;
		}
	}
}

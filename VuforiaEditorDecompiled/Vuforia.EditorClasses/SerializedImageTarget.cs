using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Vuforia.EditorClasses
{
	public class SerializedImageTarget : SerializedDataSetTrackable
	{
		private readonly SerializedProperty mAspectRatio;

		private readonly SerializedProperty mImageTargetType;

		private readonly SerializedProperty mWidth;

		private readonly SerializedProperty mHeight;

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

		public SerializedProperty ImageTargetTypeProperty
		{
			get
			{
				return this.mImageTargetType;
			}
		}

		public ImageTargetType ImageTargetType
		{
			get
			{
				return (ImageTargetType)this.mImageTargetType.get_enumValueIndex();
			}
			set
			{
				this.mImageTargetType.set_enumValueIndex((int)value);
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

		public SerializedImageTarget(SerializedObject target) : base(target)
		{
			this.mAspectRatio = target.FindProperty("mAspectRatio");
			this.mImageTargetType = target.FindProperty("mImageTargetType");
			this.mWidth = this.mSerializedObject.FindProperty("mWidth");
			this.mHeight = this.mSerializedObject.FindProperty("mHeight");
		}

		public System.Collections.Generic.List<ImageTargetAbstractBehaviour> GetBehaviours()
		{
			System.Collections.Generic.List<ImageTargetAbstractBehaviour> list = new System.Collections.Generic.List<ImageTargetAbstractBehaviour>();
			UnityEngine.Object[] targetObjects = this.mSerializedObject.get_targetObjects();
			for (int i = 0; i < targetObjects.Length; i++)
			{
				UnityEngine.Object @object = targetObjects[i];
				list.Add((ImageTargetAbstractBehaviour)@object);
			}
			return list;
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Vuforia.EditorClasses
{
	public class SerializedCylinderTarget : SerializedDataSetTrackable
	{
		private readonly SerializedProperty mSideLength;

		private readonly SerializedProperty mTopDiameter;

		private readonly SerializedProperty mBottomDiameter;

		private readonly SerializedProperty mTopDiameterRatio;

		private readonly SerializedProperty mBottomDiameterRatio;

		public SerializedProperty SideLengthProperty
		{
			get
			{
				return this.mSideLength;
			}
		}

		public float SideLength
		{
			get
			{
				return this.mSideLength.get_floatValue();
			}
			set
			{
				this.mSideLength.set_floatValue(value);
			}
		}

		public SerializedProperty TopDiameterProperty
		{
			get
			{
				return this.mTopDiameter;
			}
		}

		public float TopDiameter
		{
			get
			{
				return this.mTopDiameter.get_floatValue();
			}
			set
			{
				this.mTopDiameter.set_floatValue(value);
			}
		}

		public SerializedProperty BottomDiameterProperty
		{
			get
			{
				return this.mBottomDiameter;
			}
		}

		public float BottomDiameter
		{
			get
			{
				return this.mBottomDiameter.get_floatValue();
			}
			set
			{
				this.mBottomDiameter.set_floatValue(value);
			}
		}

		public SerializedProperty TopDiameterRatioProperty
		{
			get
			{
				return this.mTopDiameterRatio;
			}
		}

		public float TopDiameterRatio
		{
			get
			{
				return this.mTopDiameterRatio.get_floatValue();
			}
			set
			{
				this.mTopDiameterRatio.set_floatValue(value);
			}
		}

		public SerializedProperty BottomDiameterRatioProperty
		{
			get
			{
				return this.mBottomDiameterRatio;
			}
		}

		public float BottomDiameterRatio
		{
			get
			{
				return this.mBottomDiameterRatio.get_floatValue();
			}
			set
			{
				this.mBottomDiameterRatio.set_floatValue(value);
			}
		}

		public SerializedCylinderTarget(SerializedObject target) : base(target)
		{
			this.mSideLength = this.mSerializedObject.FindProperty("mSideLength");
			this.mTopDiameter = this.mSerializedObject.FindProperty("mTopDiameter");
			this.mBottomDiameter = this.mSerializedObject.FindProperty("mBottomDiameter");
			this.mTopDiameterRatio = this.mSerializedObject.FindProperty("mTopDiameterRatio");
			this.mBottomDiameterRatio = this.mSerializedObject.FindProperty("mBottomDiameterRatio");
		}

		public System.Collections.Generic.List<CylinderTargetAbstractBehaviour> GetBehaviours()
		{
			System.Collections.Generic.List<CylinderTargetAbstractBehaviour> list = new System.Collections.Generic.List<CylinderTargetAbstractBehaviour>();
			UnityEngine.Object[] targetObjects = this.mSerializedObject.get_targetObjects();
			for (int i = 0; i < targetObjects.Length; i++)
			{
				UnityEngine.Object @object = targetObjects[i];
				list.Add((CylinderTargetAbstractBehaviour)@object);
			}
			return list;
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Vuforia.EditorClasses
{
	public class SerializedMultiTarget : SerializedDataSetTrackable
	{
		public SerializedMultiTarget(SerializedObject target) : base(target)
		{
		}

		public System.Collections.Generic.List<MultiTargetAbstractBehaviour> GetBehaviours()
		{
			System.Collections.Generic.List<MultiTargetAbstractBehaviour> list = new System.Collections.Generic.List<MultiTargetAbstractBehaviour>();
			UnityEngine.Object[] targetObjects = this.mSerializedObject.get_targetObjects();
			for (int i = 0; i < targetObjects.Length; i++)
			{
				UnityEngine.Object @object = targetObjects[i];
				list.Add((MultiTargetAbstractBehaviour)@object);
			}
			return list;
		}
	}
}

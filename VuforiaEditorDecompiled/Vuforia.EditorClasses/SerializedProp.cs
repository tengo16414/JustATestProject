using System;
using UnityEditor;
using UnityEngine;

namespace Vuforia.EditorClasses
{
	public class SerializedProp : SerializedSmartTerrainTrackable
	{
		private readonly SerializedProperty mBoxColliderToUpdate;

		public SerializedProperty BoxColliderToUpdateProperty
		{
			get
			{
				return this.mBoxColliderToUpdate;
			}
		}

		public BoxCollider BoxColliderToUpdate
		{
			get
			{
				return (BoxCollider)this.mBoxColliderToUpdate.get_objectReferenceValue();
			}
			set
			{
				this.mBoxColliderToUpdate.set_objectReferenceValue(value);
			}
		}

		public SerializedProp(SerializedObject target) : base(target)
		{
			this.mBoxColliderToUpdate = target.FindProperty("mBoxColliderToUpdate");
		}
	}
}

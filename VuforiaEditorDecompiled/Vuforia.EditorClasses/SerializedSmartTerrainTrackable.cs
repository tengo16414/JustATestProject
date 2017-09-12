using System;
using UnityEditor;
using UnityEngine;

namespace Vuforia.EditorClasses
{
	public class SerializedSmartTerrainTrackable : SerializedTrackable
	{
		private readonly SerializedProperty mMeshFilterToUpdate;

		private readonly SerializedProperty mMeshColliderToUpdate;

		public SerializedProperty MeshFilterToUpdateProperty
		{
			get
			{
				return this.mMeshFilterToUpdate;
			}
		}

		public MeshFilter MeshFilterToUpdate
		{
			get
			{
				return (MeshFilter)this.mMeshFilterToUpdate.get_objectReferenceValue();
			}
			set
			{
				this.mMeshFilterToUpdate.set_objectReferenceValue(value);
			}
		}

		public SerializedProperty MeshColliderToUpdateProperty
		{
			get
			{
				return this.mMeshColliderToUpdate;
			}
		}

		public MeshCollider MeshColliderToUpdate
		{
			get
			{
				return (MeshCollider)this.mMeshColliderToUpdate.get_objectReferenceValue();
			}
			set
			{
				this.mMeshColliderToUpdate.set_objectReferenceValue(value);
			}
		}

		public SerializedSmartTerrainTrackable(SerializedObject target) : base(target)
		{
			this.mMeshFilterToUpdate = target.FindProperty("mMeshFilterToUpdate");
			this.mMeshColliderToUpdate = target.FindProperty("mMeshColliderToUpdate");
		}
	}
}

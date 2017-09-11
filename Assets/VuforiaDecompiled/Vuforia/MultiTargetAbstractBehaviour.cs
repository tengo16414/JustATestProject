using System;
using UnityEngine;

namespace Vuforia
{
	public abstract class MultiTargetAbstractBehaviour : DataSetTrackableBehaviour
	{
		private MultiTarget mMultiTarget;

		public MultiTarget MultiTarget
		{
			get
			{
				return this.mMultiTarget;
			}
		}

		protected override void InternalUnregisterTrackable()
		{
			this.mTrackable = (this.mMultiTarget = null);
		}

		protected override void CalculateDefaultOccluderBounds(out Vector3 boundsMin, out Vector3 boundsMax)
		{
			Transform transform = base.transform.Find("ChildTargets");
			Vector3 zero = new Vector3(3.40282347E+38f, 3.40282347E+38f, 3.40282347E+38f);
			Vector3 zero2 = new Vector3(-3.40282347E+38f, -3.40282347E+38f, -3.40282347E+38f);
			bool flag = false;
			if (transform != null)
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					Transform child = transform.GetChild(i);
					MeshFilter component = child.GetComponent<MeshFilter>();
					if (component != null && component.sharedMesh != null)
					{
						Vector3[] vertices = component.sharedMesh.vertices;
						for (int j = 0; j < vertices.Length; j++)
						{
							Vector3 position = vertices[j];
							Vector3 position2 = child.transform.TransformPoint(position);
							Vector3 vector = base.transform.InverseTransformPoint(position2);
							zero = new Vector3(Mathf.Min(zero.x, vector.x), Mathf.Min(zero.y, vector.y), Mathf.Min(zero.z, vector.z));
							zero2 = new Vector3(Mathf.Max(zero2.x, vector.x), Mathf.Max(zero2.y, vector.y), Mathf.Max(zero2.z, vector.z));
							flag = true;
						}
					}
				}
			}
			if (!flag)
			{
				zero = Vector3.zero;
				zero2 = Vector3.zero;
			}
			Vector3 vector2 = (zero + zero2) / 2f;
			Vector3 vector3 = zero2 - vector2;
			vector3 = new Vector3(vector3.x * 1.1f, vector3.y, vector3.z * 1.1f);
			boundsMin = vector2 - vector3;
			boundsMax = vector2 + vector3;
		}

		protected override void ProtectedSetAsSmartTerrainInitializationTarget(ReconstructionFromTarget reconstructionFromTarget)
		{
			if (this.mMultiTarget != null)
			{
				if (this.mIsSmartTerrainOccluderOffset)
				{
					reconstructionFromTarget.SetInitializationTarget(this.mMultiTarget, this.mSmartTerrainOccluderBoundsMin, this.mSmartTerrainOccluderBoundsMax, this.mSmartTerrainOccluderOffset, this.mSmartTerrainOccluderRotation);
					return;
				}
				reconstructionFromTarget.SetInitializationTarget(this.mMultiTarget, this.mSmartTerrainOccluderBoundsMin, this.mSmartTerrainOccluderBoundsMax);
			}
		}

		internal override bool InitializeTarget(Trackable trackable, bool applyTargetScaleToBehaviour)
		{
			base.InitializeTarget(trackable, applyTargetScaleToBehaviour);
			MultiTargetImpl multiTargetImpl = trackable as MultiTargetImpl;
			if (multiTargetImpl == null)
			{
				return false;
			}
			this.mTrackable = (this.mMultiTarget = multiTargetImpl);
			this.mTrackableName = multiTargetImpl.Name;
			this.mDataSetPath = multiTargetImpl.DataSet.Path;
			if (this.mExtendedTracking)
			{
				this.mMultiTarget.StartExtendedTracking();
			}
			return true;
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effect;
using Pool;

namespace HitBox
{
	public class InGameHitBox : MonoBehaviour
	{
		public GameObject Owner
		{
			get
			{
				return owner;
			}
		}

		public Vector3 HitBoxPos	
		{
			get
			{
				if(hitBoxData is null)
				{
					return transform.position;
				}
				return transform.position + (transform.forward * hitBoxData.offset.z) + (transform.up * hitBoxData.offset.y) + (transform.right * hitBoxData.offset.x);
			}
		}

		public bool IsContactDir
		{
			get
			{
				return isContactDir;
			}
		}

		public HitBoxData HitBoxData
		{
			get
			{
				return hitBoxData;
			}
		}
		private HitBoxInAction HitBoxInAction
		{
			get
			{
				hitBoxInAction ??= GetComponent<HitBoxInAction>();
				return hitBoxInAction;
			}
		}

		private CapsuleCollider col;
		private HitBoxData hitBoxData;
		private HitBoxInAction hitBoxInAction;
		private GameObject owner;
		private ulong index;
		private bool isContactDir;
		private Quaternion rotation;

		public void SetHitBox(ulong _index, HitBoxData _hitBoxData, GameObject _owner, string _tag, GameObject _parent = null, GameObject _swingEffectParent = null)
		{
			index = _index;
			gameObject.tag = _tag;
			owner = _owner;
			col ??= GetComponent<CapsuleCollider>();
			hitBoxData = _hitBoxData;
			isContactDir = hitBoxData.isContactDirection;
			transform.position = _owner.transform.position;
			transform.eulerAngles = _hitBoxData.rotation + _owner.transform.eulerAngles;
			transform.localScale = Vector3.one;
			col.center = _hitBoxData.offset;
			col.radius = _hitBoxData.radius;
			col.height = _hitBoxData.height;
			rotation = _owner.transform.rotation;


			if (hitBoxData.childization)
			{
				if(_parent is null)
				{
					gameObject.transform.SetParent(owner.transform);
				}
				else
				{
					transform.position = _parent.transform.position;
					transform.rotation = _parent.transform.rotation;
					gameObject.transform.SetParent(_parent.transform);
				}
			}
			else
			{
				gameObject.transform.SetParent(null);
			}
			gameObject.SetActive(true);

			Vector3 _pos = transform.position + (transform.forward * hitBoxData.swingEffectOffset.z) + (transform.up * hitBoxData.swingEffectOffset.y) + (transform.right * hitBoxData.swingEffectOffset.x);

			if (hitBoxData.swingEffect != "NULL")
			{
				if (hitBoxData.swingEffectChildization)
				{
					EffectManager.Instance.SetEffectDefault(hitBoxData.swingEffect, _pos, _hitBoxData.swingEffectRotation + transform.eulerAngles, _hitBoxData.swingEffectSize, _swingEffectParent?.transform);
				}
				else
				{
					EffectManager.Instance.SetEffectDefault(hitBoxData.swingEffect, _pos, _hitBoxData.swingEffectRotation + transform.eulerAngles, _hitBoxData.swingEffectSize);

				}
			}

			if(hitBoxData.deleteDelay > -0.5f)
			{
				StartCoroutine(DestroyHitBox());
			}
		}

		public void SetIndex(ulong _index)
		{
			index = _index;
		}

		public ulong GetIndex()
		{
			return index;
		}

		public Quaternion KnockbackDir()
		{
			Quaternion _quaternion = rotation * Quaternion.Euler(hitBoxData.knockbackDir);
			return _quaternion.normalized;
		}

		public float KnockbackPower()
		{
			return hitBoxData.defaultPower;
		}
		private IEnumerator DestroyHitBox()
		{
			yield return new WaitForSeconds(hitBoxData.deleteDelay);
			transform.SetParent(null);
			gameObject.SetActive(false);
			HitBoxPoolManager.Instance.RegisterObject(this);
			//Pool.ObjectPoolManager.Instance.RegisterObject("HitBox", gameObject);
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			//Gizmos.color = Color.green;
			if (hitBoxData == null)
			{
				return;
			}
			//Vector3 _pos = transform.position + (transform.forward * col.center.z) + (transform.up * -col.center.y) + (transform.right * col.center.x);
			//Vector3 _pos = transform.TransformPoint(col.center);
			
			Vector3 center = transform.TransformPoint(col.center);
			float radius = col.radius;
			float height = col.height;
			int direction = col.direction;

			// Draw capsule
			Gizmos.color = Color.green;
			
			if (direction == 0) // X-axis
			{
				Vector3 right = transform.right;
				Vector3 up = transform.up;
				Vector3 forward = transform.forward;
				Gizmos.DrawWireSphere(center + right * (height / 2 - radius), radius);
				Gizmos.DrawWireSphere(center - right * (height / 2 - radius), radius);
				Gizmos.DrawLine(center + right * (height / 2 - radius) + up * radius, center - right * (height / 2 - radius) + up * radius);
				Gizmos.DrawLine(center + right * (height / 2 - radius) - up * radius, center - right * (height / 2 - radius) - up * radius);
				Gizmos.DrawLine(center + right * (height / 2 - radius) + forward * radius, center - right * (height / 2 - radius) + forward * radius);
				Gizmos.DrawLine(center + right * (height / 2 - radius) - forward * radius, center - right * (height / 2 - radius) - forward * radius);
			}
			else if (direction == 1) // Y-axis
			{
				Vector3 right = transform.right;
				Vector3 up = transform.up;
				Vector3 forward = transform.forward;
				Gizmos.DrawWireSphere(center + up * (height / 2 - radius), radius);
				Gizmos.DrawWireSphere(center - up * (height / 2 - radius), radius);
				Gizmos.DrawLine(center + up * (height / 2 - radius) + right * radius, center - up * (height / 2 - radius) + right * radius);
				Gizmos.DrawLine(center + up * (height / 2 - radius) - right * radius, center - up * (height / 2 - radius) - right * radius);
				Gizmos.DrawLine(center + up * (height / 2 - radius) + forward * radius, center - up * (height / 2 - radius) + forward * radius);
				Gizmos.DrawLine(center + up * (height / 2 - radius) - forward * radius, center - up * (height / 2 - radius) - forward * radius);
			}
			else if (direction == 2) // Z-axis
			{
				Vector3 right = transform.right;
				Vector3 up = transform.up;
				Vector3 forward = transform.forward;
				Gizmos.DrawWireSphere(center + forward * (height / 2 - radius), radius);
				Gizmos.DrawWireSphere(center - forward * (height / 2 - radius), radius);
				Gizmos.DrawLine(center + forward * (height / 2 - radius) + up * radius, center - forward * (height / 2 - radius) + up * radius);
				Gizmos.DrawLine(center + forward * (height / 2 - radius) - up * radius, center - forward * (height / 2 - radius) - up * radius);
				Gizmos.DrawLine(center + forward * (height / 2 - radius) + right * radius, center - forward * (height / 2 - radius) + right * radius);
				Gizmos.DrawLine(center + forward * (height / 2 - radius) - right * radius, center - forward * (height / 2 - radius) - right * radius);
			}
			//DrawCapsule( _pos, transform.rotation, hitBoxData.height, hitBoxData.radius, Color.green);
		}

		public void DrawCapsule(Vector3 position, Quaternion orientation, float height, float radius, Color color, bool drawFromBase = true)
		{
			// Clamp the radius to a half of the capsule's height
			radius = Mathf.Clamp(radius, 0, height * 0.5f);
			Vector3 localUp = orientation * Vector3.up;
			Quaternion arcOrientation = orientation * Quaternion.Euler(0, 90, 0);

			Vector3 basePositionOffset = drawFromBase ? Vector3.zero : (localUp * height * 0.5f);
			Vector3 baseArcPosition = position + localUp * radius - basePositionOffset;
			DrawArc(180, 360, baseArcPosition, orientation, radius, color);
			DrawArc(180, 360, baseArcPosition, arcOrientation, radius, color);

			float cylinderHeight = height - radius * 2.0f;
			DrawCylinder(baseArcPosition, orientation, cylinderHeight, radius, color, true);

			Vector3 topArcPosition = baseArcPosition + localUp * cylinderHeight;

			DrawArc(0, 180, topArcPosition, orientation, radius, color);
			DrawArc(0, 180, topArcPosition, arcOrientation, radius, color);
		}

		public void DrawArc(float startAngle, float endAngle, Vector3 position, Quaternion orientation, float radius, Color color, bool drawChord = false, bool drawSector = false, int arcSegments = 32)
		{
			float arcSpan = Mathf.DeltaAngle(startAngle, endAngle);

			// Since Mathf.DeltaAngle returns a signed angle of the shortest path between two angles, it 
			// is necessary to offset it by 360.0 degrees to get a positive value
			if (arcSpan <= 0)
			{
				arcSpan += 360.0f;
			}

			// angle step is calculated by dividing the arc span by number of approximation segments
			float angleStep = (arcSpan / arcSegments) * Mathf.Deg2Rad;
			float stepOffset = startAngle * Mathf.Deg2Rad;

			// stepStart, stepEnd, lineStart and lineEnd variables are declared outside of the following for loop
			float stepStart = 0.0f;
			float stepEnd = 0.0f;
			Vector3 lineStart = Vector3.zero;
			Vector3 lineEnd = Vector3.zero;

			// arcStart and arcEnd need to be stored to be able to draw segment chord
			Vector3 arcStart = Vector3.zero;
			Vector3 arcEnd = Vector3.zero;

			// arcOrigin represents an origin of a circle which defines the arc
			Vector3 arcOrigin = position;

			for (int i = 0; i < arcSegments; i++)
			{
				// Calculate approximation segment start and end, and offset them by start angle
				stepStart = angleStep * i + stepOffset;
				stepEnd = angleStep * (i + 1) + stepOffset;

				lineStart.x = Mathf.Cos(stepStart);
				lineStart.y = Mathf.Sin(stepStart);
				lineStart.z = 0.0f;

				lineEnd.x = Mathf.Cos(stepEnd);
				lineEnd.y = Mathf.Sin(stepEnd);
				lineEnd.z = 0.0f;

				// Results are multiplied so they match the desired radius
				lineStart *= radius;
				lineEnd *= radius;

				// Results are multiplied by the orientation quaternion to rotate them 
				// since this operation is not commutative, result needs to be
				// reassigned, instead of using multiplication assignment operator (*=)
				lineStart = orientation * lineStart;
				lineEnd = orientation * lineEnd;

				// Results are offset by the desired position/origin 
				lineStart += position;
				lineEnd += position;

				// If this is the first iteration, set the chordStart
				if (i == 0)
				{
					arcStart = lineStart;
				}

				// If this is the last iteration, set the chordEnd
				if (i == arcSegments - 1)
				{
					arcEnd = lineEnd;
				}

				Debug.DrawLine(lineStart, lineEnd, color);
			}

			if (drawChord)
			{
				Debug.DrawLine(arcStart, arcEnd, color);
			}
			if (drawSector)
			{
				Debug.DrawLine(arcStart, arcOrigin, color);
				Debug.DrawLine(arcEnd, arcOrigin, color);
			}
		}

		public void DrawCylinder(Vector3 position, Quaternion orientation, float height, float radius, Color color, bool drawFromBase = true)
		{
			Vector3 localUp = orientation * Vector3.up;
			Vector3 localRight = orientation * Vector3.right;
			Vector3 localForward = orientation * Vector3.forward;

			Vector3 basePositionOffset = drawFromBase ? Vector3.zero : (localUp * height * 0.5f);
			Vector3 basePosition = position - basePositionOffset;
			Vector3 topPosition = basePosition + localUp * height;

			Quaternion circleOrientation = orientation * Quaternion.Euler(90, 0, 0);

			Vector3 pointA = basePosition + localRight * radius;
			Vector3 pointB = basePosition + localForward * radius;
			Vector3 pointC = basePosition - localRight * radius;
			Vector3 pointD = basePosition - localForward * radius;

			Debug.DrawRay(pointA, localUp * height, color);
			Debug.DrawRay(pointB, localUp * height, color);
			Debug.DrawRay(pointC, localUp * height, color);
			Debug.DrawRay(pointD, localUp * height, color);

			DrawCircle(basePosition, circleOrientation, radius, 32, color);
			DrawCircle(topPosition, circleOrientation, radius, 32, color);
		}
		public void DrawCircle(Vector3 position, Quaternion rotation, float radius, int segments, Color color)
		{
			// If either radius or number of segments are less or equal to 0, skip drawing
			if (radius <= 0.0f || segments <= 0)
			{
				return;
			}

			// Single segment of the circle covers (360 / number of segments) degrees
			float angleStep = (360.0f / segments);

			// Result is multiplied by Mathf.Deg2Rad constant which transforms degrees to radians
			// which are required by Unity's Mathf class trigonometry methods

			angleStep *= Mathf.Deg2Rad;

			// lineStart and lineEnd variables are declared outside of the following for loop
			Vector3 lineStart = Vector3.zero;
			Vector3 lineEnd = Vector3.zero;

			for (int i = 0; i < segments; i++)
			{
				// Line start is defined as starting angle of the current segment (i)
				lineStart.x = Mathf.Cos(angleStep * i);
				lineStart.y = Mathf.Sin(angleStep * i);
				lineStart.z = 0.0f;

				// Line end is defined by the angle of the next segment (i+1)
				lineEnd.x = Mathf.Cos(angleStep * (i + 1));
				lineEnd.y = Mathf.Sin(angleStep * (i + 1));
				lineEnd.z = 0.0f;

				// Results are multiplied so they match the desired radius
				lineStart *= radius;
				lineEnd *= radius;

				// Results are multiplied by the rotation quaternion to rotate them 
				// since this operation is not commutative, result needs to be
				// reassigned, instead of using multiplication assignment operator (*=)
				lineStart = rotation * lineStart;
				lineEnd = rotation * lineEnd;

				// Results are offset by the desired position/origin 
				lineStart += position;
				lineEnd += position;

				// Points are connected using DrawLine method and using the passed color
				Debug.DrawLine(lineStart, lineEnd, color);
			}
		}
#endif
	}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LockOn
{
	public class LockOnObj : MonoBehaviour
	{
		private LockOnCamera LockOnCamera
		{
			get
			{
				lockOnCamera ??= FindObjectOfType<LockOnCamera>();
				return lockOnCamera;
			}
		}
		[SerializeField]
		private Transform target;

		private LockOnCamera lockOnCamera;

		public void OnBecameVisible()
		{
			LockOnCamera?.AddLockOnObject(target);
		}
		public void OnBecameInvisible()
		{
			LockOnCamera?.RemoveLockOnObject(target);
		}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventObject
{
	public class EventObj_Animation : MonoBehaviour, IEventObj
	{
		public bool IsAlreadyPlay 
		{ 
			get
			{
				return isAlreadyPlay;
			}
			set
			{
				isAlreadyPlay = value;
			}
		}
		public bool IsInfinityPlay 
		{ 
			get
			{
				return isInfinityPlay;
			}
			set
			{
				isInfinityPlay = value;
			}
		}

		[SerializeField]
		private bool isInfinityPlay;
		[SerializeField]
		private bool isAlreadyPlay;

		[SerializeField]
		private Animator animator;

		[SerializeField]
		private string animationKey;

		public void PlayEvent()
		{
			if (isAlreadyPlay)
			{
				return;
			}

			//Process
			animator.enabled = true;
			animator ??= GetComponent<Animator>();
			animator.Play(animationKey);

			if (!isInfinityPlay)
			{
				isAlreadyPlay = true;
			}
		}
	}
}

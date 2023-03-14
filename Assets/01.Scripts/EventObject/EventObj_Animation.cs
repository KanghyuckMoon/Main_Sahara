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

		private bool isInfinityPlay;
		private bool isAlreadyPlay;

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
			animator ??= GetComponent<Animator>();
			animator.Play(animationKey);

			if (!isInfinityPlay)
			{
				isAlreadyPlay = true;
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using EventObject;
using UnityEngine;
using UnityEngine.Events;

public class EventObj_UnityEvent : MonoBehaviour, IEventObj
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

		public bool IsInfinityPlay { get; set; }

		[SerializeField]
		private bool isAlreadyPlay;

		[SerializeField]
		private UnityEvent animatorEvent;

		[SerializeField]
		private string animationKey;

		public void PlayEvent()
		{
			if (isAlreadyPlay)
			{
				return;
			}

			animatorEvent?.Invoke();
		}
}

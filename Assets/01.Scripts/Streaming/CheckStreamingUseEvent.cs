using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Streaming
{
	public class CheckStreamingUseEvent : MonoBehaviour
	{
		[SerializeField]
		private UnityEvent falseEvent;
		[SerializeField]
		private UnityEvent trueEvent;

		private void Start()
		{
			if(Streaming.StreamingManager.Instance.IsSetting)
			{
				trueEvent?.Invoke();
			}
			else
			{
				falseEvent?.Invoke();
			}
		}
	}
}

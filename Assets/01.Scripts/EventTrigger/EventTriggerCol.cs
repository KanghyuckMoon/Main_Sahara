using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventTrigger
{
	public class EventTriggerCol : MonoBehaviour
	{
		[SerializeField]
		private string message;

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				EventTriggerManager.Instance.EventCall(message);
			}
		}
	}
}

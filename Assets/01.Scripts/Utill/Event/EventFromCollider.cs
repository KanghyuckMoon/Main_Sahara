using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventFromCollider : MonoBehaviour
{
	public UnityEvent colEvent;
	public bool isOnlyOne;
	private bool isPlay;

	public void OnTriggerEnter(Collider other)
	{
		if (isPlay)
		{
			return;
		}
		if (other.CompareTag("Player"))
		{
			if(isOnlyOne)
			{
				isPlay = true;
			}
			colEvent?.Invoke();
		}
	}
}

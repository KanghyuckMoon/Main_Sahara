using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeManager
{
	public class TimeChangeEvent : MonoBehaviour
	{
		public float changeTime;

		public void ChangeTime()
		{
			StaticTime.EntierTime = changeTime;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using TimeManager;
using UnityEngine;
using Utill.Pattern;

namespace Attack
{
	public class TimeSlowManager : MonoSingleton<TimeSlowManager>
	{
		public float SlowTime
		{
			get
			{
				return slowTime;
			}
			set
			{
				slowTime = value;
			}
		}

		private float slowTime;

		private void Start()
		{
			StartCoroutine(AttackFeedBack_TimeSlow());
		}
		IEnumerator AttackFeedBack_TimeSlow()
		{
			bool _isTimeSlow = false;
			while(true)
			{
				slowTime -= Time.deltaTime;
				if(slowTime > 0f)
				{
					StaticTime.EntierTime = 0.2f;
					_isTimeSlow = true;
				}
				else if(_isTimeSlow)
				{
					StaticTime.EntierTime = 1;
					_isTimeSlow = false;
				}
				yield return null;
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Effect
{
	public class ImpulseEffect : MonoBehaviour
	{

		[SerializeField]
		private bool isActive = false;
		float LastEventTime = 0;
	
		private CinemachineImpulseSource cinemachineImpulse;

		public void OnEnable()
		{
			cinemachineImpulse ??= GetComponent<CinemachineImpulseSource>();
			cinemachineImpulse.GenerateImpulse();
			var now = Time.time;
			LastEventTime = now;
		}
 
    void Update()
    {
	    if (!isActive)
	    {
		    return;
	    }
        var now = Time.time;
        float eventLength = cinemachineImpulse.m_ImpulseDefinition.m_TimeEnvelope.m_AttackTime +  cinemachineImpulse.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime;
        if (now - LastEventTime > eventLength)
        {
	        cinemachineImpulse.m_ImpulseDefinition.CreateEvent(transform.position, cinemachineImpulse.m_DefaultVelocity);
            LastEventTime = now;
        }
    }
	}
}


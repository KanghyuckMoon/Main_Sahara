using UnityEngine;
using Cinemachine;

public class ContinuousImpulse : MonoBehaviour
{
    [CinemachineImpulseDefinitionProperty]
    public CinemachineImpulseDefinition ImpulseDefinition = new CinemachineImpulseDefinition();
 
    float LastEventTime = 0;
 
    void Update()
    {
        var now = Time.time;
        float eventLength = ImpulseDefinition.m_TimeEnvelope.m_AttackTime + ImpulseDefinition.m_TimeEnvelope.m_SustainTime;
        if (now - LastEventTime > eventLength)
        {
            ImpulseDefinition.CreateEvent(transform.position, Vector3.down);
            LastEventTime = now;
        }
    }
}

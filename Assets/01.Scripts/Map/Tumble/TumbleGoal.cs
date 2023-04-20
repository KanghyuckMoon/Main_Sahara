using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TumbleGoal : MonoBehaviour
{
    [SerializeField] private UnityEvent unityEvent; 
    
    private bool isGoal = false;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (isGoal)
        {
            return;
        }
        TumbleWeed tumble;
        if (other.TryGetComponent<TumbleWeed>(out tumble))
        {
            unityEvent?.Invoke();
            isGoal = true;
        }
    }
}

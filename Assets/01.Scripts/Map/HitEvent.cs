using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collider> colEvent;

    private void OnTriggerEnter(Collider other)
    {
        colEvent?.Invoke(other);
    }
}

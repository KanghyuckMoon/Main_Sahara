using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TumbleWeed : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collider> colEvent;
    
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.left * 1f, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        colEvent?.Invoke(other);
    }
}

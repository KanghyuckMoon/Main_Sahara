using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [Header("추적할 오브젝트")]
    [SerializeField] private Transform trackObject;

    [Header("추적할 오브젝트와의 거리")]
    [SerializeField] private Vector3 trackingDistance;

    private void Update()
    {
        transform.position = trackObject.position + trackingDistance;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [Header("������ ������Ʈ")]
    [SerializeField] private Transform trackObject;

    [Header("������ ������Ʈ���� �Ÿ�")]
    [SerializeField] private Vector3 trackingDistance;

    private void Update()
    {
        transform.position = trackObject.position + trackingDistance;
    }
}

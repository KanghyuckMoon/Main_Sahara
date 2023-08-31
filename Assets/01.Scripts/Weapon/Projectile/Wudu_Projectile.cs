using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Wudu_Projectile : MonoBehaviour
{
    private GameObject subject;
    private float radius;
    private float speed;
    private bool isOn;
    
    private float runningTime = 0;
    
    private void Update()
    {
        if (!isOn) return;
        //transform.RotateAround(subject.transform.position, Vector3.down, 5 * Time.deltaTime);
        AroundObject();
        //transform.
    }

    private void AroundObject()
    {
        runningTime += Time.deltaTime * speed;
        float x = radius * Mathf.Cos(runningTime);
        float y = radius * Mathf.Sin(runningTime);
        transform.position = subject.transform.position + new Vector3(x , 1, y);
        //this.transform.position = newPos;
    }

    public void SetSubject(GameObject _gameObject, float _radius, float _speed)
    {
        subject = _gameObject;
        radius = _radius;
        speed = _speed;
        isOn = true;

        transform.DOScale(3.5f, 0.5f);
    }
}

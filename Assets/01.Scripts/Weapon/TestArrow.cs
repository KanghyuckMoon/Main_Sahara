using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Weapon
{
    public class TestArrow : ProjectileObject, IProjectile
    {
        [SerializeField]
        private Transform model;

        [SerializeField] private TrailRenderer trailRenderer;
        public Rigidbody rigidbody;

        public bool usingGravity;

        private Quaternion quaternion;
        private bool isFly = false;

        [SerializeField] private UnityEvent shotEvent;

        //public void SetPosition()
        //{

        //}
        public void Update()
        {
            if (isFly)
            { 
                //+ Vector3.down
                quaternion = Quaternion.LookRotation(rigidbody.velocity.normalized);
                transform.rotation = quaternion;
            }
        }

        protected override void OnEnable()
        {
            trailRenderer.enabled = false;
            model.localEulerAngles = new Vector3(180,0,0);
            isFly = false;
            rigidbody.velocity = Vector3.zero;
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
        }

        public void MovingFunc(Vector3 _vector3)
        {
            trailRenderer.Clear();
            trailRenderer.enabled = true;
            shotEvent?.Invoke();
            Invoke("PoolObject", 5f);

            rigidbody.isKinematic = false;
            rigidbody.useGravity = usingGravity;
            rigidbody.velocity = Vector3.zero;
            //transform.rotation = Quaternion.Euler(objectData.InitialDirection);

            isFly = true;

            transform.SetParent(null);
            Vector3 _rot = (CalculateRotation(_vector3).normalized * objectData.speed);// + new Vector3(0, 1, 0);
            rigidbody.AddForce(_rot, ForceMode.Impulse);
            model.localEulerAngles = new Vector3(180,90,-90);
            quaternion = Quaternion.LookRotation(_rot);
            transform.rotation = quaternion;
            //model.LookAt(_rot + Vector3.forward);

            //rigidbody.MovePosition(Vector3.up * 10);
        }

        //private Vector3 CalculateRotation()
        //{
        //    Quaternion _rotation = transform.rotation;
        //    Vector3 _vec = _rotation.eulerAngles;

        //    return _vec + objectData.InitialDirection.normalized;
        //}
    }
}
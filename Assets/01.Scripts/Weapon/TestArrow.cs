using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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


        //public void SetPosition()
        //{

        //}
        public void Update()
        {
            if (isFly)
            { 
                //+ Vector3.down
                quaternion = Quaternion.LookRotation(rigidbody.velocity.normalized);
                //transform.rotation = quaternion;
            }
        }

        protected override void OnEnable()
        {
            rigidbody.velocity = Vector3.zero;
        }

        public void MovingFunc(Vector3 _vector3)
        {
            Invoke("PoolObject", 5f);
            rigidbody.velocity = Vector3.zero;
            rigidbody.useGravity = usingGravity;
            //transform.rotation = Quaternion.Euler(objectData.InitialDirection);

            isFly = true;

            transform.SetParent(null);
            Vector3 _rot = (CalculateRotation(_vector3).normalized * objectData.speed);// + new Vector3(0, 1, 0);
            rigidbody.AddForce(_rot, ForceMode.Impulse);
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
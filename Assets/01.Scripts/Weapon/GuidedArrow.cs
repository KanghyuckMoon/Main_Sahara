using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Weapon
{
    public class GuidedArrow : ProjectileObject, IProjectile
    {
        [SerializeField]
        private Transform model;
        public Rigidbody rigidbody;

        public bool usingGravity;

        private Quaternion quaternion;
        private bool isFly = false;
        
        public void Update()
        {
            if (isFly)
            {
                quaternion = Quaternion.LookRotation(rigidbody.velocity.normalized + Vector3.down);
                transform.rotation = quaternion;
            }
        }

        public void MovingFunc(Vector3 _vector3)
        {
            rigidbody.useGravity = usingGravity;

            isFly = true;

            transform.SetParent(null);
            Vector3 _rot = (CalculateRotation(_vector3).normalized * objectData.speed);// + new Vector3(0, 1, 0);
            rigidbody.AddForce(_rot, ForceMode.Impulse);
            model.LookAt(_rot + Vector3.forward);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (gameObject.CompareTag("Player"))
            {
                if (other.CompareTag("Enemy"))
                {
                    float _power = rigidbody.velocity.magnitude;
                    Vector3 _dir = other.transform.position - transform.position;
                    rigidbody.velocity = _dir.normalized* _power;
                }
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                if (other.CompareTag("Player"))
                {
                    float _power = rigidbody.velocity.magnitude;
                    Vector3 _dir = other.transform.position - transform.position;
                    rigidbody.velocity = _dir.normalized* _power;
                }
            }
        }
    }
}
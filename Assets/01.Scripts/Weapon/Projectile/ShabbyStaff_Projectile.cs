using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class ShabbyStaff_Projectile : ProjectileObject, IProjectile
    {
        public Rigidbody rigidbody;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void MovingFunc(Vector3 _vector3)
        {
            transform.SetParent(null);
            //rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
            rigidbody.AddForce(CalculateRotation(_vector3).normalized * objectData.speed, ForceMode.Impulse);
        }
    }
}
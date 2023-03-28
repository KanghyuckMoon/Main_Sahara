using System;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands;
using UnityEngine;

namespace Weapon
{
    public class Tantra_Projectile : ProjectileObject, IProjectile
    {
        public Rigidbody rigidbody;

        [SerializeField, Header("중력의 영향을 받는가?")]
        private bool affectedByGravity;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void MovingFunc(Vector3 _vector3)
        {
            rigidbody.useGravity = affectedByGravity;
            transform.SetParent(null);
            //rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
            Vector3 _calcVector = CalculateRotation(_vector3).normalized;
            transform.LookAt(transform.position + _calcVector);
            rigidbody.AddForce(_calcVector * objectData.speed, ForceMode.Impulse);
        }
    }
}
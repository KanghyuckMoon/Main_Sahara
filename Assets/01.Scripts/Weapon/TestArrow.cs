using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Weapon
{
    public class TestArrow : ProjectileObject, IProjectile
    {
        public Rigidbody rigidbody;

        //private void Start()
        //{
        //    rigidbody = GetComponent<Rigidbody>();
        //}

        public void MovingFunc()
        {
            rigidbody.useGravity = true;

            transform.SetParent(null);
            rigidbody.AddForce(transform.position + new Vector3(0, 0, 10), ForceMode.Impulse);
        }
    }
}
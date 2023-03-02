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

        //private void FixedUpdate()
        //{
            
        //}

        public void MovingFunc(Vector3 _pos)
        {
            rigidbody.useGravity = true;

            transform.SetParent(null);
            rigidbody.AddForce((_pos * 35) + new Vector3(0, 1, 0), ForceMode.Impulse);
            //rigidbody.MovePosition(Vector3.up * 10);
        }
    }
}
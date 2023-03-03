using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Weapon
{
    public class TestArrow : ProjectileObject, IProjectile
    {
        public Rigidbody rigidbody;

        //public void SetPosition()
        //{

        //}

        public void MovingFunc()
        {
            rigidbody.useGravity = true;

            transform.rotation = Quaternion.Euler(objectData.position);

            transform.SetParent(null);
            rigidbody.AddForce((objectData.position * objectData.speed) + new Vector3(0, 1, 0), ForceMode.Impulse);
            //rigidbody.MovePosition(Vector3.up * 10);
        }
    }
}
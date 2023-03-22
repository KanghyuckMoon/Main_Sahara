using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Weapon
{
    public class TestArrow : ProjectileObject, IProjectile
    {
        [SerializeField]
        private Transform model;
        public Rigidbody rigidbody;

        //public void SetPosition()
        //{

        //}

        public void MovingFunc(Vector3 _vector3)
        {
            //rigidbody.useGravity = true;

            //transform.rotation = Quaternion.Euler(objectData.InitialDirection);

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
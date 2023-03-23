using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class Geomtry_Test : ProjectileObject, IProjectile
    {
        public Rigidbody rigidbody;

        public void MovingFunc(Vector3 _vector3)
        {
            transform.SetParent(null);
            rigidbody.AddForce(CalculateRotation(_vector3).normalized * objectData.speed, ForceMode.Impulse);
        }
    }
}
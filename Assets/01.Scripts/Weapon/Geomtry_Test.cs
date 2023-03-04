using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class Geomtry_Test : ProjectileObject, IProjectile
    {
        public Rigidbody rigidbody;

        public void MovingFunc(Quaternion _quaternion)
        {
            transform.SetParent(null);
            rigidbody.AddForce(CalculateRotation(_quaternion).normalized * objectData.speed, ForceMode.Impulse);
        }
    }
}
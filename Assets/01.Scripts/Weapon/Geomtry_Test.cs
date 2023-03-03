using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class Geomtry_Test : ProjectileObject, IProjectile
    {
        public Rigidbody rigidbody;

        public void MovingFunc()
        {
            transform.SetParent(null);
            rigidbody.AddForce(objectData.position * objectData.speed, ForceMode.Impulse);
        }
    }
}
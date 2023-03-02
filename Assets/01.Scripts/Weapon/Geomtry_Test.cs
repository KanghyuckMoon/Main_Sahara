using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class Geomtry_Test : ProjectileObject, IProjectile
    {
        public Rigidbody rigidbody;
        public void MovingFunc(Vector3 _pos)
        {
            transform.SetParent(null);
            rigidbody.AddForce((_pos * 1) + new Vector3(0, 1, 0), ForceMode.Impulse);
        }
    }
}
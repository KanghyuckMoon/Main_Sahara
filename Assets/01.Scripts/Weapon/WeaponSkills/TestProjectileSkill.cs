using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Weapon
{
    public class TestProjectileSkill : ProjectileObject, IProjectile
    {
        public Rigidbody rigidbody;

        public void MovingFunc(Quaternion _quaternion)
        {
            transform.SetParent(null);
            //rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
            rigidbody.AddForce(CalculateRotation(_quaternion).normalized * objectData.speed, ForceMode.Impulse);
        }
    }
}
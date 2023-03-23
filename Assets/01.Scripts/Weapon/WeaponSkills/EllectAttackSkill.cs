using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Weapon
{
    public class EllectAttackSkill : ProjectileObject, IProjectile
    {
        private Transform player;
        public Rigidbody rigidbody;

        public void MovingFunc(Vector3 _vector3)
        {
            transform.SetParent(null);
            player ??= GameObject.FindGameObjectWithTag("Player").transform;
            Vector3 _dir = player.position - transform.position;
            rigidbody.AddForce(_dir.normalized * objectData.speed, ForceMode.Impulse);
        }
    }
}
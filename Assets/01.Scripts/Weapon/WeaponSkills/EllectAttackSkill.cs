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
            Vector3 _dir = (player.position + new Vector3(0,0.5f,0)) - transform.position;
            rigidbody.AddForce(_dir.normalized * objectData.speed, ForceMode.Impulse);
        }
    }
}
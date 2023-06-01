using System;
using System.Collections;
using System.Collections.Generic;
using TimeManager;
using UnityEngine;

namespace Module
{
    public class FollowEnemy : MonoBehaviour
    {
        [SerializeField] private float followRotationSpeed = 1f;
        [SerializeField] private Rigidbody rigid;
        [SerializeField] private float addHeight = 0.5f;
        private bool isTargetting;
        private Transform target;
        
        private void OnEnable()
        {
            target = null;
            isTargetting = false;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (isTargetting)
            {
                return;
            }
            
            if (gameObject.CompareTag("Player_Weapon") || gameObject.CompareTag("PlayerSkill"))
            {
                if (other.gameObject.CompareTag("Enemy"))
                {
                    target = other.transform;
                    isTargetting = true;
                }
            }
            else if (gameObject.CompareTag("EnemyWeapon") || gameObject.CompareTag("EnemySkill"))
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    target = other.transform;
                    isTargetting = true;
                }
            }
        }

        private void Update()
        {
            if (isTargetting && target != null)
            {
                Vector3 dir = target.position - transform.position + Vector3.up * addHeight;
                Vector3 newVelocity = Vector3.Lerp(rigid.velocity, dir.normalized * rigid.velocity.magnitude, StaticTime.PhysicsDeltaTime * followRotationSpeed);
                rigid.velocity = newVelocity;
            }
        }
    }
}

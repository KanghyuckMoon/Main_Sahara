using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Effect;

namespace Weapon
{
    public class PlatinumStaff_Projectile : ProjectileObject, IProjectile
    {
        public Rigidbody rigidbody;

        [SerializeField, Header("중력의 영향을 받는가?")]
        private bool affectedByGravity;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void MovingFunc(Vector3 _vector3)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.useGravity = affectedByGravity;
            transform.SetParent(null);
            //rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
            Vector3 _calcVector = CalculateRotation(_vector3).normalized;
            transform.LookAt(transform.position + _calcVector);
            rigidbody.AddForce(_calcVector * objectData.speed, ForceMode.Impulse);
        }
        [SerializeField] 
        private string address;

        [SerializeField] 
        private string hitEffectAddress;
        private void OnTriggerEnter(Collider other)
        {
            if (gameObject.CompareTag("Player_Weapon"))
            {
                if (other.gameObject.CompareTag("Enemy"))
                {
                    ObjectPoolManager.Instance.RegisterObject(address, gameObject);
                    gameObject.SetActive(false);
                    EffectManager.Instance.SetEffectDefault(hitEffectAddress, transform.position, Quaternion.identity);
                    return;
                }
            }
            
            if (gameObject.CompareTag("EnemyWeapon"))
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    ObjectPoolManager.Instance.RegisterObject(address, gameObject);
                    gameObject.SetActive(false);
                    EffectManager.Instance.SetEffectDefault(hitEffectAddress, transform.position, Quaternion.identity);
                    return;
                }
            }

            if (other.gameObject.CompareTag("Ground"))
            {
                ObjectPoolManager.Instance.RegisterObject(address, gameObject);
                gameObject.SetActive(false);
                EffectManager.Instance.SetEffectDefault(hitEffectAddress, transform.position, Quaternion.identity);
                return;
            }
        }
    }
}
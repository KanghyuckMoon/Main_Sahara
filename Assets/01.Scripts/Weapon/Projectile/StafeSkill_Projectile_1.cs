using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Effect;

namespace Weapon
{
    public class StafeSkill_Projectile_1 : ProjectileObject, IProjectile
    {
        public Rigidbody rigidbody = null;

        private SphereCollider col = null;

        [SerializeField, Header("중력의 영향을 받는가?")]
        private bool affectedByGravity;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            //col = GetComponent<SphereCollider>();
            if (col is not null) col.enabled = false;
        }

        private void OnEnable()
        {
            if (rigidbody == null)
                rigidbody = GetComponent<Rigidbody>();
            //col ??= GetComponent<SphereCollider>();
            if (rigidbody is not null) rigidbody.velocity = Vector3.zero;
            if (col is not null) col.enabled = false;
        }

        public void MovingFunc(Vector3 _vector3)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.useGravity = affectedByGravity;
            transform.SetParent(null);
            //rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
            var _calcVector = CalculateRotation(_vector3).normalized;
            transform.LookAt(transform.position + _calcVector);
            rigidbody.AddForce(_calcVector * objectData.speed, ForceMode.Impulse);
            //col.enabled = true;

            //StartCoroutine(PoolItem());
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
                    //EffectManager.Instance.SetEffectDefault(hitEffectAddress, transform.position, Quaternion.identity);
                    return;
                }
            }
            
            /*if (gameObject.CompareTag("EnemyWeapon"))
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    ObjectPoolManager.Instance.RegisterObject(address, gameObject);
                    gameObject.SetActive(false);
                    EffectManager.Instance.SetEffectDefault(hitEffectAddress, transform.position, Quaternion.identity);
                    return;
                }
            }*/

            /*if (other.gameObject.CompareTag("Ground"))
            {
                ObjectPoolManager.Instance.RegisterObject(address, gameObject);
                gameObject.SetActive(false);
                //EffectManager.Instance.SetEffectDefault(hitEffectAddress, transform.position, Quaternion.identity);
                return;
            }*/
        }


        private void OnDisable()
        {
            StopCoroutine(nameof(PoolItem));
        }

        IEnumerator PoolItem()
        {
            yield return new WaitForSeconds(17f);
            ObjectPoolManager.Instance.RegisterObject(address, gameObject);
            gameObject.SetActive(false);
            //EffectManager.Instance.SetEffectDefault(hitEffectAddress, transform.position, Quaternion.identity);
            
        }
    }
}
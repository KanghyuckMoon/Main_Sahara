using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Utill.Pattern;
using Effect;

namespace Spawner
{
    
    public class EventSpawn : MonoBehaviour
    {
        [SerializeField]
        private string enemyAddress;

        [SerializeField] private string effectAddress;
        
        [SerializeField] private Transform spawnTrm;

        [SerializeField] private bool isJumping;
        
        public void Spawn()
        {
            GameObject obj = ObjectPoolManager.Instance.GetObject(enemyAddress);
            EffectManager.Instance.SetEffectDefault(effectAddress, transform.position, Quaternion.identity);
            obj.transform.position = spawnTrm.position;
            obj.SetActive(true);

            if (isJumping)
            {
                obj.GetComponent<Rigidbody>().AddForce(Vector3.up * 3, ForceMode.Impulse);
            }
        }
    }
}

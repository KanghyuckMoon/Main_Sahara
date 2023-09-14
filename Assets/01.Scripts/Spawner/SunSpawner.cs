using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using Pool;
using Streaming;
using Effect;
using Module;

public class SunSpawner : MonoBehaviour
{
		[SerializeField]
		private string enemyAddress;
		[SerializeField]
		private ObjectDataSO objectDataSO;
		[SerializeField]
        private Transform spawnPosition;
        [SerializeField]
        private string objAddress;
        [SerializeField] 
        private string spawnEffectAddress;
        [SerializeField] 
        private string sunspawnEffectAddress;

        [SerializeField, Header("KnockBack")] 
        private Vector3 knockBackDir = Vector3.up;

        [SerializeField] 
        private float knockBackForce = 10f;
        
        private void OnTriggerEnter(Collider other)
        {
	        if(other.gameObject.CompareTag("Player"))
	        {
		        Spawn();
		        Pool();
	        }
        }

        public void Spawn()
		{
			EffectManager.Instance.SetEffectDefault(spawnEffectAddress, spawnPosition.position, Quaternion.identity);
			EffectManager.Instance.SetEffectDefault(sunspawnEffectAddress, transform.position, Quaternion.identity);
            GameObject obj = ObjectPoolManager.Instance.GetObject(enemyAddress);
            ObjectClassCycle objectClassCycle = obj.GetComponentInChildren<ObjectClassCycle>();
            objectClassCycle.TargetObject = obj;
            ObjectSceneChecker _objectSceneChecker = ClassPoolManager.Instance.GetClass<ObjectSceneChecker>();
            if (_objectSceneChecker is null)
            {
            	_objectSceneChecker = new ObjectSceneChecker();
            }
            _objectSceneChecker.ObjectDataSO = objectDataSO;
            _objectSceneChecker.ObjectClassCycle = objectClassCycle;
            objectClassCycle.AddObjectClass(_objectSceneChecker);
            obj.transform.position = spawnPosition.position;
            var _module = obj.GetComponent<AbMainModule>();
            _module.knockBackVector = knockBackDir;
            _module.knockBackPower = knockBackForce;
            obj.SetActive(true);
		}
		
		private void Pool()
		{
            ObjectPoolManager.Instance.RegisterObject(objAddress, gameObject);
            gameObject.SetActive(false);
		}
}

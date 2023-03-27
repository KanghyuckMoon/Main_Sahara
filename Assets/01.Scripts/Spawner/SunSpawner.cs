using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;

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
        
        private void OnTriggerEnter(Collision col)
        {
            if(col.gameObject.CompareTag("Player"))
            {
                Spawn();
                Pool();
            }
        }

		public void Spawn()
		{
            GameObject obj = ObjectPoolManager.Instance.GetObject(enemyAddress);
            ObjectClassCycle objectClassCycle = obj.GetComponentInChildren<ObjectClassCycle>();
            objectClassCycle.TargetObject = obj;
            ObjectSceneChecker _objectSceneChecker = ClassPoolManager.Instance.GetClass<ObjectSceneChecker>("ObjectSceneChecker");
            if (_objectSceneChecker is null)
            {
            	_objectSceneChecker = new ObjectSceneChecker();
            }
            _objectSceneChecker.ObjectDataSO = objectDataSO;
            _objectSceneChecker.ObjectClassCycle = objectClassCycle;
            objectClassCycle.AddObjectClass(_objectSceneChecker);
            obj.transform.position = spawnPosition.position;
            obj.SetActive(true);
		}
		
		private void Pool()
		{
            ObjectPoolManager.Instance.RegisterObject(objAddress, gameObject)  ;
		}
}

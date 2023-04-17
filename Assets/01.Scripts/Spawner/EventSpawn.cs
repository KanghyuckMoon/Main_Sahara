using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Utill.Pattern;

namespace Spawner
{
    
    public class EventSpawn : MonoBehaviour
    {
        [SerializeField]
        private string enemyAddress;
        //[SerializeField]
        //private ObjectDataSO objectDataSO;
        
        public void Spawn()
        {
            GameObject obj = ObjectPoolManager.Instance.GetObject(enemyAddress);
            //ObjectClassCycle objectClassCycle = obj.GetComponentInChildren<ObjectClassCycle>();
            //objectClassCycle.TargetObject = obj;
            //ObjectSceneChecker _objectSceneChecker = ClassPoolManager.Instance.GetClass<ObjectSceneChecker>("ObjectSceneChecker");
            //if (_objectSceneChecker is null)
            //{
            //    _objectSceneChecker = new ObjectSceneChecker();
            //}
            //_objectSceneChecker.ObjectDataSO = objectDataSO;
            //_objectSceneChecker.ObjectClassCycle = objectClassCycle;
            //objectClassCycle.AddObjectClass(_objectSceneChecker);
            obj.transform.position = transform.position;
            obj.SetActive(true);
        }
    }
}

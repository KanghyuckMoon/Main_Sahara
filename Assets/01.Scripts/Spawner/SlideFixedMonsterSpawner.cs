using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Streaming;
using Module;
using Effect;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif


namespace Spawner
{
    public class SlideFixedMonsterSpawner : MonoBehaviour
    {
        private static Dictionary<string, bool> isSpawnDic = new Dictionary<string, bool>();
        private static int nameKey;
        [SerializeField] 
        private string key;
        
        //몬스터SO 혹은 몬스터 어드레스
        [SerializeField]
        private string enemyAddress;
        [SerializeField]
        private ObjectDataSO objectDataSO;
        [SerializeField]
        private string spawnEffectAddress;

        private bool isGetOut = false;
        
        #if UNITY_EDITOR
        [ContextMenu("RandomName")]
        public void RandomName()
        {
            //var _prefeb = UnityEditor.PrefabUtility.GetCorrespondingObjectFromSource(gameObject);
            //gameObject.name = _prefeb.name + nameKey++;
            key = gameObject.name + nameKey++;

            EditorSceneManager.MarkSceneDirty(gameObject.scene);
        }
        #endif
        
        private void Start()
        {
            if (isSpawnDic.ContainsKey(key))
            {
                isGetOut = true;
                Spawn();
            }
        }
        
        //유니티 이벤트에서 사용
        public void SetGetOut(bool isGetOut)
        {
            this.isGetOut = isGetOut;
            if (isGetOut)
            {
                if (!isSpawnDic.ContainsKey(key))
                {
                    isSpawnDic.Add(key, true);
                }
                Spawn();
            }
        }

        //RandomMonsterSpawner의 소환 함수
        public void Spawn()
        {
            Vector3 _spawnPos = transform.position;
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
			
            obj.transform.position = _spawnPos;
            var _module = obj.GetComponent<AbMainModule>();
            _module.attackedTime = 0f;
            _module.knockBackVector = Vector3.up;
            _module.knockBackPower = 15;
            EffectManager.Instance.SetEffectDefault(spawnEffectAddress, _spawnPos, Quaternion.identity);
			
            obj.SetActive(true);
        }
        
        //Start함수를 만든다.
        //Start함수에 소환 함수를 넣는다.
        
        
        
    }   
}

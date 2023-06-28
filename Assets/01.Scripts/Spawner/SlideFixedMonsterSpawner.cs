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
        
        //����SO Ȥ�� ���� ��巹��
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
        
        //����Ƽ �̺�Ʈ���� ���
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

        //RandomMonsterSpawner�� ��ȯ �Լ�
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
        
        //Start�Լ��� �����.
        //Start�Լ��� ��ȯ �Լ��� �ִ´�.
        
        
        
    }   
}

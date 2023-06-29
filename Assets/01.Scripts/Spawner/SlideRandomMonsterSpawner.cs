using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Streaming;
using Module;
using Effect;
using Utill.Random;
using Detect;
using Random = UnityEngine.Random;
using UnityEngine.Events;

namespace Spawner
{
    public class SlideRandomMonsterSpawner : MonoBehaviour, IReturnFunction
    {
        //풀 관련
        //퍼센트 배열
        [SerializeField]
        private float[] percentArray;
        //랜덤몬스터SO 배열
        [SerializeField]
        private RandomMonsterListSO[] monsterListSOList;
        
        [SerializeField]
        private string spawnEffectAddress;

        
        //함수 만들고 이걸 슬라이드 디텍트의 이벤트로 넘겨준다.
        public void SpawnRandomMonsterFromRandomListSO()
        {
            int _index = StaticRandom.Choose(percentArray);
            RandomAndChoiceSpawn(monsterListSOList[_index]);
        }
        
        
        private void RandomAndChoiceSpawn(RandomMonsterListSO _randomMonsterListSO)
        {
            int _randomRange = Random.Range(_randomMonsterListSO.minSpawnCount, _randomMonsterListSO.maxSpawnCount + 1);
            for(;_randomRange-- > 0 ; )
            {
                int _index = StaticRandom.Choose(_randomMonsterListSO.randomPercentArr);
                StartCoroutine(RandomSpawn(_randomMonsterListSO.spawnMonsterDataArr[_index]));
            }
        }

        private IEnumerator RandomSpawn(RandomMonsterData _randomMonsterData)
        {
            int _randomRange = Random.Range(_randomMonsterData.minSpawnCount, _randomMonsterData.maxSpawnCount + 1);
            for(; _randomRange-- > 0; )
            {
                yield return new WaitForSeconds(1.5f);
                Spawn(_randomMonsterData);
            }
        }
        private void Spawn(RandomMonsterData _randomMonsterData)
        {
            Vector3 _spawnPos = transform.position;
            GameObject obj = ObjectPoolManager.Instance.GetObject(_randomMonsterData.enemyAddress);
            ObjectClassCycle objectClassCycle = obj.GetComponentInChildren<ObjectClassCycle>();
            objectClassCycle.TargetObject = obj;
            ObjectSceneChecker _objectSceneChecker = ClassPoolManager.Instance.GetClass<ObjectSceneChecker>();
            if (_objectSceneChecker is null)
            {
                _objectSceneChecker = new ObjectSceneChecker();
            }
            _objectSceneChecker.ObjectDataSO = _randomMonsterData.objectDataSO;
            _objectSceneChecker.ObjectClassCycle = objectClassCycle;
            objectClassCycle.AddObjectClass(_objectSceneChecker);
            
            obj.transform.position = _spawnPos;
            var _module = obj.GetComponent<AbMainModule>();
            _module.attackedTime = 0f;
            _module.knockBackVector = new Vector3(Random.Range(-0.5f, 0.5f),1,Random.Range(-0.5f, 0.5f));
            _module.knockBackPower = 15;
            EffectManager.Instance.SetEffectDefault(spawnEffectAddress, _spawnPos, Quaternion.identity);
			
            obj.SetActive(true);
        }

        public UnityAction ReturnFunction()
        {
            return SpawnRandomMonsterFromRandomListSO;
        }
    }
}


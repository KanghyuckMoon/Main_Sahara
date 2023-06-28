using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Streaming;
using Module;
using Effect;
using Utill.Random;

namespace Spawner
{
    public class SlideRandomMonsterSpawner : MonoBehaviour
    {
        //Ǯ ����
        //�ۼ�Ʈ �迭
        [SerializeField]
        private float[] percentArray;
        //��������SO �迭
        [SerializeField]
        private RandomMonsterListSO[] monsterListSOList;
        
        [SerializeField]
        private string spawnEffectAddress;

        
        //�Լ� ����� �̰� �����̵� ����Ʈ�� �̺�Ʈ�� �Ѱ��ش�.
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
                RandomSpawn(_randomMonsterListSO.spawnMonsterDataArr[_index]);
            }
        }

        private void RandomSpawn(RandomMonsterData _randomMonsterData)
        {
            int _randomRange = Random.Range(_randomMonsterData.minSpawnCount, _randomMonsterData.maxSpawnCount + 1);
            for(; _randomRange-- > 0; )
            {
                StartCoroutine(Spawn(_randomMonsterData));
            }
        }
        private IEnumerator Spawn(RandomMonsterData _randomMonsterData)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
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
            _module.knockBackVector = Vector3.up;
            _module.knockBackPower = 15;
            EffectManager.Instance.SetEffectDefault(spawnEffectAddress, _spawnPos, Quaternion.identity);
			
            obj.SetActive(true);
        }
        
    }
}


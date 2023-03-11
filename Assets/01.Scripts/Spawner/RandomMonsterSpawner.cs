using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using Pool;
using Streaming;
using Utill.Random;
using Module;

namespace Spawner
{
    public class RandomMonsterSpawner : MonoBehaviour, Observer
	{

		public Transform Player
        {
			get
			{
				player ??= GameObject.FindGameObjectWithTag("Player")?.transform;
				return player;
            }
			set
            {
				player = value;

			}
        }

		private Transform player;

		[SerializeField]
		private RandomMonsterListSO randomMonsterListSO;
		[SerializeField]
		private LayerMask spawnLayerMask;

		[SerializeField]
		private float minSpawnTime = 1f;
		[SerializeField]
		private float maxSpawnTime = 3f;

		private float spawnTimer = 1f;

		public float radius = 1f;

		private bool isMonsterNone = true;

		private List<EnemyDead> enemyDeadList = new List<EnemyDead>();

        private void OnDisable()
        {
			player = null;
		}

        private void OnDestroy()
        {
			player = null;
		}

        private void OnEnable()
        {
			isMonsterNone = true;
        }

        private void Update()
        {
			CountDown();
		}

		private void CountDown()
        {
			if(Player is null)
            {
				return;
            }
			if((transform.position - Player.position).sqrMagnitude <= radius * radius)
            {
				if (spawnTimer > 0f)
                {
					spawnTimer -= Time.deltaTime;
				}
				else if(isMonsterNone)
                {
					spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
					RandomAndChoiceSpawn(randomMonsterListSO);
				}
            }
        }

        public void RandomAndChoiceSpawn(RandomMonsterListSO _randomMonsterListSO)
        {
			int _randomRange = Random.Range(_randomMonsterListSO.minSpawnCount, _randomMonsterListSO.maxSpawnCount + 1);
			for(;_randomRange-- > 0 ; )
			{
				int _index = StaticRandom.Choose(_randomMonsterListSO.randomPercentArr);
				RandomSpawn(_randomMonsterListSO.spawnMonsterDataArr[_index]);
			}
        }

		public void RandomSpawn(RandomMonsterData _randomMonsterData)
        {
			int _randomRange = Random.Range(_randomMonsterData.minSpawnCount, _randomMonsterData.maxSpawnCount + 1);
			for(; _randomRange-- > 0; )
            {
				Spawn(_randomMonsterData);
			}
        }

		public void Spawn(RandomMonsterData _randomMonsterData)
		{
			GameObject obj = ObjectPoolManager.Instance.GetObject(_randomMonsterData.enemyAddress);
			ObjectClassCycle objectClassCycle = obj.GetComponentInChildren<ObjectClassCycle>();
			objectClassCycle.TargetObject = obj;
			ObjectSceneChecker _objectSceneChecker = ClassPoolManager.Instance.GetClass<ObjectSceneChecker>("ObjectSceneChecker");
			if (_objectSceneChecker is null)
			{
				_objectSceneChecker = new ObjectSceneChecker();
			}
			_objectSceneChecker.ObjectDataSO = _randomMonsterData.objectDataSO;
			_objectSceneChecker.ObjectClassCycle = objectClassCycle;
			objectClassCycle.AddObjectClass(_objectSceneChecker);
			EnemyDead _enemyDead = obj.GetComponent<EnemyDead>();
			_enemyDead.AddObserver(this);
			enemyDeadList.Add(_enemyDead);	
			obj.transform.position = GetRandomPos();
			obj.SetActive(true);
			isMonsterNone = false;
		}

		private Vector3 GetRandomPos()
        {
			Vector3 _result = new Vector3();

			while(true)
			{
				float randomPosX = Random.Range(-radius, radius);
				float randomPosZ = Random.Range(-radius, radius);

				Vector3 rayPos = new Vector3(randomPosX, transform.position.y + 50, randomPosZ);
				RaycastHit raycastHit;

				if (Physics.Raycast(rayPos, Vector3.down, out raycastHit, 150f, spawnLayerMask))
                {
					_result = rayPos;
					_result.y = raycastHit.point.y + 3f;
					break;
				}
				
			}

			return _result;
        }

        public void Receive()
        {
			for(int i = 0; i < enemyDeadList.Count; )
            {
                try
				{
					if(enemyDeadList[i] is null || enemyDeadList[i].IsDead || enemyDeadList[i].IsDestroy)
					{
						enemyDeadList.RemoveAt(i);
					}
					else
					{
						++i;
					}
				}
                catch
                {
					enemyDeadList.RemoveAt(i);
				}
			}

			if(enemyDeadList.Count == 0)
            {
				isMonsterNone = true;
            }
        }

    }

}
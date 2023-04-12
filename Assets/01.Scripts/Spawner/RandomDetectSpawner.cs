using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using Pool;
using Streaming;
using Utill.Random;
using Module;
using Effect;
using Detect;

namespace Spawner
{
    public class RandomDetectSpawner : MonoBehaviour, Observer
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

		private float spawnTimer = 1f;

		public float radius = 1f;

		private bool isDetectNone = true;

		private Vector3 spawnPos = Vector3.zero;

		private List<IDetectItem> iDetectList = new List<IDetectItem>();
		
		
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
			isDetectNone = true;
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
				if(isDetectNone)
                {
	                if (spawnTimer > 0f)
	                {
		                spawnTimer -= Time.deltaTime;
	                }
	                else
	                {
		                spawnTimer = Random.Range(randomMonsterListSO.minSpawnTime, randomMonsterListSO.maxSpawnTime);
		                RandomAndChoiceSpawn(randomMonsterListSO);   
	                }
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
				StartCoroutine(Spawn(_randomMonsterData));
			}
        }

		public IEnumerator Spawn(RandomMonsterData _randomMonsterData)
		{
			yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
			yield return StartCoroutine(GetRandomPos());
			Vector3 _spawnPos = spawnPos;
			GameObject obj = ObjectPoolManager.Instance.GetObject(_randomMonsterData.enemyAddress);
			iDetectList.Add(obj.GetComponent<IDetectItem>());
			obj.transform.position = _spawnPos;// + new Vector3(0,-2,0);
			obj.SetActive(true);
			isDetectNone = false;
		}

		private IEnumerator GetRandomPos()
        {
			Vector3 _result = new Vector3();

			while(true)
			{
				float randomPosX = Random.Range(-radius, radius);
				float randomPosZ = Random.Range(-radius, radius);

				Vector3 rayPos = new Vector3(transform.position.x + randomPosX, transform.position.y + 50, transform.position.z +randomPosZ);
				RaycastHit raycastHit;

				if (Physics.Raycast(rayPos, Vector3.down, out raycastHit, 150f, spawnLayerMask))
                {
					_result = rayPos;
					_result.y = raycastHit.point.y;
					break;
                }

				yield return null;
			}

			spawnPos = _result;
			yield return null;
        }

        public void Receive()
        {
			for(int i = 0; i < iDetectList.Count; )
            {
                try
				{
					if(iDetectList[i] is null || iDetectList[i].IsGetOut)
					{
						iDetectList.RemoveAt(i);
					}
					else
					{
						++i;
					}
				}
                catch
                {
	                iDetectList.RemoveAt(i);
				}
			}

			if(iDetectList.Count == 0)
            {
				isDetectNone = true;
            }
        }
    }
}

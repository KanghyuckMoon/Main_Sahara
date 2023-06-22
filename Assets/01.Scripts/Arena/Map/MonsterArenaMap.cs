using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Module;
using UnityEngine;

namespace Arena
{
    public class MonsterArenaMap : ArenaMap
    {
        [SerializeField] private List<EnemyDead> spawnMonsterList = new List<EnemyDead>();

        protected override void Awake()
        {
            base.Awake();
            spawnMonsterList = transform.GetComponentsInChildren<EnemyDead>().ToList(); 
            
        }

        protected override void Start()
        {
            base.Start();
        }

        private void OnEnable()
        {
            StartCoroutine(SetInit());
        }
        
        private IEnumerator SetInit()
        {
            while (true)
            {
                if (GameManager.GamePlayerManager.Instance.IsPlaying)
                {
                    InitEnemyList();
                    yield break;
                }
                
                yield return null;
            }
            
        }
        

        public override void Receive()
        {
            // 투기장 몬스터를 모두 처치했는가 
            bool _isComplete = false; 
            for(int i = 0; i < spawnMonsterList.Count; )
            {
                try
                {
                    if(spawnMonsterList[i] is null || spawnMonsterList[i].IsDead || spawnMonsterList[i].IsDestroy)
                    {
                        spawnMonsterList.RemoveAt(i);
                    }
                    else
                    {
                        ++i;
                    }
                }
                catch
                {
                    spawnMonsterList.RemoveAt(i);
                }
            }

            if(spawnMonsterList.Count == 0)
            {
                GetEndTriggerList().First().inactiveTriggerEvent?.Invoke();
                CompleteArena(); 
                // 다음으로 
            }
        }

        private void InitEnemyList()
        {
            foreach (var _monster in spawnMonsterList)
            {
                _monster.AddObserver(this);
            }
        }
    }
}
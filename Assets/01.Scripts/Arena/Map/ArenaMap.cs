using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CondinedModule;
using Data;
using UnityEngine;

namespace Arena
{
    public class ArenaMap : MonoBehaviour, IArenaMap,Observer
    {
        private ParticleSystem particle;
        private bool isActive = false;

        private Player player;
        private StatData statData; 
        [SerializeField]
        private List<AbArenaCtrlTrigger> triggerList = new List<AbArenaCtrlTrigger>(); 
        
        //[Header("탐색 해야할 요소들")]
        //[SerializeField] private List<GameObject> installationList = new List<GameObject>();
        // 프로퍼티 
        public bool IsActive => isActive;

                
        private Player Player
        {
            get
            {
                if (player == null)
                {
                    player = FindObjectOfType<Player>(); 
                }
                return player; 
            }
        }

        private StatData StatData
        {
            get
            {
                if (Player == null) return null;
                if (statData == null)
                {
                    statData = Player.GetComponent<StatData>(); 
                }
                return statData; 
            }
        }
        
        protected virtual void Awake()
        {
            particle = GetComponentInChildren<ParticleSystem>(); 
            triggerList = transform.parent.GetComponentsInChildren<AbArenaCtrlTrigger>().ToList(); 
        }

        protected virtual void Start()
        {
            StartCoroutine(Init(false));
            SetTriggerEvent(); 
            //installationList = 
        }
        
        /// <summary>       
        /// 트리거 이벤트 넣어주기 
        /// </summary>
        protected virtual void SetTriggerEvent()
        {
            foreach (var _trigger in triggerList)
            {
                // 아레나 시작 트리거면 
                _trigger.activeTriggerEvent.AddListener(() => StatData.Jump = 5.0f);
                _trigger.inactiveTriggerEvent.AddListener(() => StatData.Jump = 2.9f);
                if (_trigger.IsStartArena == true)
                {
                    _trigger.activeTriggerEvent.AddListener(StartArena);
                    continue; 
                }
                _trigger.activeTriggerEvent.AddListener(CompleteArena); 
            }
        }

        public virtual void StartArena()
        {
            gameObject.SetActive(true);
            StartCoroutine(Init(true));
        }

        public virtual  void CompleteArena()
        {
            StartCoroutine(Init(false));
        }

        public virtual bool CheckCondition()
        {
            return true; 
        }

        protected virtual IEnumerator Init(bool _isActive)
        {
            isActive = _isActive; 
            if (_isActive == true)
            {
                gameObject.SetActive(isActive);
                particle.Play();
            }
            else
            {
                particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                yield return new WaitForSeconds(1f); 
                gameObject.SetActive(isActive);
            }


        }

        public virtual void Receive()
        {
        }
    }
    
}

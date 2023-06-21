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
        private GameObject map; 
        
        [SerializeField]
        private List<AbArenaCtrlTrigger> triggerList = new List<AbArenaCtrlTrigger>(); 
        
        //[Header("탐색 해야할 요소들")]
        //[SerializeField] private List<GameObject> installationList = new List<GameObject>();
        // 프로퍼티 
        public bool IsActive => isActive;
        public List<AbArenaCtrlTrigger> TriggerList
        {
            get
            {
                if (triggerList.Count == 0)
                {
                    Cashing(); 
                }
                return triggerList;
            }
        }

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
            Cashing();
        }

        protected virtual void Start()
        {
            SetTriggerEvent(); 
            //installationList = 
            StartCoroutine(Init(false, true));
        }

        public void Active(bool _isTrue)
        {
            gameObject.SetActive(_isTrue);
        }
        private void Cashing()
        {
            particle = GetComponentInChildren<ParticleSystem>(); 
            triggerList = transform.GetComponentsInChildren<AbArenaCtrlTrigger>().ToList();
            map = transform.Find("Map").gameObject;
        }
        
        /// <summary>       
        /// 트리거 이벤트 넣어주기 
        /// </summary>
        protected virtual void SetTriggerEvent()
        {
            foreach (var _trigger in triggerList)
            {
                // 아레나 시작 트리거면 
                _trigger.activeTriggerEvent.AddListener(() =>
                {
                    StartArena(); 
                    StatData.Jump = 6.0f;
                });
                _trigger.inactiveTriggerEvent.AddListener(() =>
                {
                    CompleteArena();
                    StatData.Jump = 2.9f;
                });
                /*
                if (_trigger.IsStartArena == true)
                {
                    _trigger.activeTriggerEvent.AddListener(StartArena);
                    continue; 
                }
                _trigger.activeTriggerEvent.AddListener(CompleteArena); 
            */
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

        protected virtual IEnumerator Init(bool _isActive, bool _isImme = false)
        {
            yield return new WaitForEndOfFrame();
            Debug.Log("@@@Init");
            isActive = _isActive; 
            if (_isActive == true)
            {
                map.SetActive(isActive);
                GetEndTriggerList().ForEach((x) => x.gameObject.SetActive(true));
                particle.Play();
            }
            else
            {
                particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                if (_isImme == false)
                {
                    yield return new WaitForSeconds(1f);
                }
                map.SetActive(isActive);
                GetEndTriggerList().ForEach((x) => x.gameObject.SetActive(false));
            }


        }

        public List<AbArenaCtrlTrigger> GetEndTriggerList()
        {
            return TriggerList.FindAll((x) => x.IsStartArena == false); 
        }
                public List<AbArenaCtrlTrigger> GetActiveTriggerList()
                {
                    return TriggerList.FindAll((x) => x.IsStartArena == true); 
                }
        public virtual void Receive()
        {
        }
    }
    
}

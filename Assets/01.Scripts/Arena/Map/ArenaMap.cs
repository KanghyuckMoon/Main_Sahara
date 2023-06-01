using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public class ArenaMap : MonoBehaviour, IArenaMap,Observer
    {
        private ParticleSystem particle;
        private bool isActive = false;

        //[Header("탐색 해야할 요소들")]
        //[SerializeField] private List<GameObject> installationList = new List<GameObject>();
        // 프로퍼티 
        public bool IsActive => isActive;

        protected virtual void Awake()
        {
            particle = GetComponentInChildren<ParticleSystem>(); 
        }

        protected virtual void Start()
        {
            StartCoroutine(Init(false)); 
            //installationList = 
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

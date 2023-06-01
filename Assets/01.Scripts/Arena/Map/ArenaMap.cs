using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public class ArenaMap : MonoBehaviour, IArenaMap,IObserble
    {
        private ParticleSystem particle;
        private List<Observer> observerList = new List<Observer>();
        private bool isActive = false;

        [Header("탐색 해야할 요소들")]
        [SerializeField] private List<GameObject> installationList = new List<GameObject>();
        // 프로퍼티 
        public bool IsActive => isActive;
        public List<Observer> Observers => observerList; 

        private void Awake()
        {
            particle = GetComponentInChildren<ParticleSystem>(); 
        }

        private void Start()
        {
            StartCoroutine(Init(false)); 
            //installationList = 
        }


        public void StartArena()
        {
            StartCoroutine(Init(true));
        }

        public void CompleteArena()
        {
            StartCoroutine(Init(false));
        }

        public bool CheckCondition()
        {
            return true; 
        }

        private IEnumerator Init(bool _isActive)
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

    }
    
}

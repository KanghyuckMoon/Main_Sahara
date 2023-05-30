using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public class ArenaMap : MonoBehaviour, IArenaMap,Observer
    {
        private ParticleSystem particle;

        private void Awake()
        {
            particle = GetComponentInChildren<ParticleSystem>(); 
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void StartArena()
        {
            gameObject.SetActive(true);
            particle.Play();
        }

        public void EndArena()
        {
            particle.Pause();

        }

        public void Receive()
        {
            StartArena(); 
        }
        
        private  void Init()
        {}
    }
    
}

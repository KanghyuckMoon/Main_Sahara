
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Arena
{
    public abstract class AbArenaCtrlTrigger : MonoBehaviour, IArenaStartInteraction
    {
        [SerializeField] 
        private ArenaInteractionType interactionType;
        [Header("True : 투기장 시작 False : 투기장 종료"),SerializeField] 
        private bool isStartArena = false; 
        [SerializeField]
        private ArenaMap _arenaMap;

        public ArenaInteractionType InteractionType => interactionType; 
        public IArenaMap connectArenaMap => _arenaMap;
       
        protected virtual void Awake()
        {
            _arenaMap = transform.root.GetComponentInChildren<ArenaMap>(); 
        }
        
        public void Interact()
        {
            Debug.Log("@@ 투기장 트리거 활성화");
            if (isStartArena == true)
            {
                connectArenaMap.StartArena();
                return; 
            }
            connectArenaMap.CompleteArena();
        }
    }    
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using CondinedModule;
using Data;

namespace Arena
{
    public abstract class AbArenaCtrlTrigger : MonoBehaviour, IArenaStartInteraction
    {
        [SerializeField] 
        private ArenaInteractionType interactionType;
        [Header("True : ������ ���� False : ������ ����"),SerializeField] 
        private bool isStartArena = false; 
        [SerializeField]
        private ArenaMap _arenaMap;

        public UnityEvent activeTriggerEvent = null; 
        public UnityEvent inactiveTriggerEvent = null; 
        
        public ArenaInteractionType InteractionType => interactionType; 
        public IArenaMap connectArenaMap => _arenaMap;
        public bool IsStartArena => isStartArena; 

        protected virtual void Awake()
        {
            _arenaMap = transform.parent.GetComponentInChildren<ArenaMap>(); 
        }
        
        public void Interact()
        {
            Debug.Log("@@ ������ Ʈ���� Ȱ��ȭ");
            if (isStartArena == true)
            {
                activeTriggerEvent?.Invoke();
                //connectArenaMap.StartArena();
                
                return; 
            }
            inactiveTriggerEvent?.Invoke();
            //connectArenaMap.CompleteArena();
        }
    }    
}



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
        [Header("True : ������ ���� False : ������ ����"),SerializeField] 
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
            Debug.Log("@@ ������ Ʈ���� Ȱ��ȭ");
            if (isStartArena == true)
            {
                connectArenaMap.StartArena();
                return; 
            }
            connectArenaMap.CompleteArena();
        }
    }    
}


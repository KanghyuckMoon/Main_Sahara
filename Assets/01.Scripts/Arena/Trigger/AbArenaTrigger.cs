
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public abstract class AbArenaTrigger : MonoBehaviour, IArenaInteraction
    {
        [SerializeField] 
        private ArenaInteractionType interactionType;
        private ArenaMap _arenaMap;

        public ArenaInteractionType InteractionType => interactionType; 
        public IArenaMap connectArenaMap => _arenaMap;
       
        protected virtual void Awake()
        {
            _arenaMap = transform.root.GetComponentInChildren<ArenaMap>(); 
        }
        
        public void Interact()
        {
            connectArenaMap.StartArena();
        }
    }    
}


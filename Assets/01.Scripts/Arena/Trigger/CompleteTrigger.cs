using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public class CompleteTrigger : MonoBehaviour, IArenaStartInteraction
    {
        public ArenaInteractionType InteractionType => ArenaInteractionType.Touch; 
        public IArenaMap connectArenaMap { get; }
        public void Interact()
        {
            
        }

    }
}


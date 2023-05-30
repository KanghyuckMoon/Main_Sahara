using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public enum ArenaInteractionType
    {
        Touch, // 닿기만 해도  
        Detect, // 탐색해야 
        Click, // 아무거나 클릭하면 
    }
    public interface IArenaInteraction
    {
        public ArenaInteractionType InteractionType { get;  }
        public IArenaMap connectArenaMap { get; }

        public void Interact();
    }
    
}

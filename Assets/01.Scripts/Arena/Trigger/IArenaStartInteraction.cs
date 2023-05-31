using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public enum ArenaInteractionType
    {
        Touch, // ��⸸ �ص�  
        Detect, // Ž���ؾ� 
        Click, // �ƹ��ų� Ŭ���ϸ� 
    }
    public interface IArenaStartInteraction : IArenaInteraction
    {
        public IArenaMap connectArenaMap { get; }

    }

    public interface IArenaInteraction
    {
        public ArenaInteractionType InteractionType { get;  }
        public void Interact();

    }
}

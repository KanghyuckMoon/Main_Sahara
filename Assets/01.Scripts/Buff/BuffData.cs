using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buff
{
    public enum Buffs
    {
        U_Healing,
        U_ReduceDamage,
        None
    }
    public enum BuffType
    {
        Update,
        Once,
        None
    }
    
    [System.Serializable]
    public class BuffData
    {
        public Buffs buffs;
        public float duration;
        public float value;
        public float period;
        public string spownObjectName;
        public string spriteName;
        public BuffType bufftype;
    }
    
    
}
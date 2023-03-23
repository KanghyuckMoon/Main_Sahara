using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buff
{
    public enum Buffs
    {
        U_Healing,
        U_ChangeMagicDef,
        U_ChangePhysicDef,
        U_ChangeMana,
        U_MoveSpeed,
        U_AnimationSpeed,
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
        public BuffData()
        {
        }

        public BuffData(BuffData _buffData)
        {
            buffs = _buffData.buffs;
            duration = _buffData.duration;
            buffs = _buffData.buffs;
            value = _buffData.value;
            period = _buffData.period;
            bufftype = _buffData.bufftype;
        }

        public Buffs buffs;
        public float duration;
        public float value;
        public float period;
        /*public string spownObjectName;
        public string spriteName;*/
        public BuffType bufftype;
    }
    
    
}
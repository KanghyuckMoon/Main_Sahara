using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace Buff
{
    /// <summary>
    /// 여기서 몇 초 지속되는지, 생성될 때, 사라질 때를 관리
    /// </summary>
    public abstract class AbBuffEffect : IBuff
    {
        public float Duration
        {
            get
            {
                return duration;
            }
        }
        public float MaxDuration
        {
            get
            {
                return maxDuration;
            }
        }

        protected BuffModule buffModule;

        protected float value = 0;
        protected float maxDuration = 0;
        protected float duration = 0;
        protected float period = 0;

        protected string spownObjectName;

        public AbBuffEffect(BuffModule _buffModule)
        {
            buffModule = _buffModule;
        }

        public AbBuffEffect SetValue(float _value) { value = _value;  return this; }
        public AbBuffEffect SetDuration(float _duration) { maxDuration = _duration; duration = maxDuration;  return this; }
        public AbBuffEffect SetPeriod(float _period) { period = _period;  return this; }
        public AbBuffEffect SetSpownObjectName(string _spownObjectName) { spownObjectName = _spownObjectName;  return this; }

        public abstract void Buff(AbMainModule _mainModule);
    }
}
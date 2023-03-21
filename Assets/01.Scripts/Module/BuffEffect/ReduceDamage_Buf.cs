using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using TimeManager;

namespace Buff
{
    public class ReduceDamage_Buf : AbBuffEffect
    {
        private HpModule hpModule;

        //private float currentDuration;
        private float currentPeriod = 0;

        public ReduceDamage_Buf(BuffModule _buffModule) : base(_buffModule)
        {
            //currentDuration = duration;
        }

        public override void Buff(AbMainModule _mainModule)
        {
            hpModule ??= _mainModule.GetModuleComponent<HpModule>(ModuleType.Hp);
            Timer();
        }

        private void Timer()
        {
            if (duration >= 0)
            {
                if (currentPeriod <= 0)
                {
                    hpModule.SetReduceDamagePercent(value);
                    //Debug.LogError("회복호복");
                    currentPeriod = period;
                }

                currentPeriod -= Time.deltaTime;
                duration -= Time.deltaTime;
            }

            else
            {
                hpModule.SetReduceDamagePercent(-value);
                
                buffModule.buffDic.Remove(this);
                buffModule.buffList.Remove(this);
            }
        }
    }
}
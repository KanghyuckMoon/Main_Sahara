using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace Buff
{
    public class Healing_Buf : AbBuffEffect
    {
        private HpModule hpModule;

        //private float currentDuration;
        private float currentPeriod;

        public Healing_Buf(BuffModule _buffModule) : base(_buffModule)
        {
            //currentDuration = duration;
        }

        public override void Buff(AbMainModule _mainModule)
        {
            hpModule ??= _mainModule.GetModuleComponent<HpModule>(ModuleType.Hp);
            Heal(hpModule);
        }

        private void Heal(HpModule _hpModule)
        {
            if (duration >= 0)
            {
                if (currentPeriod <= 0)
                {
                    hpModule.GetHeal((int)value);
                    //Debug.LogError("회복호복");
                    currentPeriod = period;
                }

                currentPeriod -= Time.deltaTime;
                duration -= Time.deltaTime;
            }

            else
            {
                buffModule.buffDic.Remove(this);
                buffModule.buffList.Remove(this);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Buff;
using Module;
using Data;
using UnityEngine;

namespace Skill
{
    public class ChangePhysicResistance_Buf : AbBuffEffect
    {
        private StatData statData;
        
        private float currentPeriod = 0;

        private float increseResistance;
        
        public ChangePhysicResistance_Buf(BuffModule _buffModule) : base(_buffModule)
        {
        
        }
        
        public override void Buff(AbMainModule _mainModule)
        {
            statData ??= _mainModule.GetComponent<StatData>();
            Timer();
        }
        
        private void Timer()
        {
            if (duration >= 0)
            {
                if (currentPeriod <= 0)
                {
                    increseResistance = CalculateDef(statData.PhysicalResistance);

                    statData.PhysicalResistance += (int)increseResistance;
                    
                    currentPeriod = period;
                }

                currentPeriod -= Time.deltaTime;
                duration -= Time.deltaTime;
            }

            else
            {
                statData.PhysicalResistance -= (int)increseResistance;
                
                buffModule.buffDic.Remove(this);
                buffModule.buffList.Remove(this);
            }
        }

        private float CalculateDef(float _def)
        {
            float addspeed = _def * (value / 100f);

            return addspeed;
        }
    }
}
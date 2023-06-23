using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Module;

namespace Buff
{
    public class MoveSpeed_Buf : AbBuffEffect
    {
        private StatData statData;
        
        private float currentPeriod = 0;

        private float walk;
        private float run;

        public MoveSpeed_Buf(BuffModule _buffModule) : base(_buffModule)
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
                    walk = CalculateSpeed(statData.WalkSpeed);
                    run = CalculateSpeed(statData.RunSpeed);

                    statData.WalkSpeed += walk;
                    statData.RunSpeed += run;
                    //Debug.LogError("회복호복");
                    
                    currentPeriod = period;
                }

                currentPeriod -= Time.deltaTime;
                duration -= Time.deltaTime;
            }

            else
            {
                statData.WalkSpeed -= walk;
                statData.RunSpeed -= run;
                
                buffModule.buffDic.Remove(this);
                buffModule.buffList.Remove(this);
            }
        }

        private float CalculateSpeed(float _speed)
        {
            float addspeed = _speed * (value / 100f);

            return addspeed;
        }
    }
}
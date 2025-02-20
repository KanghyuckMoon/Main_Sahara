using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Module;

namespace Buff
{
    public class AnimationSpeed_Buf : AbBuffEffect
    {
        private float currentPeriod = 0;
        private AbMainModule abMainModule;
        
        public AnimationSpeed_Buf(BuffModule _buffModule) : base(_buffModule)
        {
        }

        public override void Buff(AbMainModule _mainModule)
        {
            abMainModule = _mainModule;
            Timer();
        }
        
        private void Timer()
        {
            if (duration >= 0)
            {
                if (currentPeriod <= 0)
                {
                    
                    
                    abMainModule.PersonalTime += CalculateSpeed(abMainModule.PersonalTime);
                    //Debug.LogError("회복호복");
                    
                    currentPeriod = period;
                }

                currentPeriod -= Time.deltaTime;
                duration -= Time.deltaTime;
            }

            else
            {
                abMainModule.PersonalTime -= CalculateSpeed(abMainModule.PersonalTime);
                
                buffModule.buffDic.Remove(this);
                buffModule.buffList.Remove(this);
            }
        }

        private float CalculateSpeed(float _speed)
        {
            float addspeed = _speed * (value / 100);

            return addspeed;
        }
    }
}
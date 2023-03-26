using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Buff;
using Data;
using Module;

namespace Skill
{
    public class ChangeMana_Buf : AbBuffEffect
    {
        private StatData statData;
        
        private float currentPeriod = 0;
        
        public ChangeMana_Buf(BuffModule _buffModule) : base(_buffModule)
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
                    statData.ManaChange = value;
                    //Debug.LogError("회복호복");
                    
                    currentPeriod = period;
                }

                currentPeriod -= Time.deltaTime;
                duration -= Time.deltaTime;
            }

            else
            {
                statData.ManaChange = 0;
                buffModule.buffDic.Remove(this);
                buffModule.buffList.Remove(this);
            }
        }
    }
}
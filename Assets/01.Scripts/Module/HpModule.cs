using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace Module
{
    public class HpModule : AbBaseModule
    {
        private int maxHp;
        private int currentHp;

        private Data.StatData statData;

        public HpModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            statData = mainModule.GetComponent<Data.StatData>();
            statData.CurrentHp = statData.MaxHp;
        }


        public int GetDamage(int value)
        {
            value = -value;
            return ChangeHpValue(value);
        }

        public void GetHeal(int value)
        {
            ChangeHpValue(value);
        }

        private int ChangeHpValue(int value)
        {
            statData.CurrentHp += value;
            if (statData.CurrentHp > statData.MaxHp) statData.CurrentHp = statData.MaxHp;
            if (statData.CurrentHp < 0) statData.CurrentHp = 0;

            return statData.CurrentHp;
        }
    }
}
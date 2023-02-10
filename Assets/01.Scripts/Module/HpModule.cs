using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module
{
    public class HpModule : AbBaseModule
    {
        public int CurrentHp 
        {
            get { return currentHp; }
            set 
            {
                currentHp = value;
            } 
        }

        private int maxHp;
        private int currentHp;

        private StateModule stateModule;

        public HpModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            stateModule = mainModule.GetModuleComponent<StateModule>(ModuleType.State);
            maxHp = stateModule.MaxHp;
            currentHp = maxHp;
        }


        public void GetDamage(int value)
        {
            value = -value;
            ChangeHpValue(value);
        }

        public void GetHeal(int value)
        {
            ChangeHpValue(value);
        }

        private void ChangeHpValue(int value)
        {
            stateModule.CurrentHp += value;
            if (currentHp > maxHp) currentHp = maxHp;
            if (currentHp < 0) currentHp = 0;
        }
    }
}
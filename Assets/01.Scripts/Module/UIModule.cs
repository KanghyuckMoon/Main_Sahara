using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module
{
    public class StateData
    {
        public StateData()
        {
            maxHp = 0;
            currentHp = 0;
            speed = 0;
            jumpScale = 0;
        }

        public int maxHp;
        public int currentHp;

        public float speed;
        public float jumpScale;

    }

    public class UIModule : AbBaseModule
    {
        public StateModule stateModule => mainModule.GetModuleComponent<StateModule>(ModuleType.State);
        public HpModule hpModule => mainModule.GetModuleComponent<HpModule>(ModuleType.Hp);


        public StateData stateData;

        public UIModule(MainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            stateData = new StateData();

            SetState();
        }

        public void SetState()
        {
            stateData.jumpScale = stateModule.JumpPower;
            stateData.speed = stateModule.Speed;
            stateData.maxHp = stateModule.MaxHp;
        }

        public override void Update()
        {
            stateData.currentHp = hpModule.CurrentHp;
        }
    }
}
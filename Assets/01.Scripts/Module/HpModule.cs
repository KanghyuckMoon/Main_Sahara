using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Pool;

namespace Module
{
    public class HpModule : AbBaseModule
    {
        private int maxHp;
        private int currentHp;

        private StatData _StatData => mainModule.StatData;

        public HpModule(AbMainModule _mainModule) : base(_mainModule)
        {
            Init(_mainModule);
        }
        public HpModule()
        {

        }

        public override void Init(AbMainModule _mainModule, params string[] _parameters)
        {
            mainModule = _mainModule;
            Awake();
        }

        public override void Start()
        {
            _StatData.CurrentHp = _StatData.MaxHp;
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
            _StatData.CurrentHp += value;
            if (_StatData.CurrentHp > _StatData.MaxHp) _StatData.CurrentHp = _StatData.MaxHp;
            if (_StatData.CurrentHp < 0) _StatData.CurrentHp = 0;

            return _StatData.CurrentHp;
        }

		public override void OnDisable()
		{
			base.OnDisable();
            ClassPoolManager.Instance.RegisterObject<HpModule>("HpModule", this);
        }
    }
}
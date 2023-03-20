using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utill.Addressable;
using Pool;

namespace Module
{
    public class SkillModule : AbBaseModule
    {
        private StateModule StateModule
        {
            get
            {
                stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
                return stateModule;
            }
        }

        private Dictionary<string, ISkill> currentSkill = new Dictionary<string, ISkill>();
        private IWeaponSkill weaponSkill;

        private StateModule stateModule;

        public SkillModule(AbMainModule _mainModule) : base(_mainModule)
        {
            SetSkill("E", "TestSkill_01");
            SetSkill("R", "TestSkill_02");
        }

        public SkillModule() : base()
        {
            
        }

        public override void Start()
        {
            
        }


        public void SetSkill(string _keyCode, string _address)
        {
            ISkill _skill = AddressablesManager.Instance.GetResource<GameObject>(_address).GetComponent<ISkill>();

            if (currentSkill.TryGetValue(_keyCode, out var _value))
                _value = _skill;
            else
                currentSkill.Add(_keyCode, _skill);
        }
        public void SetWeaponSkill(IWeaponSkill _weaponSkill)
        {
            weaponSkill = _weaponSkill;
        }
        public void RemoveSkill(string _keyCode)
        {
            currentSkill.Remove(_keyCode);
        }
        // ReSharper disable Unity.PerformanceAnalysis
        public void UseSkill(string _keyCode)
        {
            if (!CheakSkill(_keyCode)) return;
            StateModule.AddState(State.SKILL);
            currentSkill[_keyCode].Skill(mainModule);
        }
        public void UseWeaponSkill()
        {
            if (!CheakWeaponSkill()) return;
            StateModule.AddState(State.SKILL);
            weaponSkill.Skills(mainModule);
        }

        private bool CheakSkill(string _keyCode)
        {
            return currentSkill.TryGetValue(_keyCode, out var _skill);
        }

        private bool CheakWeaponSkill()
        {
            return weaponSkill is not null;
        }
        
        
        public override void OnDisable()
        {
            currentSkill.Clear();
            weaponSkill = null;
            stateModule = null;
            base.OnDisable();
            ClassPoolManager.Instance.RegisterObject<SkillModule>("SkillModule", this);
        }
        
        public override void OnDestroy()
        {
            currentSkill.Clear();
            weaponSkill = null;
            stateModule = null;
            base.OnDestroy();
            ClassPoolManager.Instance.RegisterObject<SkillModule>("SkillModule", this);
        }
        
    }
}
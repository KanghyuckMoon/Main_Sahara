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

        }

        public SkillModule() : base()
        {
            
        }

        public override void Start()
        {
            SetSkill("E", "TestSkill");
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
        public void UseSkill(string _keyCode)
        {
            currentSkill[_keyCode]?.Skill(mainModule);
            StateModule.AddState(State.SKILL);
        }
        public void UseWeaponSkill()
        {
            weaponSkill.Skills(mainModule);
            StateModule.AddState(State.SKILL);
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
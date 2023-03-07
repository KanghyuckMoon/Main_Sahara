using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;

namespace Module
{
    public class SkillModule : AbBaseModule
    {
        private Dictionary<string, ISkill> currentSkill = new Dictionary<string, ISkill>();

        public SkillModule(AbMainModule _mainModule) : base(_mainModule)
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

        public void RemoveSkill(string _keyCode)
        {
            currentSkill.Remove(_keyCode);
        }

        public void UseSkill(string _keyCode)
        {
            currentSkill[_keyCode]?.Skill(mainModule);
        }
    }
}
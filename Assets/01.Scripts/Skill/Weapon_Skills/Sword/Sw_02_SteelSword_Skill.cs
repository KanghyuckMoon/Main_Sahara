using System.Collections;
using System.Collections.Generic;
using Module;
using UnityEngine;
using HitBox;

namespace Skill
{
    public class Sw_02_SteelSword_Skill : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;

        public void Skills(AbMainModule _mainModule)
        {
            if (UseMana(_mainModule, -usingMana))
            {
                _mainModule.GetModuleComponent<StateModule>(ModuleType.State).AddState(State.SKILL);
                PlaySkillAnimation(_mainModule, animationClip);
                //GetBuff(_mainModule);
            }
        }
        public HitBoxAction GetHitBoxAction()
        {
            return null;
        }
    }
}
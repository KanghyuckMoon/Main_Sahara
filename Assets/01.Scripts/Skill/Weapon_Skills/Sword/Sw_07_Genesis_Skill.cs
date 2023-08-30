using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using HitBox;

namespace Skill
{
    public class Sw_07_Genesis_Skill : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;

        public void Skills(AbMainModule _mainModule)
        {
            UseMana(_mainModule, -usingMana);
            PlaySkillAnimation(_mainModule, animationClip);
        }
        public HitBoxAction GetHitBoxAction()
        {
            return null;
        }
    }
}
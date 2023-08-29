using System.Collections;
using System.Collections.Generic;
using Module;
using UnityEngine;
using HitBox;

namespace Skill
{
    public class Sw_04_Dainsleif_Skill : WeaponSkillFunctions, IWeaponSkill
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
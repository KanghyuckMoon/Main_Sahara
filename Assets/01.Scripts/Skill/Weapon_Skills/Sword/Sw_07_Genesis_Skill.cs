using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace Skill
{
    public class Sw_07_Genesis_Skill : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;

        
        //[SerializeField] private buv

        [SerializeField]
        private int usingMana;

        public void Skills(AbMainModule _mainModule)
        {
            UseMana(_mainModule, -usingMana);
            PlaySkillAnimation(_mainModule, animationClip);
        }
    }
}
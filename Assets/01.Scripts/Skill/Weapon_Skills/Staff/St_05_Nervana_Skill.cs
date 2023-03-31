using System.Collections;
using System.Collections.Generic;
using Module;
using UnityEngine;

namespace Skill
{
    public class St_05_Nervana_Skill : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;
        [SerializeField]
        private int usingMana;

        public void Skills(AbMainModule _mainModule)
        {
            UseMana(_mainModule, -usingMana);
            PlaySkillAnimation(_mainModule, animationClip);
        }
    }
}
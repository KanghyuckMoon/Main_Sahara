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

        public void Skills(AbMainModule _mainModule)
        {
            PlaySkillAnimation(_mainModule, animationClip);
        }
    }
}
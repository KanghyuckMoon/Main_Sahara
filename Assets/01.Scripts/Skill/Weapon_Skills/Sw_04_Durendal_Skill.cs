using System.Collections;
using System.Collections.Generic;
using Module;
using UnityEngine;

namespace Skill
{
    public class Sw_04_Durendal_Skill : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;

        public void Skills(AbMainModule _mainModule)
        {
            PlaySkillAnimation(_mainModule, animationClip);
            GetBuff(_mainModule);
        }
    }
}
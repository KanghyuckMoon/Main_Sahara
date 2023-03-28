using System.Collections;
using System.Collections.Generic;
using Module;
using UnityEngine;

namespace Skill
{
    public class St_01_PlausibleStaff_Skill : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;

        public void Skills(AbMainModule _mainModule)
        {
            PlaySkillAnimation(_mainModule, animationClip);
        }
    }
}
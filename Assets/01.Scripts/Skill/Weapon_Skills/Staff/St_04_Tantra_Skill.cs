using System.Collections;
using System.Collections.Generic;
using Module;
using UnityEngine;

namespace Skill
{
    public class St_04_Tantra_Skill : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;

        public void Skills(AbMainModule _mainModule)
        {
            PlaySkillAnimation(_mainModule, animationClip);
        }
    }
}
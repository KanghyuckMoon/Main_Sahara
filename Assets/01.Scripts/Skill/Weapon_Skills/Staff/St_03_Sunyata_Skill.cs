using System.Collections;
using System.Collections.Generic;
using Module;
using UnityEngine;

namespace Skill
{
    public class St_03_Sunyata_Skill : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;

        public void Skills(AbMainModule _mainModule)
        {
            PlaySkillAnimation(_mainModule, animationClip);
        }
    }
}
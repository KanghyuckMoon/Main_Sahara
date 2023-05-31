using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using HitBox;

namespace Skill
{
    public class ThreeFire_Skill : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;
        
        public void Skills(AbMainModule _mainModule)
        {
            PlaySkillAnimation(_mainModule, animationClip);
        }
        public HitBoxAction GetHitBoxAction()
        {
            return null;
        }
    }
}
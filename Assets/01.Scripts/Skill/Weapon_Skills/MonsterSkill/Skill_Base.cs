using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Streaming;
using Pool;
using HitBox;

namespace Skill
{

    public class Skill_Base : WeaponSkillFunctions, IWeaponSkill
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

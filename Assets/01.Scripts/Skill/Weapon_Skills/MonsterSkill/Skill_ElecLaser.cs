using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Streaming;
using Pool;
using HitBox;

namespace Skill
{

    public class Skill_ElecLaser : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;

        [SerializeField] private GameObject elecLaser;

        public void Skills(AbMainModule _mainModule)
        {
            PlaySkillAnimation(_mainModule, animationClip);
            elecLaser.SetActive(true);
        }
        public HitBoxAction GetHitBoxAction()
        {
            return null;
        }

        public void EndSkill()
        {
            elecLaser.SetActive(false);
        }
    }   
}

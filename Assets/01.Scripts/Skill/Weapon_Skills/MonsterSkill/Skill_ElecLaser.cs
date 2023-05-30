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
        [SerializeField] private Transform hitPoint;
        [SerializeField] private Vector3 originHitPos;
        
        public void Skills(AbMainModule _mainModule)
        {
            PlaySkillAnimation(_mainModule, animationClip);
            hitPoint.localPosition = originHitPos;
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

using System;
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
        [SerializeField] private float delay = 0.5f;
        private bool isSpawn;
        
        public void Start()
        {
            elecLaser.SetActive(false);
        }

        public void Skills(AbMainModule _mainModule)
        {
            isSpawn = true;
            PlaySkillAnimation(_mainModule, animationClip);
            Invoke("SpawnLaser", delay);
        }
        public HitBoxAction GetHitBoxAction()
        {
            return null;
        }

        private void SpawnLaser()
        {
            if (isSpawn)
            {
                hitPoint.localPosition = originHitPos;
                elecLaser.SetActive(true);
            }
        }

        public void EndSkill()
        {
            isSpawn = false;
            elecLaser.SetActive(false);
        }
    }   
}

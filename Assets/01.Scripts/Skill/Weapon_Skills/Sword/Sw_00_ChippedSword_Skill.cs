using System;
using System.Collections;
using System.Collections.Generic;
using Module;
using UnityEngine;
using HitBox;
using Pool;
using Utill.Addressable;

namespace Skill
{
    public class Sw_00_ChippedSword_Skill : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;
        
        [SerializeField]
        private int usingMana;

        [SerializeField] private HitBoxInAction hitBoxInAction;
        [SerializeField] private HitBoxAction hitBoxAction = new HitBoxAction();

        private void Start()
        {
            hitBoxAction.SetCondition(Hit, 30, HitBoxActionType.Hit);
            hitBoxAction.SetCondition(HitEffect, Vector2.zero, HitBoxActionType.Start);
        }

        public void Skills(AbMainModule _mainModule)
        {
            UseMana(_mainModule, -usingMana);
        
            PlaySkillAnimation(_mainModule, animationClip);
        }

        public HitBoxAction GetHitBoxAction()
        {
            return hitBoxAction;
        }

        public void Hit(int _a)
        {
            //Debug.LogError("弧府弧府 弧府弧府");
        }

        public void HitEffect(Vector2 _vector2)
        {
            //GameObject obj = ObjectPoolManager.Instance.GetObject("FireEffect_1");
            //obj.SetActive(true);
            //obj.transform.position = transform.position;
        }
    }
}
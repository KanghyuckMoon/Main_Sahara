using System;
using System.Collections;
using System.Collections.Generic;
using Module;
using UnityEngine;
using HitBox;
using DG.Tweening;
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

        [SerializeField] private string _skillEffectName;

        private GameObject effect;

        private void Start()
        {
            hitBoxAction.SetCondition(Hit, 30, HitBoxActionType.Hit);
            hitBoxAction.SetCondition(HitEffect, Vector2.zero, HitBoxActionType.Start);
        }

        public void Skills(AbMainModule _mainModule)
        {
            UseMana(_mainModule, -usingMana);
            PlaySkillAnimation(_mainModule, animationClip);

            effect = ObjectPoolManager.Instance.GetObject(_skillEffectName);
            effect.transform.SetParent(transform);
            effect.transform.localPosition = Vector3.forward * 3;
            effect.SetActive(true);
            Invoke(nameof(SetEffectOff), 1.96f);
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

        private void SetEffectOff()
        {
            effect.SetActive(false);
            ObjectPoolManager.Instance.RegisterObject(_skillEffectName, effect);
        }
    }
}
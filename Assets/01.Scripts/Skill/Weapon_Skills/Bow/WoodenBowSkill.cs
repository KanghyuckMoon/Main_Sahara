using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using UnityEngine;
using HitBox;
using Pool;
using Utill.Addressable;
using Player;
using Weapon;

namespace Skill
{
    public class WoodenBowSkill : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;

        [SerializeField] private HitBoxInAction hitBoxInAction;
        [SerializeField] private HitBoxAction hitBoxAction = new HitBoxAction();

        private SetPlayerMaterial setPlayerMaterial;

        public void Skills(AbMainModule _mainModule)
        {
            setPlayerMaterial ??= transform.root.GetComponentInChildren<SetPlayerMaterial>();

            GameObject _skillProjectile = ObjectPoolManager.Instance.GetObject("WoodenBowSkill_Arrow");
            _skillProjectile.GetComponent<WoodenBowSkillObject>().SetInfo(transform.root.gameObject, "Enemy");
            
            var _effect = ObjectPoolManager.Instance.GetObject("WoodenBow_SkillEffectShot");
            _effect.transform.position = transform.position;
            _effect.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            _effect.SetActive(true);
            
            PlaySkillAnimation(_mainModule, animationClip);
            UseMana(_mainModule, usingMana);
            GetBuff(_mainModule);
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
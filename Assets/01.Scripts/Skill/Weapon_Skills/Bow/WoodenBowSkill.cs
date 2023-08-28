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
        
        [SerializeField]
        private int usingMana;  

        [SerializeField] private HitBoxInAction hitBoxInAction;
        [SerializeField] private HitBoxAction hitBoxAction = new HitBoxAction();

        private SetPlayerMaterial setPlayerMaterial;

        public void Skills(AbMainModule _mainModule)
        {
            setPlayerMaterial ??= transform.root.GetComponentInChildren<SetPlayerMaterial>();

            GameObject _skillProjectile = ObjectPoolManager.Instance.GetObject("WoodenBowSkillProjectile");
            _skillProjectile.GetComponent<WoodenBowSkillObject>().SetInfo(transform.root.gameObject, "Enemy");
            
            
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
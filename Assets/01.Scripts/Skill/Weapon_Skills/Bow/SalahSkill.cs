using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using UnityEngine;
using HitBox;
using Pool;
using Utill.Addressable;
using Player;

namespace Skill
{
    public class SalahSkill : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;
        
        [SerializeField]
        private int usingMana;

        [SerializeField] private Material playerReinnforceMat;

        [SerializeField] private HitBoxInAction hitBoxInAction;
        [SerializeField] private HitBoxAction hitBoxAction = new HitBoxAction();

        private SetPlayerMaterial setPlayerMaterial;

        public void Skills(AbMainModule _mainModule)
        {
            setPlayerMaterial ??= transform.root.GetComponentInChildren<SetPlayerMaterial>();

            var _effect = ObjectPoolManager.Instance.GetObject("Salah_SkillEffect_Aura");
            var _barrier = ObjectPoolManager.Instance.GetObject("Salah_SkillEffect");

            _effect.transform.SetParent(transform.root);
            _effect.transform.localPosition = new Vector3(0, 0.1f, 0);
            _effect.SetActive(true);
            
            _barrier.transform.SetParent(transform.root);
            _barrier.transform.localPosition = new Vector3(0, 1f, 0);
            _barrier.SetActive(true);
            
            setPlayerMaterial.SetMaterials(playerReinnforceMat);
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
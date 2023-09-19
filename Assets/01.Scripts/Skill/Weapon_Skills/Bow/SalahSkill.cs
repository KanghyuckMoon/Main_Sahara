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

        [SerializeField] private Material playerReinnforceMat;

        [SerializeField] private HitBoxInAction hitBoxInAction;
        [SerializeField] private HitBoxAction hitBoxAction = new HitBoxAction();

        private GameObject effect;
        private GameObject barrier;

        private SetPlayerMaterial setPlayerMaterial;

        // ReSharper disable Unity.PerformanceAnalysis
        public void Skills(AbMainModule _mainModule)
        {
            if (!UseMana(_mainModule, usingMana)) return;
            setPlayerMaterial ??= transform.root.GetComponentInChildren<SetPlayerMaterial>();

            effect = ObjectPoolManager.Instance.GetObject("Salah_SkillEffect_Aura");
            barrier = ObjectPoolManager.Instance.GetObject("Salah_SkillEffect");

            effect.transform.SetParent(transform.root);
            effect.transform.localPosition = new Vector3(0, 0.1f, 0);
            effect.SetActive(true);
            
            barrier.transform.SetParent(transform.root);
            barrier.transform.localPosition = new Vector3(0, 1f, 0);
            barrier.SetActive(true);
            
            setPlayerMaterial.SetMaterials(playerReinnforceMat);

            Invoke(nameof(ResetEffect), 10);
            PlaySkillAnimation(_mainModule, animationClip);
        }
        public HitBoxAction GetHitBoxAction()
        {
            return hitBoxAction;
        }

        public void Hit(int _a)
        {
            //Debug.LogError("»¡¸®»¡¸® »¡¸®»¡¸®");
        }

        public void HitEffect(Vector2 _vector2)
        {
            //GameObject obj = ObjectPoolManager.Instance.GetObject("FireEffect_1");
            //obj.SetActive(true);
            //obj.transform.position = transform.position;
        }

        private void ResetEffect()
        {
            Debug.LogError("¾ø¾îÁ³´Ù" + gameObject.name);
            setPlayerMaterial.ResetMaterials();
            ObjectPoolManager.Instance.RegisterObject("Salah_SkillEffect", barrier);
            ObjectPoolManager.Instance.RegisterObject("Salah_SkillEffect_Aura", effect);
            barrier.SetActive(false);
            effect.SetActive(false);
        }
    }
}
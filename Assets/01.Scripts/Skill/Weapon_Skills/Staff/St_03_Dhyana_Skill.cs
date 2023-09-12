using System.Collections;
using System.Collections.Generic;
using Module;
using UnityEngine;
using Pool;
using HitBox;

namespace Skill
{
    public class St_03_Dhyana_Skill : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;

        public void Skills(AbMainModule _mainModule)
        {
            if (UseMana(_mainModule, -usingMana))
            {
                var _meteo = ObjectPoolManager.Instance.GetObject("Dhyana_SkillEffect");
                _meteo.transform.SetParent(null);
                _meteo.transform.localPosition = transform.root.position + transform.root.TransformDirection(new Vector3(0, 0.1f, 5f));

                _meteo.tag = _mainModule.tag;

                var _hitbox = _meteo.GetComponent<HitBoxOnProjectile>();
                _hitbox.SetOwner(transform.root.gameObject);

                StartCoroutine(SetHitBox(_hitbox));

                _meteo.SetActive(true);
                
                PlaySkillAnimation(_mainModule, animationClip);
            }
        }
        public HitBoxAction GetHitBoxAction()
        {
            return null;
        }

        IEnumerator SetHitBox(HitBoxOnProjectile _hitBox)
        {
            yield return new WaitForSeconds(0.9f);
            _hitBox.SetEnable();
        }
    }
}
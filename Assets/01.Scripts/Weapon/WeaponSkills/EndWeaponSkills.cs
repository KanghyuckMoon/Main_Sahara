using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Pool;

namespace Weapon
{
    public class EndWeaponSkills : MonoBehaviour, IWeaponSkills
    {
        public BaseWeapon baseWeapon;

        private Animator animator;
        private GameObject effect;

        public void WeaponSkills()
        {
            animator = GetComponentInParent<Animator>();

            animator.Play("Skills");
            StartCoroutine(EffectSpown());
            //effect.transform.SetParent(animator.gameObject.transform);

            //animator.gameObject.transform.position
            //Vector3 vec = animator.gameObject.transform.position;

            //vec += new Vector3(0, 20, 0);
            //animator.gameObject.transform.position = vec;
        }

        IEnumerator EffectSpown()
        {
            effect = ObjectPoolManager.Instance.GetObject("EndSword_SkillEffect");
            effect.transform.SetParent(animator.transform);
            effect.SetActive(true);

            yield return new WaitForSecondsRealtime(2f);
            effect.SetActive(false);
            ObjectPoolManager.Instance.RegisterObject("EndSword_SkillEffect", effect);
        }

        public void Skills()
        {
            WeaponSkills();
        }
    }
}
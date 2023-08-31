using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using UnityEngine;
using HitBox;
using Pool;
using Utill.Addressable;

namespace Skill
{
    public class WuduSkill : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;

        [SerializeField] private HitBoxInAction hitBoxInAction;
        [SerializeField] private HitBoxAction hitBoxAction = new HitBoxAction();

        public void Skills(AbMainModule _mainModule)
        {
            var skillObject = ObjectPoolManager.Instance.GetObject("Wudu_SkillEffect");
            //skillObject.transform.SetParent(transform.root);
            //skillObject.transform.localPosition = transform.root.position + new Vector3(2, 1, 0);
            skillObject.GetComponent<Wudu_Projectile>().SetSubject(transform.root.gameObject, 1, 1);
            skillObject.SetActive(true);

            //Debug.Log(skillObject);
            
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
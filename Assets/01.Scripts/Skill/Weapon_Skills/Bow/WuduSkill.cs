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

        private bool isBallOn = false;

        private GameObject skillObject;

        public void Skills(AbMainModule _mainModule)
        {
            if (isBallOn) return;
            if (UseMana(_mainModule, -usingMana))
            {
                skillObject = ObjectPoolManager.Instance.GetObject("Wudu_SkillEffect");
                skillObject.transform.localPosition = Vector3.zero;
                skillObject.GetComponent<HitBoxOnProjectile>().SetOwner(transform.root.gameObject);
                skillObject.tag = _mainModule.tag;
                skillObject.GetComponent<HitBoxOnProjectile>().SetEnable();
                skillObject.GetComponent<Wudu_Projectile>().SetSubject(transform.root.gameObject, 1, 1);
                skillObject.SetActive(true);

                isBallOn = true;

                Invoke(nameof(BallOff), 10f);

                PlaySkillAnimation(_mainModule, animationClip);
            }
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

        public void BallOff()
        {
            skillObject.SetActive(false);
            ObjectPoolManager.Instance.RegisterObject("Wudu_SkillEffect", skillObject);
            
            isBallOn = false;
        }
    }
}
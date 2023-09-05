using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using UnityEngine;
using HitBox;
using Pool;
using UnityEngine.Serialization;
using Utill.Addressable;
using Weapon;

namespace Skill
{
    public class HajjSkill : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]                    
        private AnimationClip animationClip;
        
        

        [SerializeField] private HitBoxInAction hitBoxInAction;
        [SerializeField] private HitBoxAction hitBoxAction = new HitBoxAction();

        private MeshRenderer meshRenderer;

        private Disolve disolve;
        
        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            disolve = GetComponent<Disolve>();
        }

        public void Skills(AbMainModule _mainModule)
        {
            if (UseMana(_mainModule, -usingMana))
            {
                //transform.root.GetComponent<CharacterController>().Move(Vector3.forward * 10);

                var sword = ObjectPoolManager.Instance.GetObject("Hajj_SkillSword");
                var effect = ObjectPoolManager.Instance.GetObject("Hajj_SkillEffect");
                //var effect2 = ObjectPoolManager.Instance.GetObject("Hajj_SkillEffect_1");

                //meshFilter.mesh = swordMesh;
                //meshRenderer.material = swordMat;

                //transform.localScale = Vector3.one;

                sword.transform.SetParent(transform.parent);
                sword.GetComponent<RememberPosition>().SetPos();
                sword.SetActive(true);
                sword.GetComponent<Disolve>().DoFade(0, 1, 1f);
                disolve.DoFade(1, 0, 0.8f);

                effect.transform.localPosition = _mainModule.transform.position + new Vector3(0, 1, 0);
                effect.transform.SetParent(_mainModule.transform);
                effect.SetActive(true);

                //effect2.transform.localPosition = transform.position;
                //effect2.SetActive(true);

                //meshRenderer.enabled = false;

                PlaySkillAnimation(_mainModule, animationClip);
                GetBuff(_mainModule);

                StartCoroutine(SetSwordOff(sword, animationClip.length, _mainModule));
            }
        }

        IEnumerator SetSwordOff(GameObject _sword, float _time,AbMainModule _mainModule)
        {
            yield return new WaitUntil(() => !_mainModule.Animator.GetBool("WeaponSkill"));
    
            //meshRenderer.enabled = true;
            _sword.GetComponent<Disolve>().DoFade(1, 0, 1f);
            disolve.DoFade(0, 1, 0.8f);
            //transform.root.GetComponent<Animator>().speed
            
            yield return new WaitForSeconds(0.1f);
            _sword.SetActive(false);
            
            ObjectPoolManager.Instance.RegisterObject("Hajj_SkillSword", _sword);
            
            /*transform.localScale = new Vector3(0.24f, 0.24f,0.24f);
            meshFilter.mesh = originMesh;
            meshRenderer.material = originMat;*/
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
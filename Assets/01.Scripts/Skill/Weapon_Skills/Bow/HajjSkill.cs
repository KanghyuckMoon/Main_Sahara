using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using UnityEngine;
using HitBox;
using Pool;
using Utill.Addressable;
using Weapon;

namespace Skill
{
    public class HajjSkill : WeaponSkillFunctions, IWeaponSkill
    {
        [SerializeField]
        private AnimationClip animationClip;
        
        [SerializeField]
        private int usingMana;

        [SerializeField] private HitBoxInAction hitBoxInAction;
        [SerializeField] private HitBoxAction hitBoxAction = new HitBoxAction();

        [SerializeField] private Mesh swordMesh;
        [SerializeField] private Material mat;

        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        private Material originMat;
        private Mesh originMesh;

        private void Start()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void Skills(AbMainModule _mainModule)
        {
            //transform.root.GetComponent<CharacterController>().Move(Vector3.forward * 10);

            var sword = ObjectPoolManager.Instance.GetObject("Hajj_SkillSword");

            meshFilter.mesh = swordMesh;
            
            
            sword.transform.SetParent(transform.parent);
            sword.GetComponent<RememberPosition>().SetPos();
            sword.SetActive(true);

            StartCoroutine(SetSwordOff(sword, animationClip.length));

            PlaySkillAnimation(_mainModule, animationClip);
            UseMana(_mainModule, usingMana);
            GetBuff(_mainModule);
        }

        IEnumerator SetSwordOff(GameObject _sword, float _time)
        {
            yield return new WaitForSeconds(_time);
            _sword.SetActive(false);
            ObjectPoolManager.Instance.RegisterObject("Hajj_SkillSword", _sword);
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
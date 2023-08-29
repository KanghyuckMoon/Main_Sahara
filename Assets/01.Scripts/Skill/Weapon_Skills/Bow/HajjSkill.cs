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
        
        [SerializeField]
        private int usingMana;

        [SerializeField] private HitBoxInAction hitBoxInAction;
        [SerializeField] private HitBoxAction hitBoxAction = new HitBoxAction();

        [SerializeField] private Mesh swordMesh;
        [SerializeField] private Material swordMat;

        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        private Material originMat;
        private Mesh originMesh;

        private void Start()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();

            originMesh = meshFilter.mesh;
            originMat = meshRenderer.material;
        }

        public void Skills(AbMainModule _mainModule)
        {
            //transform.root.GetComponent<CharacterController>().Move(Vector3.forward * 10);

            //var sword = ObjectPoolManager.Instance.GetObject("Hajj_SkillSword");

            meshFilter.mesh = swordMesh;
            meshRenderer.material = swordMat;
            
            //sword.transform.SetParent(transform.parent);
            //sword.GetComponent<RememberPosition>().SetPos();
            //sword.SetActive(true);

            StartCoroutine(SetSwordOff(animationClip.length));

            PlaySkillAnimation(_mainModule, animationClip);
            UseMana(_mainModule, usingMana);
            GetBuff(_mainModule);
        }

        IEnumerator SetSwordOff(float _time)
        {
            yield return new WaitForSeconds(_time);
            meshFilter.mesh = originMesh;
            meshRenderer.material = originMat;
        }

        public HitBoxAction GetHitBoxAction()
        {
            return hitBoxAction;
        }

        public void Hit(int _a)
        {
            //Debug.LogError("�������� ��������");
        }

        public void HitEffect(Vector2 _vector2)
        {
            //GameObject obj = ObjectPoolManager.Instance.GetObject("FireEffect_1");
            //obj.SetActive(true);
            //obj.transform.position = transform.position;
        }
    }
}
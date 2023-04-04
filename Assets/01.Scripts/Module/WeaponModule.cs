using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Weapon;
using HitBox;
using System;
using Effect;

namespace Module
{
    public class WeaponModule : AbBaseModule
    {
        #region public 프로퍼티
        public WeaponSpownObject[] WeaponRight
        {
            get
            {
                weaponRight = mainModule.GetComponentsInChildren<WeaponSpownObject>();
                return weaponRight;
            }
        }
        public AttackModule AttackModule
        {
            get
            {
                attackModule ??= MainModule.GetModuleComponent<AttackModule>(ModuleType.Attack);
                return attackModule;
            }
        }
        public BaseWeapon BaseWeapon => baseWeapon;
        public CurrentArrowInfo CurrentArrowInfo => currentArrowInfo;
        #endregion

        public GameObject currentWeapon;
        public bool isProjectileWeapon;

        private StatModule StatModule
        {
            get
            {
                stateModule ??= mainModule.GetModuleComponent<StatModule>(ModuleType.Stat);
                return stateModule;
            }
        }
        public Animator Animator
        {
            get
            {
                try
                {
                    animator ??= mainModule.GetComponentInChildren<Animator>();
                }
                catch
                {
                    animator = mainModule.GetComponentInChildren<Animator>();
                }
                return animator;
            }
            set
            {
                animator = value;
            }
        }

        private SkillModule SkillModule
        {
            get
            {
                skillModule ??= mainModule.GetModuleComponent<SkillModule>(ModuleType.Skill);
                return skillModule;
            }
        }

        #region private 변수
        private int animationIndex;
        private string currentWeaponName;

        private CurrentArrowInfo currentArrowInfo = new CurrentArrowInfo("Arrow");
        private AttackModule attackModule;
        private BaseWeapon baseWeapon;
        private ProjectileGenerator projectileGenerator;

        private SkillModule skillModule;
        private StatModule stateModule;
        private Animator animator;
        private WeaponSpownObject[] weaponRight;
        private IWeaponSkill iWeaponSkills;
        #endregion
        //private ArrowInfo arrowInfo;

        public WeaponModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }
        public WeaponModule() : base()
        {

        }
        public override void Init(AbMainModule _mainModule, params string[] _parameters)
        {
            base.Init(_mainModule, _parameters);
        }

        public override void Start()
        {
            animationIndex = int.MaxValue;
            currentArrowInfo.arrowAddress = "Arrow";
            currentArrowInfo.action = null;

            projectileGenerator = mainModule.GetComponent<ProjectileGenerator>();
        }

        public void ChangeWeapon(string weapon, string animationName)
        {
            SetAnimation(animationName);
            mainModule.IsWeaponExist = false;

            if (currentWeapon != null)
            {
                currentWeapon.SetActive(false);
                ObjectPoolManager.Instance.RegisterObject(currentWeaponName, currentWeapon);
            }

            if (weapon != null && weapon != "")
            {
                GameObject _weapon = ObjectPoolManager.Instance.GetObject(weapon);
                _weapon.SetActive(true);

                string tagname = mainModule.tag == "Player" ? "Player_Weapon" : "EnemyWeapon";
                _weapon.tag = tagname;

                SkillModule.SetWeaponSkill(_weapon.GetComponent<IWeaponSkill>());
                baseWeapon = _weapon.GetComponent<BaseWeapon>();

                _weapon.transform.SetParent(WhichHandToHold(BaseWeapon));
                _weapon.transform.localPosition = BaseWeapon.WeaponPositionSO.GetWeaponPoritionData(mainModule.name.Trim()).weaponPosition;
                _weapon.transform.localRotation = BaseWeapon.WeaponPositionSO.GetWeaponPoritionData(mainModule.name.Trim()).weaponRotation;

                string _tagname = mainModule.tag == "Player" ? "Enemy" : "Player";
                BaseWeapon.tagName = _tagname;
                mainModule.GetComponent<HitBoxOnAnimation>()?.ChangeSO(BaseWeapon.HitBoxDataSO);
                projectileGenerator?.ChangeSO(BaseWeapon.ProjectilePositionSO);
                mainModule.GetComponent<AnimationOnEffect>().ChangeSO(BaseWeapon.AnimationEffectSO);

                StatModule.SetAttackDamage(BaseWeapon.WeaponDataSO);

                mainModule.StatData.ManaRegen = BaseWeapon.WeaponDataSO.chargeMana;

                currentWeaponName = weapon;
                currentWeapon = _weapon;

                Animator.SetBool("CanCharge", BaseWeapon.WeaponDataSO.canCharge);

                isProjectileWeapon = BaseWeapon.isProjectile;

                //if(isProjectileWeapon) 
                AttackModule.ProjectileName = BaseWeapon.WeaponDataSO.projectileObjectName;

                mainModule.IsConsecutiveWeapon = BaseWeapon.isConsecutiveWeapon;
                mainModule.IsWeaponExist = true;
                SetBehaveAnimation();
            }
        }
        private Transform WhichHandToHold(BaseWeapon _baseWeapon)
        {
            //_baseWeapon.weaponHand
            foreach(WeaponSpownObject _hand in WeaponRight)
            {
                    if (_hand.weaponHand == _baseWeapon.weaponHand)
                    return _hand.transform;
            }

            return null;
        }
        private void SetAnimation(string animationName)
        {
            if (animationIndex != int.MaxValue)
                Animator.SetLayerWeight(animationIndex, 0);

            animationIndex = int.MaxValue;

            if (!string.IsNullOrEmpty(animationName))
            {
                animationIndex = Animator.GetLayerIndex(animationName);

                Animator.SetLayerWeight(animationIndex, 1);
            }
        }
        private void SetBehaveAnimation()
        {
            int count = 0;
            foreach (var _attackClip in BaseWeapon.WeaponDataSO.attackAnimation)
            {
                MainModule.AnimatorOverrideController["Attack" + count.ToString()] = _attackClip;
                count++;
            }

            MainModule.AnimatorOverrideController["StrongAttack"] = BaseWeapon.WeaponDataSO.strongAttackAnimation;
            MainModule.AnimatorOverrideController["Ready"] = BaseWeapon.WeaponDataSO.readyAttackAnimation;
            MainModule.AnimatorOverrideController["ChargeAttack"] = BaseWeapon.WeaponDataSO.chargeAttackAnimation;
        }
        public void SetArrow(string _name, Action _action)
        {
            currentArrowInfo.arrowAddress = _name;
            currentArrowInfo.action = _action;
        }
        public override void OnDisable()
        {
            currentWeapon = null;
            //weaponSkills = null;
            baseWeapon = null;
            stateModule = null;
            animator = null;
            weaponRight = null;
            mainModule = null;
            base.OnDisable();
            ClassPoolManager.Instance.RegisterObject<WeaponModule>("WeaponModule", this);
        }
        public override void OnDestroy()
        {
            currentWeapon = null;
            //weaponSkills = null;
            baseWeapon = null;
            stateModule = null;
            animator = null;
            weaponRight = null;
            mainModule = null;
            base.OnDestroy();
            ClassPoolManager.Instance.RegisterObject<WeaponModule>("WeaponModule", this);
        }
    }
}
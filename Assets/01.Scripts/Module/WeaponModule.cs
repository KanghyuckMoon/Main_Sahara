using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Weapon;
using HitBox;

namespace Module
{
    public class WeaponModule : AbBaseModule
    {
        public WeaponSpownObject[] WeaponRight
        {
            get
            {
                weaponRight ??= mainModule.GetComponentsInChildren<WeaponSpownObject>();
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
 
        public GameObject currentWeapon;
        public WeaponSkills weaponSkills;
        

        public bool isProjectileWeapon;

        private int animationIndex;
        private string currentWeaponName;

        private CurrentArrowInfo currentArrowInfo = new CurrentArrowInfo("Arrow", WeaponHand.RightHand);
        private AttackModule attackModule;
        private Animator Animator
        {
            get
            {
                animator ??= mainModule.GetModuleComponent<AnimationModule>(ModuleType.Animation).animator;
                return animator;
            }
        }
        private BaseWeapon baseWeapon;

        private StatModule StateModule
        {
            get
            {
                stateModule ??= mainModule.GetModuleComponent<StatModule>(ModuleType.Stat);
                return stateModule;
            }
        }
        private StatModule stateModule;
        private Animator animator;
        private WeaponSpownObject[] weaponRight;
        private IWeaponSkills iWeaponSkills;
        //private ArrowInfo arrowInfo;

        public WeaponModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            animationIndex = int.MaxValue;
            currentArrowInfo.arrowAddress = "Arrow";
            currentArrowInfo.weaponHand = WeaponHand.RightHand;
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

            if (weapon != "")
            {
                GameObject _weapon = ObjectPoolManager.Instance.GetObject(weapon);
                _weapon.SetActive(true);

                string tagname = mainModule.tag == "Player" ? "Player_Weapon" : "EnemyWeapon";
                _weapon.tag = tagname;

                iWeaponSkills = _weapon.GetComponent<IWeaponSkills>();
                baseWeapon = _weapon.GetComponent<BaseWeapon>();

                _weapon.transform.SetParent(WhichHandToHold(BaseWeapon));
                _weapon.transform.localPosition = BaseWeapon.WeaponPositionSO.GetWeaponPoritionData(mainModule.gameObject.name.Trim()).weaponPosition;
                _weapon.transform.localRotation = BaseWeapon.WeaponPositionSO.GetWeaponPoritionData(mainModule.gameObject.name.Trim()).weaponRotation;

                string _tagname = mainModule.tag == "Player" ? "Enemy" : "Player";
                BaseWeapon.tagName = _tagname;
                mainModule.GetComponent<HitBoxOnAnimation>().ChangeSO(BaseWeapon.HitBoxDataSO);

                StateModule.SetAttackDamage(BaseWeapon.WeaponDataSO);

                currentWeaponName = weapon;
                currentWeapon = _weapon;

                Animator.SetBool("CanCharge", BaseWeapon.WeaponDataSO.canCharge);

                isProjectileWeapon = BaseWeapon.isProjectile;

                //if(isProjectileWeapon) 
                AttackModule.ProjectileName = BaseWeapon.WeaponDataSO.projectileObjectName;

                mainModule.IsWeaponExist = true;
                SetBehaveAnimation();
                SetWeaponSkills();
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

            if (animationName != "")
            {
                animationIndex = Animator.GetLayerIndex(animationName);

                Animator.SetLayerWeight(animationIndex, 1);
            }
        }
        private void SetBehaveAnimation()
        {
            Debug.Log(MainModule.AnimatorOverrideController);
            AnimationClip _a = BaseWeapon.WeaponDataSO.attackAnimation;
            MainModule.AnimatorOverrideController["Attack"] = _a;
            MainModule.AnimatorOverrideController["StrongAttack"] = BaseWeapon.WeaponDataSO.strongAttackAnimation;
            MainModule.AnimatorOverrideController["Ready"] = BaseWeapon.WeaponDataSO.readyAttackAnimation;
            MainModule.AnimatorOverrideController["ChargeAttack"] = BaseWeapon.WeaponDataSO.chargeAttackAnimation;
        }
        private void SetWeaponSkills()
        {
            if (iWeaponSkills is not null)
            {
                weaponSkills = new WeaponSkills(iWeaponSkills.Skills);
            }
        }
        public void UseWeaponSkills()
        {
            if (baseWeapon is null)
            {
                return;
            }

            //if (StateModule.Mana >= baseWeapon.WeaponDataSO.manaConsumed)
            //{
            //    weaponSkills?.Invoke();
            //    StateModule.Mana -= baseWeapon.WeaponDataSO.manaConsumed;
            //}
        }
    }
}
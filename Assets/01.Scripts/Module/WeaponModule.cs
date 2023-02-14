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
        public GameObject currentWeapon;
        public BaseWeapon BaseWeapon => baseWeapon;

        public WeaponSkills weaponSkills;




        private int animationIndex;

        private string currentWeaponName;

        private GameObject WeaponRight
        {
            get
            {
                weaponRight ??= mainModule.GetComponentInChildren<WeaponSpownObject>().gameObject;
                return weaponRight;
            }
        }
        private Animator Animator
        {
            get
            {
                animator ??= mainModule.GetModuleComponent<AnimationModule>(ModuleType.Animation).animator;
                return animator;
            }
        }
        private BaseWeapon baseWeapon;

        private StateModule StateModule
        {
            get
            {
                stateModule ??= mainModule.GetModuleComponent<StateModule>(ModuleType.State);
                return stateModule;
            }
        }
        private StateModule stateModule;
        private Animator animator;
        private GameObject weaponRight;
        private IWeaponSkills iWeaponSkills;

        public WeaponModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            animationIndex = int.MaxValue;
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

                _weapon.transform.SetParent(WeaponRight.transform);

                string tagname = mainModule.tag == "Player" ? "Player_Weapon" : "EnemyWeapon";
                _weapon.tag = tagname;

                iWeaponSkills = _weapon.GetComponent<IWeaponSkills>();
                baseWeapon = _weapon.GetComponent<BaseWeapon>();
                
                _weapon.transform.localPosition = baseWeapon.WeaponPositionSO.weaponPosition;
                _weapon.transform.localRotation = baseWeapon.WeaponPositionSO.weaponRotation;

                string _tagname = mainModule.tag == "Player" ? "Enemy" : "Player";
                baseWeapon.tagName = _tagname;
                mainModule.gameObject.GetComponent<HitBoxOnAnimation>().ChangeSO(baseWeapon.HitBoxDataSO);

                StateModule.SetAttackDamage(baseWeapon.WeaponDataSO);

                currentWeaponName = weapon;
                currentWeapon = _weapon;

                mainModule.IsWeaponExist = true;
                SetWeaponSkills();
            }
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

            if (StateModule.Mana >= baseWeapon.WeaponDataSO.manaConsumed)
            {
                weaponSkills?.Invoke();
                StateModule.Mana -= baseWeapon.WeaponDataSO.manaConsumed;
            }
        }
    }
}
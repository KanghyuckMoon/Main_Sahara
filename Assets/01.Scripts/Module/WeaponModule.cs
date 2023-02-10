using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Weapon;

namespace Module
{
    public class WeaponModule : AbBaseModule
    {
        public GameObject currentWeapon;
        public BaseWeapon BaseWeapon => baseWeapon;

        public WeaponSkills weaponSkills;




        private int animationIndex;

        private string currentWeaponName;

        private GameObject weaponRight;
        private Animator animator;
        private BaseWeapon baseWeapon;
        private StateModule stateModule;

        private IWeaponSkills iWeaponSkills;

        public WeaponModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            weaponRight = mainModule.GetComponentInChildren<WeaponSpownObject>().gameObject;//.Find("Weapon_r").gameObject;
            animator = mainModule.GetModuleComponent<AnimationModule>(ModuleType.Animation).animator;
            stateModule = mainModule.GetModuleComponent<StateModule>(ModuleType.State);
            animationIndex = int.MaxValue;
        }

        public void ChangeWeapon(string weapon, string animationName)
        {
            SetAnimation(animationName);
            mainModule.isWeaponExist = false;

            if (currentWeapon != null)
            {
                currentWeapon.SetActive(false);
                ObjectPoolManager.Instance.RegisterObject(currentWeaponName, currentWeapon);
            }

            if (weapon != "")
            {
                GameObject _weapon = ObjectPoolManager.Instance.GetObject(weapon);
                _weapon.SetActive(true);

                _weapon.transform.SetParent(weaponRight.transform);

                string tagname = mainModule.tag == "Player" ? "Player_Weapon" : "EnemyWeapon";
                _weapon.tag = tagname;

                iWeaponSkills = _weapon.GetComponent<IWeaponSkills>();
                baseWeapon = _weapon.GetComponent<BaseWeapon>();
                _weapon.transform.localPosition = baseWeapon.WeaponPositionSO.weaponPosition;
                _weapon.transform.localRotation = baseWeapon.WeaponPositionSO.weaponRotation;

                string _tagname = mainModule.tag == "Player" ? "Enemy" : "Player";
                baseWeapon.tagName = _tagname;

                stateModule.SetAttackDamage(baseWeapon.WeaponDataSO);

                currentWeaponName = weapon;
                currentWeapon = _weapon;

                mainModule.isWeaponExist = true;
                SetWeaponSkills();
            }
        }
        private void SetAnimation(string animationName)
        {
            if (animationIndex != int.MaxValue)
                animator.SetLayerWeight(animationIndex, 0);

            animationIndex = int.MaxValue;

            if (animationName != "")
            {
                animationIndex = animator.GetLayerIndex(animationName);

                animator.SetLayerWeight(animationIndex, 1);
            }
        }
        private void SetWeaponSkills()
        {
            weaponSkills = new WeaponSkills(iWeaponSkills.Skills);
        }

        public void UseWeaponSkills()
        {
            if (stateModule.Mana >= baseWeapon.WeaponDataSO.manaConsumed)
            {
                weaponSkills?.Invoke();
                stateModule.Mana -= baseWeapon.WeaponDataSO.manaConsumed;
            }
        }
    }
}
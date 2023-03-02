using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.ExtraStruct;
using Effect;
using Weapon;
using Utill.Addressable;
using Utill.Pattern;
using Pool;

namespace Module
{
    public class AttackModule : AbBaseModule
    {
        public GameObject ProjectileObject
        {
            get
            {
                return projectileObject;
            }
            set
            {
                projectileObject = value;
            }
        }
        public string ProjectileName
        {
            get
            {
                return projectileName;
            }
            set
            {
                projectileName = value;
            }
        }

        private WeaponModule WeaponModule
        {
            get
            {
                weaponModule ??= mainModule.GetModuleComponent<WeaponModule>(ModuleType.Weapon);
                return weaponModule;
            }
        }

        private WeaponModule weaponModule;
        private GameObject projectileObject;

        private string projectileName;

        public AttackModule(AbMainModule _mainModule) : base(_mainModule)
        {
        }

        public void SpownCurrentArrow()
        {
            CreateProjectile(WeaponModule.CurrentArrowInfo.weaponHand, ProjectileName);
        }

        public GameObject CreateProjectile(WeaponHand _weaponHand, string _projectileObjectName)
        {
            if (WeaponModule.isProjectileWeapon)
            {
                GameObject _projectile = ObjectPoolManager.Instance.GetObject(_projectileObjectName);

                _projectile.transform.SetParent(WhichHandToHold(_weaponHand));
                ProjectileObject _projectileObject = _projectile.GetComponent<ProjectileObject>();

                _projectile.SetActive(true);
                //Debug.LogError(mainModule.name + "     :     " + mainModule.gameObject.name);
                _projectile.transform.localPosition = _projectileObject.weaponPositionSO.GetWeaponPoritionData(mainModule.name).weaponPosition;
                _projectile.transform.localRotation = _projectileObject.weaponPositionSO.GetWeaponPoritionData(mainModule.name).weaponRotation;

                ProjectileObject = _projectile;
            }
            else
            {
                ProjectileObject = null;
            }
            return ProjectileObject;
        }

        private Transform WhichHandToHold(WeaponHand _weaponHand)
        {
            foreach (WeaponSpownObject _hand in WeaponModule.WeaponRight)
            {
                if (_hand.weaponHand == _weaponHand)
                    return _hand.transform;
            }
            return null;
        }

            public void SpownAttackEffect()
        {
            if (mainModule.Attacking)
            {
                EffectManager.Instance.SetEffectDefault(WeaponModule.BaseWeapon.WeaponDataSO.attackEffectName, mainModule.transform.position + (new Vector3(0, 1.4f, 0) + mainModule.transform.forward), Quaternion.identity);//SetEffectDefault();
            }
            else if (mainModule.StrongAttacking)
            {
                EffectManager.Instance.SetEffectDefault(WeaponModule.BaseWeapon.WeaponDataSO.strongAttackEffectName, mainModule.transform.position + (new Vector3(0, 1.4f, 0) + mainModule.transform.forward), Quaternion.identity);//SetEffectDefault();

            }
        }
    }
}
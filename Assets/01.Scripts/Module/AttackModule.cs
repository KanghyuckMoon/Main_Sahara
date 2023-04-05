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

        public HitBoxAction HitBoxAction
        {
            get
            {
                hitBoxAction ??= new HitBoxAction();
                return hitBoxAction;
            }
        }

        private WeaponModule weaponModule;
        private GameObject projectileObject;
        private HitBoxAction hitBoxAction;

        private string projectileName;

        public AttackModule(AbMainModule _mainModule) : base(_mainModule)
        {
        }
        public AttackModule() : base()
        {
        }

        public void SpownCurrentArrow()
        {
            //CreateProjectile(WeaponModule.CurrentArrowInfo.weaponHand, ProjectileName);
        }

        public GameObject CreateProjectile(ProjectileObjectData _projectileObjectData)
        {
            if (WeaponModule.isProjectileWeapon)
            {
                string _name = _projectileObjectData.projectileName == "Arrow" ? WeaponModule.CurrentArrowInfo.arrowAddress : _projectileObjectData.projectileName;
                GameObject _projectile = ObjectPoolManager.Instance.GetObject(_name);

                if (_projectileObjectData.projectileName == "Arrow") WeaponModule.CurrentArrowInfo.action?.Invoke();

                _projectile.transform.SetParent(WhichHandToHold(_projectileObjectData.weaponHand));
                ProjectileObject _projectileObject = _projectile.GetComponent<ProjectileObject>();

                _projectile.SetActive(true);
                //Debug.LogError(mainModule.name + "     :     " + mainModule.gameObject.name);
                _projectile.transform.localPosition = _projectileObjectData.position;
                _projectile.transform.localRotation = _projectileObjectData.rotation;

                _projectileObject.objectData = _projectileObjectData;

                ProjectileObject = _projectile;
            }
            else
            {
                Debug.LogError("None WeaponModule Or NotIsProjectileWeapon", mainModule.gameObject);
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
            if (mainModule.Attacking && !string.IsNullOrEmpty(WeaponModule.BaseWeapon.WeaponDataSO.attackEffectName))
            {
                EffectManager.Instance.SetEffectDefault(WeaponModule.BaseWeapon.WeaponDataSO.attackEffectName, mainModule.transform.position + (new Vector3(0, 1.4f, 0) + mainModule.transform.forward), Quaternion.identity);//SetEffectDefault();
            }
            else if (mainModule.StrongAttacking && !string.IsNullOrEmpty(WeaponModule.BaseWeapon.WeaponDataSO.strongAttackEffectName))
            {
                EffectManager.Instance.SetEffectDefault(WeaponModule.BaseWeapon.WeaponDataSO.strongAttackEffectName, mainModule.transform.position + (new Vector3(0, 1.4f, 0) + mainModule.transform.forward), Quaternion.identity);//SetEffectDefault();

            }
        }

		public override void OnDisable()
        {
            weaponModule = null;
            projectileObject = null;
            mainModule = null;
			base.OnDisable();
            Pool.ClassPoolManager.Instance.RegisterObject<AttackModule>("AttackModule", this);
		}
	}
}
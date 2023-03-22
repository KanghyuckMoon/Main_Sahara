using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using HitBox;

namespace Weapon
{
    public delegate void WeaponSkills();

    public class BaseWeapon : MonoBehaviour
    {
        public WeaponHand weaponHand;
        public string WeaponColKey
        {
            get
            {
                return weaponName + weaponColKey;
            }
        }
        
        public string weaponName;
        public string tagName;

        public WeaponPositionSO WeaponPositionSO
        {
            get
            {
                weaponPositionSO ??= AddressablesManager.Instance.GetResource<WeaponPositionSO>(weaponName + weaponPosStr);
                return weaponPositionSO;
            }
        }

        public WeaponDataSO WeaponDataSO
        {
            get
            {
                weaponDataSO ??= AddressablesManager.Instance.GetResource<WeaponDataSO>(weaponName + weaponDataStr);
                return weaponDataSO;
            }
        }
        public HitBoxDatasSO HitBoxDataSO => hitBoxDataSO;

        public ProjectilePositionSO ProjectilePositionSo
        {
            get
            {
                projectilePositionSo ??= AddressablesManager.Instance.GetResource<ProjectilePositionSO>(weaponDataSO.projectileObjectName + weaponPosStr);
                return projectilePositionSo;
            }
        }

        public bool isProjectile;

        private WeaponDataSO weaponDataSO;
        private WeaponPositionSO weaponPositionSO;
        private ProjectilePositionSO projectilePositionSo;
        private string weaponPosStr = "_Position";
        private string weaponDataStr = "_Data";
        private string weaponColKey = "_ColKey";

        [SerializeField]
        private HitBoxDatasSO hitBoxDataSO;
        //[SerializeField]
        //private ProjectilePositionSO projectilePositionSO;

        private void Awake()
        {
            //weaponSkills = new WeaponSkills();
            weaponPositionSO ??= AddressablesManager.Instance.GetResource<WeaponPositionSO>(weaponName + weaponPosStr);
            weaponDataSO ??= AddressablesManager.Instance.GetResource<WeaponDataSO>(weaponName + weaponDataStr);
            projectilePositionSo ??= AddressablesManager.Instance.GetResource<ProjectilePositionSO>(weaponDataSO.projectileObjectName + weaponPosStr);
        }

        [ContextMenu("??? ????")]
        public void Upload()
        {
            WeaponPositionData _weaponPositionData = new WeaponPositionData();

            _weaponPositionData.objectName = GetComponentInParent<CharacterController>().name.Trim();
            _weaponPositionData.weaponPosition = transform.localPosition;
            _weaponPositionData.weaponRotation = transform.localRotation;

            weaponPositionSO ??= AddressablesManager.Instance.GetResource<WeaponPositionSO>(weaponName + weaponPosStr);
            weaponPositionSO.UploadWeaponPositionData(_weaponPositionData);
        }
    }
}
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
        public string WeaponColKey
        {
            get
            {
                return weaponName + weaponColKey;
            }
        }
        
        public string weaponName;
        public string tagName;
        public WeaponPositionSO WeaponPositionSO => weaponPositionSO;
        public WeaponDataSO WeaponDataSO => weaponDataSO;
        public HitBoxDatasSO HitBoxDataSO => hitBoxDataSO;

        private WeaponDataSO weaponDataSO;
        private WeaponPositionSO weaponPositionSO;
        private string weaponPosStr = "_Position";
        private string weaponDataStr = "_Data";
        private string weaponColKey = "_ColKey";

        [SerializeField]
        private HitBoxDatasSO hitBoxDataSO;

        private void Awake()
        {
            //weaponSkills = new WeaponSkills();
            weaponPositionSO = AddressablesManager.Instance.GetResource<WeaponPositionSO>(weaponName + weaponPosStr);
            weaponDataSO = AddressablesManager.Instance.GetResource<WeaponDataSO>(weaponName + weaponDataStr);
        }
    }
}
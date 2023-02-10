using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;

namespace Weapon
{
    public delegate void WeaponSkills();

    public class BaseWeapon : MonoBehaviour
    {
        
        public string weaponName;
        public string tagName;
        public WeaponPositionSO WeaponPositionSO => weaponPositionSO;
        public WeaponDataSO WeaponDataSO => weaponDataSO;

        private WeaponDataSO weaponDataSO;
        private WeaponPositionSO weaponPositionSO;
        private string weaponPosStr = "_Position";
        private string weaponDataStr = "_Data";

        private void Awake()
        {
            //weaponSkills = new WeaponSkills();
            weaponPositionSO = AddressablesManager.Instance.GetResource<WeaponPositionSO>(weaponName + weaponPosStr);
            weaponDataSO = AddressablesManager.Instance.GetResource<WeaponDataSO>(weaponName + weaponDataStr);
        }
    }
}
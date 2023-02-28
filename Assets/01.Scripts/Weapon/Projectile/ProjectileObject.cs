using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;

namespace Weapon
{
    public class ProjectileObject : MonoBehaviour
    {
        public WeaponPositionSO weaponPosition;
        public WeaponHand weaponHand;

        [SerializeField, Header("무기 이름")]
        private string objectName;
        private string positionString = "_Position";

        private void Awake()
        {
            weaponPosition = AddressablesManager.Instance.GetResource<WeaponPositionSO>(objectName + positionString);
        }
    }
}
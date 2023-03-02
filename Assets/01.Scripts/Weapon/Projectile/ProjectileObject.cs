using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;

namespace Weapon
{
    public class ProjectileObject : MonoBehaviour
    {
        public WeaponPositionSO weaponPositionSO;
        public WeaponHand weaponHand;

        [SerializeField, Header("���� �̸�")]
        private string objectName;
        private string positionString = "_Position";

        private void Awake()
        {
            weaponPositionSO = AddressablesManager.Instance.GetResource<WeaponPositionSO>(objectName + positionString);
        }

        [ContextMenu("��ġ ����")]
        public void Upload()
        {
            WeaponPositionData _weaponPositionData = new WeaponPositionData();

            _weaponPositionData.objectName = GetComponentInParent<CharacterController>().name.Trim();
            _weaponPositionData.weaponPosition = transform.position;
            _weaponPositionData.weaponRotation = transform.rotation;

            weaponPositionSO = AddressablesManager.Instance.GetResource<WeaponPositionSO>(objectName + positionString);
            weaponPositionSO.UploadWeaponPositionData(_weaponPositionData);
        }
    }
}
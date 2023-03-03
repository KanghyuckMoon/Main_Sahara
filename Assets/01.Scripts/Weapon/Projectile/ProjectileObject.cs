using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;

namespace Weapon
{
    public class ProjectileObject : MonoBehaviour
    {
        public ProjectilePositionSO projectilePosSO;
        public WeaponHand weaponHand;

        public ProjectileObjectData objectData;

        [SerializeField, Header("���� �̸�")]
        private string objectName;
        private string positionString = "_Position";

        private void Awake()
        {
            projectilePosSO = AddressablesManager.Instance.GetResource<ProjectilePositionSO>(objectName + positionString);
        }

        [ContextMenu("��ġ ����")]
        public void Upload()
        {
            //WeaponPositionData _weaponPositionData = new WeaponPositionData();

            //_weaponPositionData.objectName = GetComponentInParent<CharacterController>().name.Trim();
            //_weaponPositionData.weaponPosition = transform.localPosition;
            //_weaponPositionData.weaponRotation = transform.localRotation;

            projectilePosSO = AddressablesManager.Instance.GetResource<ProjectilePositionSO>(objectName + positionString);
            projectilePosSO.Upload(objectData);
        }
    }
}
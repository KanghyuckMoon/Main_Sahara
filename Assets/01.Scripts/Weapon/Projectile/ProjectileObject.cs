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


        [SerializeField, Header("저장될 SO이름(_Position뺴고)")]
        private string objectName;
        private string positionString = "_Position";

        private void Awake()
        {
            projectilePosSO = AddressablesManager.Instance.GetResource<ProjectilePositionSO>(objectName + positionString);
        }
        
        public ProjectileObjectData objectData;
        //#if UNITY_EDITOR
        //[Header("Debug")]

        [ContextMenu("위치 저장")]
        public void Upload()
        {
            objectData.position = transform.localPosition;
            objectData.rotation = transform.localRotation;
            
            projectilePosSO ??= AddressablesManager.Instance.GetResource<ProjectilePositionSO>(objectName + positionString);
            projectilePosSO.Upload(objectData);
        }
        //#endif
        
        protected Vector3 CalculateRotation(Vector3 _vector3)
        {
            //Quaternion _qu = _qu;
            //Vector3 _vec = _quaternion * objectData.InitialDirection;
            
            //Vector3 _vec = _rotation.eulerAngles;

            //Quaternion _quaternion = Quaternion.Euler(0, _vec.y, 0);
            Vector3 _vec = _vector3 - transform.position;
            
            
            return _vec;//_quaternion * objectData.InitialDirection;
        }
    }
}
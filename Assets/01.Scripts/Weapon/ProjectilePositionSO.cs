using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.SeralizableDictionary;

namespace Weapon
{
    [System.Serializable]
    public class StringListProjectilePositionData : SerializableDictionary<string, ProjectileObjectDataList> { };

    [CreateAssetMenu(menuName = "SO/ProjectilePositionSO")]
    public class ProjectilePositionSO : ScriptableObject
    {
        public StringListProjectilePositionData projectilePosDic = new StringListProjectilePositionData();

        public ProjectileObjectDataList GetProjectilePosList(string _str)
        {
            if(projectilePosDic.TryGetValue(_str, out var _list))
            {
                return _list;
            }
            return null;
        }

        public void Upload(ProjectileObjectData _projectileObjectData)
        {
            if (_projectileObjectData.animationEventName is null)
                return;

            if(projectilePosDic.TryGetValue(_projectileObjectData.animationEventName, out var _list))
			{
				_list.list.Add(_projectileObjectData);
			}
            else
            {
                ProjectileObjectDataList _projectileObjectDataList = new ProjectileObjectDataList();
                _projectileObjectDataList.list.Add(ProjectileObjectData.StaticCopy(_projectileObjectData));
                projectilePosDic.Add(_projectileObjectData.animationEventName, _projectileObjectDataList);
            }
            
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }

    [System.Serializable]
    public class ProjectileObjectDataList
    {
        public List<ProjectileObjectData> list = new List<ProjectileObjectData>();
    }

    [System.Serializable]
    public class ProjectileObjectData
    {
        public string animationEventName;
        public string projectileAddress;

        //생성 위치
        public Vector3 position;
        public Quaternion rotation;
        public WeaponHand weaponHand;

        [Header("날아가는 방향")]
        public Vector3 InitialDirection;

        public float speed;


        public bool isParentOn = true;

        public bool isSetHitBoxWhenSetProjectile = true;
        
        public static ProjectileObjectData StaticCopy(ProjectileObjectData _projectileObjectData)
        {
            ProjectileObjectData _data = new ProjectileObjectData();

            _data.animationEventName = _projectileObjectData.animationEventName;

            _data.position = _projectileObjectData.position;
            _data.rotation = _projectileObjectData.rotation;
            _data.weaponHand = _projectileObjectData.weaponHand;

            _data.speed = _projectileObjectData.speed;

            _data.projectileAddress = _projectileObjectData.projectileAddress;

            return _data;
        }

        public void Copy(ProjectileObjectData _projectileObjectData)
        {
			animationEventName = _projectileObjectData.animationEventName;

            position = _projectileObjectData.position;
            rotation = _projectileObjectData.rotation;
            weaponHand = _projectileObjectData.weaponHand;

            speed = _projectileObjectData.speed;

            projectileAddress = _projectileObjectData.projectileAddress;
        }
    }
}

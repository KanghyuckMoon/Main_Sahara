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
            if (_projectileObjectData.projectileName is null)
                return;

            if(projectilePosDic.TryGetValue(_projectileObjectData.projectileName, out var _list))
            {
                ProjectileObjectData _data = _list.list.Find(x => x.distinguishingName == _projectileObjectData.distinguishingName);
                if (_data is not null)
                    _data = _projectileObjectData;
                else
                    _list.list.Add(_projectileObjectData);
            }
            else
            {
                ProjectileObjectDataList _projectileObjectDataList = new ProjectileObjectDataList();
                _projectileObjectDataList.list.Add(ProjectileObjectData.Copy(_projectileObjectData));
                projectilePosDic.Add(_projectileObjectData.projectileName, _projectileObjectDataList);
            }
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
        public string projectileName;
        public string distinguishingName;

        //생성 위치
        public Vector3 position;
        public Quaternion rotation;
        public WeaponHand weaponHand;

        public float speed;

        public string projectileAddress;

        public static ProjectileObjectData Copy(ProjectileObjectData _projectileObjectData)
        {
            ProjectileObjectData _data = new ProjectileObjectData();

            _data.projectileName = _projectileObjectData.projectileName;
            _data.distinguishingName = _projectileObjectData.distinguishingName;

            _data.position = _projectileObjectData.position;
            _data.rotation = _projectileObjectData.rotation;
            _data.weaponHand = _projectileObjectData.weaponHand;

            _data.speed = _projectileObjectData.speed;

            return _data;
        }
    }
}

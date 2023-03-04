using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Module
{
    public class ProjectileGenerator : MonoBehaviour
    {
        public ProjectilePositionSO PositionSO
        {
            get
            {
                return positionSO;
            }
        }

        private ProjectilePositionSO positionSO;
        private AttackModule attackModule;

        private List<GameObject> projectileObjects = new List<GameObject>();

        private void Start()
        {
            attackModule = GetComponent<AbMainModule>().GetModuleComponent<AttackModule>(ModuleType.Attack);
        }

        public void ChangeSO(ProjectilePositionSO _positionSO)
        {
            positionSO = _positionSO;
        }

        public void SpownAndMove(string _projectileName)
        {
            SpownProjectile(_projectileName);
            MoveProjectile();
        }

        public void SpownProjectile(string _projectileName)
        {
            //for (int i = 0; i < _count; i++)
            //{
            ProjectileObjectDataList _list = PositionSO.GetProjectilePosList(_projectileName);

            if (_list is not null)
            {
                foreach (ProjectileObjectData _datas in _list.list)
                {
                    //KeyValuePair<GameObject, ProjectileObjectData> keyValuePair = new KeyValuePair(attackModule.CreateProjectile(_datas), _datas);
                    projectileObjects.Add(attackModule.CreateProjectile(_datas));
                }
            }
            //_gameObject?.GetComponent<IProjectile>().MovingFunc(transform.forward, Quaternion.identity);
            //}
        }

        public void MoveProjectile()
        {
            projectileObjects.ForEach(i => i.GetComponent<IProjectile>().MovingFunc());
        }
    }
}
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

        [SerializeField]
        private ProjectilePositionSO positionSO;
        private AttackModule attackModule;
        private AbMainModule mainModule;

        private List<GameObject> projectileObjects = new List<GameObject>();

        private void Start()
        {
            mainModule = GetComponent<AbMainModule>();
            attackModule = mainModule.GetModuleComponent<AttackModule>(ModuleType.Attack);
        }

        public void ChangeSO(ProjectilePositionSO _positionSO)
        {
            positionSO = _positionSO;
        }

        public void SpownAndMove(string _projectileName)
        {
            if (PositionSO is null)
                return;

            SpownProjectile(_projectileName);
            MoveProjectile();
        }

        public void SpownProjectile(string _projectileName)
        {
            //for (int i = 0; i < _count; i++)
            //{
            if (PositionSO is null)
                return;

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
            if (PositionSO is null)
                return;

            //Quaternion _quaternion = Quaternion.Euler(transform.forward);

            projectileObjects.ForEach(i => i.GetComponent<IProjectile>().MovingFunc(mainModule.ObjRotation));

            projectileObjects.Clear();
        }
    }
}
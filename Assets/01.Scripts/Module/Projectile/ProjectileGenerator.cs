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
        private StateModule stateModule;

        private float delay = 1;
        private bool canSpwon;

        private List<GameObject> projectileObjects = new List<GameObject>();

        private void Start()
        {
            mainModule = GetComponent<AbMainModule>();
            attackModule = mainModule.GetModuleComponent<AttackModule>(ModuleType.Attack);
            stateModule = mainModule.GetModuleComponent<StateModule>(ModuleType.State);
        }

        public void ChangeSO(ProjectilePositionSO _positionSO)
        {
            positionSO = _positionSO;
        }

        public void SpownAndMove()
        {
            //if (stateModule.CheckState(State.ATTACK)) return;
            if (canSpwon) return;
            if (PositionSO is null)
                return;

            SpownProjectile();
            MoveProjectile();
        }

        public void SpownProjectile()
        {
            //for (int i = 0; i < _count; i++)
            //{
            if (canSpwon) return;
            if (PositionSO is null)
                return;

            //if (stateModule.CheckState(State.ATTACK)) return;
            Delay();

            ProjectileObjectDataList _list = PositionSO.GetProjectilePosList(attackModule.ProjectileName);

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
            //if (stateModule.CheckState(State.ATTACK)) return;
            if (canSpwon) return;
            if (PositionSO is null)
                return;
        
            //Quaternion _quaternion = Quaternion.Euler(transform.forward);

            projectileObjects.ForEach(i => i.GetComponent<IProjectile>().MovingFunc(mainModule.ObjRotation));

            projectileObjects.Clear();
        }

        IEnumerator Delay()
        {
            canSpwon = true;
            yield return new WaitForSeconds(delay);
            canSpwon = false;
        }
    }
}
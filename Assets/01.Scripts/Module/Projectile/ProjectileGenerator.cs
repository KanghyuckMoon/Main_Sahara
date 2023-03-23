using System.Collections;
using System.Collections.Generic;
using HitBox;
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
        [SerializeField]
        private LayerMask targetLayerMask;
        private AttackModule attackModule;
        private AbMainModule mainModule;
        private StateModule stateModule;
        private CameraModule cameraModule;
        private WeaponModule weaponModule;

        private float delay = 1;
        private bool canSpwon;

        private List<GameObject> projectileObjects = new List<GameObject>();

        private void Start()
        {
            mainModule = GetComponent<AbMainModule>();
            attackModule = mainModule.GetModuleComponent<AttackModule>(ModuleType.Attack);
            stateModule = mainModule.GetModuleComponent<StateModule>(ModuleType.State);
            weaponModule = mainModule.GetModuleComponent<WeaponModule>(ModuleType.Weapon);
            cameraModule = mainModule.GetModuleComponent<CameraModule>(ModuleType.Camera);
        }

        public void ChangeSO(ProjectilePositionSO _positionSO)
        {
            positionSO = _positionSO;
        }

        public void SpownAndMove(string _projectileName)
        {
            //if (stateModule.CheckState(State.ATTACK)) return;
            if (canSpwon) return;
            if (positionSO == null)
                return;
            SpownProjectile(_projectileName);
            MoveProjectile();
        }

        public void SpownProjectile(string _projectileName)
        {
            //for (int i = 0; i < _count; i++)
            //{
            if (canSpwon) return;
            if (positionSO == null)
                return;

            //if (stateModule.CheckState(State.ATTACK)) return;
            Delay();

            ProjectileObjectDataList _list = PositionSO.GetProjectilePosList(_projectileName);

            if (_list is not null)
            {
                foreach (ProjectileObjectData _datas in _list.list)
                {
                    //KeyValuePair<GameObject, ProjectileObjectData> keyValuePair = new KeyValuePair(attackModule.CreateProjectile(_datas), _datas);
                    GameObject _projectile = attackModule.CreateProjectile(_datas);
                    HitBoxOnProjectile _hitProj = _projectile.GetComponent<HitBoxOnProjectile>();
                    _hitProj.SetOwner(gameObject);
                    _projectile.tag = mainModule.player ? "Player" : "EnemyWeapon";
                    projectileObjects.Add(_projectile);
                    _hitProj.SetEnable();
                    //projectileObjects
                }
            }
            //_gameObject?.GetComponent<IProjectile>().MovingFunc(transform.forward, Quaternion.identity);
            //}
        }

        public void MoveProjectile()
        {
            //if (stateModule.CheckState(State.ATTACK)) return;
            if (!weaponModule.isProjectileWeapon) return;
            if (canSpwon) return;
            if (PositionSO is null)
                return;
        
            //Quaternion _quaternion = Quaternion.Euler(transform.forward);

            /*projectileObjects.ForEach((x) =>
            {
                x.GetComponent<IProjectile>().MovingFunc(mainModule.ObjRotation);
            });*/

            Vector3 _vec;
            
            
            
            //if (mainModule.LockOnTarget is not null)
            //    _vec = mainModule.LockOnTarget.position + new Vector3(0,1, 0);
            //else
            {
                Ray _ray = new Ray(cameraModule.CurrentCamera.transform.position, cameraModule.CurrentCamera.transform.forward);
                RaycastHit _raycastHit;

                _vec = cameraModule.CurrentCamera.transform.position + cameraModule.CurrentCamera.transform.forward * 40f;
                //if (Physics.Raycast(_ray, out _raycastHit, 40f))
                //    _vec = _raycastHit.point;
                //else

            }
            
            foreach (GameObject _projectile in projectileObjects)
            {
                _projectile.GetComponent<IProjectile>().MovingFunc(_vec);
            }

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
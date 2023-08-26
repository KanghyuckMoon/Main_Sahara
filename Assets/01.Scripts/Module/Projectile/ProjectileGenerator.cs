using System.Collections;
using System.Collections.Generic;
using HitBox;
using UnityEngine;
using Weapon;
using Pool;

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

        public bool IsUseWeapon
        {
            get
            {
                return isUseWeapon;
            }
            set
            {
                isUseWeapon = value;
            }
        }

        [SerializeField]
        private ProjectilePositionSO positionSO;
        [SerializeField]
        private LayerMask targetLayerMask;
        [SerializeField]
        private bool isUseWeapon;
        [SerializeField]
        private Transform noneUseWeaponTargetTrm;

		private WeaponModule weaponModule;
        private AbMainModule mainModule;
        private StateModule stateModule;
        private CameraModule cameraModule;

        [SerializeField, Header("Enemy Only")] private bool isUseHeight;
        [SerializeField, Header("IsUseHeightOn")] private float height;

        private float delay = 0.1f;
        private bool canSpwon;

        private List<GameObject> projectileObjects = new List<GameObject>();

        private void OnEnable()
        {
            mainModule = GetComponent<AbMainModule>();
            if(isUseWeapon)
            {
                weaponModule = mainModule.GetModuleComponent<WeaponModule>(ModuleType.Weapon);
            }

            stateModule = mainModule.GetModuleComponent<StateModule>(ModuleType.State);
            cameraModule = mainModule.GetModuleComponent<CameraModule>(ModuleType.Camera);
        }

        public void ChangeSO(ProjectilePositionSO _positionSO)
        {
            positionSO = _positionSO;
        }

        public void SpownAndMove(string _projectileName)
        {
            if (canSpwon) 
                return;
            if (positionSO == null)
                return;
            SpownProjectile(_projectileName);
            MoveProjectile();
        }

        public void SpownProjectile(string _animationEvent)
        {
            if (canSpwon) 
                return;
            if (positionSO == null)
                return;

            StartCoroutine(Delay());

            
            ProjectileObjectDataList _list = PositionSO.GetProjectilePosList(_animationEvent);

            if (_list is not null)
            {
                foreach (ProjectileObjectData _datas in _list.list)
                {
                    GameObject _projectile = CreateProjectile(_datas);
                    HitBoxOnProjectile _hitProj = _projectile.GetComponent<HitBoxOnProjectile>();
                    if (_hitProj != null)
                    {
                        _hitProj.SetOwner(gameObject);
                        _projectile.tag = mainModule.player ? "Player" : "EnemyWeapon";
                        if(_datas.isSetHitBoxWhenSetProjectile)
                        {
                            _hitProj.SetEnable();
                        }
                    }

                    projectileObjects.Add(_projectile);
                }
            }
            else
            {
                Debug.Log($"None Animation Event : {_animationEvent} Current SO : {PositionSO}");
            }
        }

        public void MoveProjectile()
        {
            if (PositionSO is null)
                return;
        
            Vector3 _vec;
            
            if(mainModule.player)
            {
                Ray _ray = new Ray(cameraModule.CurrentCamera.transform.position, cameraModule.CurrentCamera.transform.forward);
                RaycastHit _raycastHit;

                if (Physics.Raycast(_ray, out _raycastHit, 80, targetLayerMask))
                {
                    _vec = _raycastHit.point;
                    Debug.Log("맞음!" + _raycastHit.transform.name);
                }
                else
                {
                    _vec = cameraModule.CurrentCamera.transform.position + cameraModule.CurrentCamera.transform.forward * 100f;
                }
            }
            else
            {
                _vec = PlayerObj.Player.transform.position + Vector3.up * height;
            }

            foreach (GameObject _projectile in projectileObjects)
            {
                _projectile.GetComponent<IProjectile>().MovingFunc(_vec);
            }

            projectileObjects.Clear();
        }

		public GameObject CreateProjectile(ProjectileObjectData _projectileObjectData)
		{
            string _name = "";
			if (isUseWeapon)
			{
			    _name = _projectileObjectData.projectileAddress == "Arrow" ? weaponModule.CurrentArrowInfo.arrowAddress : _projectileObjectData.projectileAddress;
				weaponModule = mainModule.GetModuleComponent<WeaponModule>(ModuleType.Weapon);
			}
            {
                _name = _projectileObjectData.projectileAddress;

			}
			GameObject _projectile = ObjectPoolManager.Instance.GetObject(_name);

			if (_projectileObjectData.animationEventName == "Arrow")
			{
				weaponModule.CurrentArrowInfo.action?.Invoke();
			}

			if (_projectileObjectData.isParentOn)
			{
				_projectile.transform.SetParent(isUseWeapon ? WhichHandToHold(_projectileObjectData.weaponHand) : noneUseWeaponTargetTrm);
				_projectile.transform.localRotation = _projectileObjectData.rotation;
				_projectile.transform.localPosition = _projectileObjectData.position;
				_projectile.SetActive(true);
			}
			else
			{
				Transform _parent = isUseWeapon ? WhichHandToHold(_projectileObjectData.weaponHand) : noneUseWeaponTargetTrm;
				_projectile.transform.localRotation = _projectileObjectData.rotation;
				_projectile.transform.localPosition = _parent.position + _projectileObjectData.position;
				_projectile.SetActive(true);
			}

			ProjectileObject _projectileObject = _projectile.GetComponent<ProjectileObject>();
			_projectileObject.objectData = _projectileObjectData;

			return _projectile;
		}

		private Transform WhichHandToHold(WeaponHand _weaponHand)
		{
			foreach (WeaponSpownObject _hand in weaponModule.WeaponRight)
			{
				if (_hand.weaponHand == _weaponHand)
					return _hand.transform;
			}
			return null;
		}

		IEnumerator Delay()
        {
            canSpwon = true;
            yield return new WaitForEndOfFrame();
            canSpwon = false;
        }
    }
}
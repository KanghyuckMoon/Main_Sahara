using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Cinemachine;

namespace CondinedModule
{
    public class Spider : AbMainModule, IEnemy
    {
        public string aiSOAddress = "TestEnemySO";
        public string AIAddress => aiSOAddress;
		public CinemachineSmoothPath SmoothPath
		{
            get
			{
                return smoothPath;
            }
            set
			{
                smoothPath = value;
            }
		}

        [SerializeField]
        private CinemachineSmoothPath smoothPath;

		private void Awake()
        {
            StopOrNot = 1;
            CanMove = true;

            moduleComponentsDic = new();
            CharacterController = GetComponent<CharacterController>();
            //footRotate = GetComponentInParent<csHomebrewIK>();
            AddModule(ModuleType.Input, new AIModule(this));
            AddModule(ModuleType.Move, new NoneDirMoveModule(this));
            AddModule(ModuleType.Stat, new StatModule(this));
            //AddModule(ModuleType.Camera, new CameraModule(this));
            AddModule(ModuleType.Jump, new JumpModule(this));
            AddModule(ModuleType.Hp, new HpModule(this));
            AddModule(ModuleType.Animation, new AnimationModule(this));
            AddModule(ModuleType.Physics, new PhysicsModule(this));
            AddModule(ModuleType.UI, new UIModule(this));
            AddModule(ModuleType.Attack, new AttackModule(this));
            AddModule(ModuleType.Weapon, new WeaponModule(this));
            AddModule(ModuleType.Hit, new HitModule(this));
            AddModule(ModuleType.Item, new ItemModule(this));

            RaycastTarget = transform.Find("RayCastPoint");

            Animator = GetComponent<Animator>();
            LockOnTarget = null;
        }

        private void OnDestroy()
        {
            CharacterController = null;
            moduleComponentsDic.Clear();
            RaycastTarget = null;
        }
    }
}

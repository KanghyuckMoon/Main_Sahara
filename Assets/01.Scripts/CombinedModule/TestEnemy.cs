using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Module;
using Pool;
using Utill.Pattern;
using Cinemachine;

namespace CondinedModule
{
    public class TestEnemy : AbMainModule, IEnemy
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

		protected void OnEnable()
        {
            moduleComponentsDic ??= new();
            CharacterController = GetComponent<CharacterController>();
            StopOrNot = 1;
            CanMove = true;

            moduleComponentsDic = new();
            CharacterController = GetComponent<CharacterController>();
            //footRotate = GetComponentInParent<csHomebrewIK>();
            AddModuleWithPool<AIModule>(ModuleType.Input, "AIModule");
            AddModuleWithPool<MoveModule>(ModuleType.Move, "MoveModule");
            AddModuleWithPool<StatModule>(ModuleType.Stat, "StatModule");
            //AddModuleWithPool<CameraModule>(ModuleType.Camera, "CameraModule");
            AddModuleWithPool<JumpModule>(ModuleType.Jump, "JumpModule");
            AddModuleWithPool<HpModule>(ModuleType.Hp, "HpModule");
            AddModuleWithPool<AnimationModule>(ModuleType.Animation, "AnimationModule");
            AddModuleWithPool<PhysicsModule>(ModuleType.Physics, "PhysicsModule");
            AddModuleWithPool<UIModule>(ModuleType.UI, "UIModule");
            AddModuleWithPool<AttackModule>(ModuleType.Attack, "AttackModule");
            AddModuleWithPool<WeaponModule>(ModuleType.Weapon, "WeaponModule");
            AddModuleWithPool<HitModule>(ModuleType.Hit, "HitModule");
            AddModuleWithPool<ItemModule>(ModuleType.Item, "ItemModule");
            AddModuleWithPool<EquipmentModule>(ModuleType.Equipment, "EquipmentModule");
            AddModuleWithPool<StateModule>(ModuleType.State, "StateModule");

            RaycastTarget ??= transform.Find("RayCastPoint");

            //visualObject ??= transform.Find("Visual")?.gameObject;
            Animator = GetComponent<Animator>();
            animatorOverrideController = new AnimatorOverrideController(Animator.runtimeAnimatorController);
            LockOnTarget = null;

            base.OnEnable();
        }
        private void OnDestroy()
        {
            CharacterController = null;
            RaycastTarget = null;
            Animator = null;
            moduleComponentsDic.Clear();
        }
    }
}

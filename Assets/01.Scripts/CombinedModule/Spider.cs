using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Cinemachine;
using Talk;

namespace CondinedModule
{
    public class Spider : AbMainModule, IEnemy
    {
        public string aiSOAddress = "TestEnemySO";
		public string AIAddress => aiSOAddress;
        
        public PathHarver PathHarver
        {
            get
            {
                return pathHarver;
            }
            set
            {
                pathHarver = value;
            }
        }

        [SerializeField]
        private PathHarver pathHarver;

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
            AddModuleWithPool<UIModule>(ModuleType.UI, "UIModule","HudUI");
            AddModuleWithPool<AttackModule>(ModuleType.Attack, "AttackModule");
            AddModuleWithPool<WeaponModule>(ModuleType.Weapon, "WeaponModule");
            AddModuleWithPool<HitModule>(ModuleType.Hit, "HitModule");
            AddModuleWithPool<ItemModule>(ModuleType.Item, "ItemModule");
            AddModuleWithPool<EquipmentModule>(ModuleType.Equipment, "EquipmentModule");
            AddModuleWithPool<StateModule>(ModuleType.State, "StateModule");
            AddModuleWithPool<SkillModule>(ModuleType.Skill, "SkillModule");
            AddModuleWithPool<BuffModule>(ModuleType.Buff, "BuffModule");

            RaycastTarget ??= transform.Find("RayCastPoint");

            //visualObject ??= transform.Find("Visual")?.gameObject;
            Animator = GetComponent<Animator>();
            animatorOverrideController = new AnimatorOverrideController(Animator.runtimeAnimatorController);
            LockOnTarget = null;

            base.OnEnable();
        }
        public void SetActiveAnimatorRoot(int _on)
        {
            bool _isOn = _on > 0;
            Animator.applyRootMotion = _isOn;
        }
        public void SetLayer(int layer)
        {
            gameObject.layer = layer;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace CondinedModule
{
    public class Player : AbMainModule
    {
        public void Awake()
        {
            StopOrNot = 1;
            CanMove = true;

            moduleComponentsDic = new();
            CharacterController = GetComponent<CharacterController>();
            //footRotate = GetComponentInParent<csHomebrewIK>();
            AddModuleWithPool<InputModule>(ModuleType.Input, "InputModule");
            AddModuleWithPool<MoveModule>(ModuleType.Move, "MoveModule");
            AddModuleWithPool<StatModule>(ModuleType.Stat, "StatModule");
            AddModuleWithPool<CameraModule>(ModuleType.Camera, "CameraModule");
            AddModuleWithPool<JumpModule>(ModuleType.Jump, "JumpModule");
            AddModule(ModuleType.Hp, new HpModule(this));
            AddModuleWithPool<AnimationModule>(ModuleType.Animation, "AnimationModule");
            AddModuleWithPool<PhysicsModule>(ModuleType.Physics, "PhysicsModule");
            AddModule(ModuleType.UI, new UIModule(this));
            AddModuleWithPool<AttackModule>(ModuleType.Attack, "AttackModule");
            AddModuleWithPool<WeaponModule>(ModuleType.Weapon, "WeaponModule");
            AddModule(ModuleType.Hit, new HitModule(this));
            AddModule(ModuleType.Item, new ItemModule(this));
            AddModule(ModuleType.Equipment, new EquipmentModule(this));
            AddModule(ModuleType.State, new StateModule(this));

            RaycastTarget ??= transform.Find("RayCastPoint");

            visualObject ??= transform.Find("Visual")?.gameObject;
            animator = GetComponent<Animator>();
            animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
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
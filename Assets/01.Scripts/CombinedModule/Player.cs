using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace CondinedModule
{
    public class Player : AbMainModule
    {
        private void Awake()
        {
            StopOrNot = 1;
            CanMove = true;

            moduleComponentsDic = new();
            CharacterController = GetComponent<CharacterController>();
            //footRotate = GetComponentInParent<csHomebrewIK>();
            AddModule(ModuleType.Input, new InputModule(this));
            AddModule(ModuleType.Move, new MoveModule(this));
            AddModule(ModuleType.Stat, new StatModule(this));
            AddModule(ModuleType.Camera, new CameraModule(this));
            AddModule(ModuleType.Jump, new JumpModule(this));
            AddModule(ModuleType.Hp, new HpModule(this));
            AddModule(ModuleType.Animation, new AnimationModule(this));
            AddModule(ModuleType.Physics, new PhysicsModule(this));
            AddModule(ModuleType.UI, new UIModule(this));
            AddModule(ModuleType.Attack, new AttackModule(this));
            AddModule(ModuleType.Weapon, new WeaponModule(this));
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
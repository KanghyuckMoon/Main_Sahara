using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Module;
using Pool;
using Utill.Pattern;

namespace CondinedModule
{
    public class TestEnemy : AbMainModule, IEnemy
    {
        public string aiSOAddress = "TestEnemySO";
		public string AIAddress => aiSOAddress;

		protected void OnEnable()
        {
            moduleComponentsDic ??= new();
            CharacterController = GetComponent<CharacterController>();
            StopOrNot = 1;
            CanMove = true;

            //footRotate = GetComponentInParent<csHomebrewIK>();

            AddModuleWithPool<AIModule>(ModuleType.Input, "AIModule");
            AddModuleWithPool<MoveModule>(ModuleType.Move, "MoveModule");
            AddModuleWithPool<StatModule>(ModuleType.Stat, "StatModule");
            AddModuleWithPool<HpModule>(ModuleType.Hp, "HpModule");
            AddModuleWithPool<AnimationModule>(ModuleType.Animation, "AnimationModule");
            AddModuleWithPool<UIModule>(ModuleType.UI, "UIModule", "HudUI");
            AddModuleWithPool<HitModule>(ModuleType.Hit, "HitModule");

            AddModule(ModuleType.Jump, new JumpModule(this));
            AddModule(ModuleType.Physics, new PhysicsModule(this));
            AddModule(ModuleType.Attack, new AttackModule(this));
            AddModule(ModuleType.Weapon, new WeaponModule(this));

            animator = GetComponent<Animator>();
            visualObject ??= transform.Find("Visual")?.gameObject;
            animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            RaycastTarget = transform.Find("RayCastPoint");

            base.OnEnable();
        }
    }
}

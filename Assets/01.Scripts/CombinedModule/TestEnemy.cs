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
            //AddModule(ModuleType.Input, new AIModule(this));

            AddModule(ModuleType.Move, new MoveModule(this));
            AddModule(ModuleType.Stat, new StatModule(this));
            //AddModule(ModuleType.Camera, new CameraModule(this));
            AddModule(ModuleType.Jump, new JumpModule(this));
            AddModule(ModuleType.Hp, new HpModule(this));
            AddModule(ModuleType.Animation, new AnimationModule(this));
            AddModule(ModuleType.Physics, new PhysicsModule(this));
            AddModule(ModuleType.UI, new UIModule(this, "HudUI"));
            AddModule(ModuleType.Attack, new AttackModule(this));
            AddModule(ModuleType.Weapon, new WeaponModule(this));
            AddModule(ModuleType.Hit, new HitModule(this));
            AddModule(ModuleType.State, new StateModule(this));

            animator = GetComponent<Animator>();
            visualObject ??= transform.Find("Visual")?.gameObject;
            animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            RaycastTarget = transform.Find("RayCastPoint");

            base.OnEnable();
        }
    }
}

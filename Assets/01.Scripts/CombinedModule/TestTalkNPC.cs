using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Module.Talk;
using Module.Shop;

namespace CondinedModule
{
	public class TestTalkNPC : AbMainModule, IEnemy
	{
		public string aiSOAddress = "TestEnemySO";
		public string AIAddress => aiSOAddress;

		public string textSOAddress;

        protected void OnEnable()
        {
            moduleComponentsDic ??= new();
            CharacterController = GetComponent<CharacterController>();
            StopOrNot = 1;
            CanMove = true;

            moduleComponentsDic = new();
            CharacterController = GetComponent<CharacterController>();
            AddModuleWithPool<AIModule>(ModuleType.Input, "AIModule");
            AddModule(ModuleType.Move, new MoveModule(this));
            AddModule(ModuleType.Stat, new StatModule(this));
            AddModule(ModuleType.Jump, new JumpModule(this));
            AddModule(ModuleType.Hp, new HpModule(this));
            AddModule(ModuleType.Animation, new AnimationModule(this));
            AddModule(ModuleType.Physics, new PhysicsModule(this));
            AddModule(ModuleType.UI, new UIModule(this, "HudUI"));
            AddModule(ModuleType.Attack, new AttackModule(this));
            AddModule(ModuleType.Weapon, new WeaponModule(this));
            AddModule(ModuleType.Hit, new HitModule(this));
            AddModule(ModuleType.State, new StateModule(this));
            AddModule(ModuleType.Talk, new TalkModule(this, textSOAddress));

            animator = GetComponent<Animator>();
            visualObject ??= transform.Find("Visual")?.gameObject;
            animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            RaycastTarget = transform.Find("RayCastPoint");

            base.OnEnable();
        }
    }

}
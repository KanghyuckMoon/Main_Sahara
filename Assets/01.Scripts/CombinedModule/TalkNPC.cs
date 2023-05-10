using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Module.Talk;
using Module.Shop;
using Cinemachine;
using Talk;

namespace CondinedModule
{
	public class TalkNPC : AbMainModule, IEnemy
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
        protected PathHarver pathHarver;

        [SerializeField] protected NPCRegisterManager.NPCTYPE npctype;
        
		public string textSOAddress;

        protected void OnEnable()
        {
            moduleComponentsDic ??= new();
            CharacterController = GetComponent<CharacterController>();
            StopOrNot = 1;
            CanMove = true;

            moduleComponentsDic = new();
            CharacterController = GetComponent<CharacterController>();
            LockOnTarget = null;
            ModuleAdd();

            Animator = GetComponent<Animator>();
            //visualObject ??= transform.Find("Visual")?.gameObject;
            animatorOverrideController = new AnimatorOverrideController(Animator.runtimeAnimatorController);
            RaycastTarget = transform.Find("RayCastPoint");

            NPCRegisterManager.Instance.Register(npctype, this);
            
            base.OnEnable();
        }

        protected virtual void ModuleAdd()
        {
	        AddModuleWithPool<AIModule>(ModuleType.Input, "AIModule");
	        AddModuleWithPool<MoveModule>(ModuleType.Move, "MoveModule");
	        AddModuleWithPool<StatModule>(ModuleType.Stat, "StatModule");
	        AddModuleWithPool<JumpModule>(ModuleType.Jump, "JumpModule");
	        AddModuleWithPool<HpModule>(ModuleType.Hp, "HpModule");
	        AddModuleWithPool<AnimationModule>(ModuleType.Animation, "AnimationModule");
	        AddModuleWithPool<PhysicsModule>(ModuleType.Physics, "PhysicsModule");
	        AddModuleWithPool<UIModule>(ModuleType.UI, "UIModule");
	        AddModuleWithPool<AttackModule>(ModuleType.Attack, "AttackModule");
	        AddModuleWithPool<WeaponModule>(ModuleType.Weapon, "WeaponModule");
	        AddModuleWithPool<HitModule>(ModuleType.Hit, "HitModule");
	        AddModuleWithPool<StateModule>(ModuleType.State, "StateModule");
	        AddModuleWithPool<TalkModule>(ModuleType.Talk, "TalkModule", textSOAddress);
        }
    }

}
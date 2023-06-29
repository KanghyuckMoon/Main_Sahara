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
	        AddModuleWithPool<AIModule>(ModuleType.Input);
	        AddModuleWithPool<MoveModule>(ModuleType.Move);
	        AddModuleWithPool<StatModule>(ModuleType.Stat);
	        AddModuleWithPool<JumpModule>(ModuleType.Jump);
	        AddModuleWithPool<HpModule>(ModuleType.Hp);
	        AddModuleWithPool<AnimationModule>(ModuleType.Animation);
	        AddModuleWithPool<PhysicsModule>(ModuleType.Physics);
	        //AddModuleWithPool<UIModule>(ModuleType.UI, null);
	        AddModuleWithPool<AttackModule>(ModuleType.Attack);
	        AddModuleWithPool<WeaponModule>(ModuleType.Weapon);
	        AddModuleWithPool<HitModule>(ModuleType.Hit);
	        AddModuleWithPool<StateModule>(ModuleType.State);
	        AddModuleWithPool<TalkModule>(ModuleType.Talk, textSOAddress);
        }
    }

}
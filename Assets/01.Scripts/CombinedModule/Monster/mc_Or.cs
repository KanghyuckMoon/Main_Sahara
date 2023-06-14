using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Module;
using Pool;
using Utill.Pattern;
using Cinemachine;
using Talk;

namespace CondinedModule
{
    public class mc_Or : AbMainModule, IEnemy
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

        [ContextMenu(("SS"))]
        public override void Start()
        {
            base.Start();
            
        }

        protected void OnEnable()
        {
            Debug.Log("@@@OnEnable");
            moduleComponentsDic ??= new();
            CharacterController = GetComponent<CharacterController>();
            StopOrNot = 1;
            CanMove = true;

            moduleComponentsDic = new();
            CharacterController = GetComponent<CharacterController>();
            //footRotate = GetComponentInParent<csHomebrewIK>();
            AddModuleWithPool<AIModule>(ModuleType.Input);
            AddModuleWithPool<RotationFreeMoveModule>(ModuleType.Move);
            AddModuleWithPool<StatModule>(ModuleType.Stat);
            //AddModuleWithPool<CameraModule>(ModuleType.Camera, "CameraModule");
            AddModuleWithPool<NoneAnimationJumpModule>(ModuleType.Jump);
            AddModuleWithPool<HpModule>(ModuleType.Hp);
            AddModuleWithPool<AnimationModule>(ModuleType.Animation);
            AddModuleWithPool<PhysicsModule>(ModuleType.Physics);
            AddModuleWithPool<UIModule>(ModuleType.UI,"HudUI");
            AddModuleWithPool<AttackModule>(ModuleType.Attack);
            AddModuleWithPool<WeaponModule>(ModuleType.Weapon);
            AddModuleWithPool<HitModule>(ModuleType.Hit);
            AddModuleWithPool<ItemModule>(ModuleType.Item);
            AddModuleWithPool<EquipmentModule>(ModuleType.Equipment);
            AddModuleWithPool<StateModule>(ModuleType.State);
            AddModuleWithPool<SkillModule>(ModuleType.Skill);
            AddModuleWithPool<BuffModule>(ModuleType.Buff);

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

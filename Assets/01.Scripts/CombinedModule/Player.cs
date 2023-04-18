using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using ForTheTest;

namespace CondinedModule
{
    public class Player : AbMainModule
    {
        [SerializeField] private ThirdPersonCameraController camera;
        
        private JumpModule jumpModule;
        private StateModule stateModule;
        
        
        public List<State> currentState;

        public void OnEnable()
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
            AddModuleWithPool<SkillModule>(ModuleType.Skill, "SkillModule");
            AddModuleWithPool<BuffModule>(ModuleType.Buff, "BuffModule");

            RaycastTarget ??= transform.Find("RayCastPoint");

            //visualObject ??= transform.Find("Visual")?.gameObject;
            Animator = GetComponent<Animator>();
            animatorOverrideController = new AnimatorOverrideController(Animator.runtimeAnimatorController);
            LockOnTarget = null;

            //SetInput(true);

            currentState = GetModuleComponent<StateModule>(ModuleType.State).CurrentState;

            base.OnEnable();
        }

        public override void SetConsecutiveAttack(int _on)
        {
            bool _isOn = _on > 0;
            CanConsecutiveAttack = _isOn;
        }
        
        public override void SetActiveAnimatorRoot(int _on)
        {
            bool _isOn = _on > 0;
            Animator.applyRootMotion = _isOn;
        }

        public void Jump()
        {
            jumpModule ??= GetModuleComponent<JumpModule>(ModuleType.Jump);
            jumpModule.Jump();
        }

        [ContextMenu("UIOn")]
        public void SetCam()
        {
            SetInput(true);
        }
        
        [ContextMenu("UIoff")]
        public void SetCama()
        {
            SetInput(false);
        }
        
        public void SetInput(bool _isOn)
        {
            stateModule ??= GetModuleComponent<StateModule>(ModuleType.State);
            if (_isOn)
                stateModule.AddState(State.UI);
            else stateModule.RemoveState(State.UI);

            camera.isUIOn = _isOn;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using ForTheTest;

using DG.Tweening;

namespace CondinedModule
{
    public class Player : AbMainModule
    {
        [SerializeField] private ThirdPersonCameraController camera;
        
        private JumpModule jumpModule;
        private StateModule stateModule;
        
        public List<State> currentState;

        public ThirdPersonCameraController Camera
        {
            get
            {
                if (camera is not null) return camera;
                camera = GameObject.Find("PlayerCam")?.GetComponent<ThirdPersonCameraController>();//FindObjectOfType<ThirdPersonCameraController>();
                return camera ?? null;
            }
        }
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

        public override void SetAnimationLayerOn(int _on, float _duration)
        {
            var animationIndex = Animator.GetLayerIndex(CurrentAnimationLayer);

            if (_duration is > 0f and < 1f)
            {
                var _a = Animator.GetLayerWeight(animationIndex);

                DOTween.To(
                    () => _a, (x) => Animator.SetLayerWeight(animationIndex, x), _on, _duration);
            }

            else
            {
                Animator.SetLayerWeight(animationIndex, _on);
            }
        }

        public override void Jump()
        {
            jumpModule ??= GetModuleComponent<JumpModule>(ModuleType.Jump);
            jumpModule.Jump(jumpstrenght);
        }
        public override void Jump(float power)
        {
            jumpModule ??= GetModuleComponent<JumpModule>(ModuleType.Jump);
            jumpModule.Jump(power);
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

            if (Camera is not null)
                Camera.isUIOn = _isOn;
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
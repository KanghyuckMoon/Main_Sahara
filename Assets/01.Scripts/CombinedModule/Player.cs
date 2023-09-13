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
            AddModuleWithPool<InputModule>(ModuleType.Input);
            AddModuleWithPool<MoveModule>(ModuleType.Move);
            AddModuleWithPool<StatModule>(ModuleType.Stat);
            AddModuleWithPool<CameraModule>(ModuleType.Camera);
            AddModuleWithPool<JumpModule>(ModuleType.Jump);
            AddModuleWithPool<HpModule>(ModuleType.Hp);
            AddModuleWithPool<AnimationModule>(ModuleType.Animation);
            AddModuleWithPool<PhysicsRotateHillModule>(ModuleType.Physics);
            //AddModuleWithPool<PhysicsModule>(ModuleType.Physics);
            AddModuleWithPool<UIModule>(ModuleType.UI);
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
            int animationIndex = Animator.GetLayerIndex(CurrentAnimationLayer);

            if (_duration is > 0 and < 1)
            {
                float _a = Animator.GetLayerWeight(animationIndex);

                DOTween.To(
                    () => _a, (x) => Animator.SetLayerWeight(animationIndex, x), _on, _duration);
            }

            else
            {
                //Debug.LogError("에러에러ㅔ러");
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

        public void SetAnimatorSpeed(float _speed)
        {
            Animator.speed = _speed;
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
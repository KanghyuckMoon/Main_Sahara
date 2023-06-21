using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace Module
{
    public class JumpModule : AbBaseModule
    {

        protected Animator Animator
        {
            get
            {
                animator ??= mainModule.GetModuleComponent<AnimationModule>(ModuleType.Animation).animator;
                return animator;
            }
        }
        protected float JumpHeight
        {
            get
            {
                if (mainModule is null)
                {
                    return 0f;
                }
                return mainModule.StatData.Jump;
            }
        }
        public float gravityWeight = 0;
        protected Animator animator;
        protected float jumpHeight;
        protected float _GravityScale => mainModule.GravityScale;

        protected float jumpDelay;
        protected float calculatedTime;
        protected float currentDelay;
        protected float jumpingDelay;

        protected bool isJumotrue;

        protected float antiFallTime;
        protected float calculatedFallTime;

        protected bool onJump;
        
        protected System.Action jumpAction;

        public JumpModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }
        public JumpModule() : base()
        {

        }


        public override void Start()
        {
            animator = mainModule.GetModuleComponent<AnimationModule>(ModuleType.Animation).animator;
            jumpHeight = mainModule.StatData.Jump;
            jumpDelay = 0.36f;
            antiFallTime = 0.16f;
        }

        public override void FixedUpdate()
        {
            JumpCheack();
            Weight();
        }

        public override void Update()
        {
            //Delay();
        }

        void Delay()
        {
            if (onJump)
            {
                if (currentDelay <= 0)
                    Jump();
                else
                    currentDelay -= mainModule.PersonalDeltaTime;
            }
        }

        void Weight()
        {
            if (gravityWeight >= mainModule.Gravity)
                gravityWeight = mainModule.Gravity;
        }

        protected virtual void JumpCheack()
        {
            if (mainModule.isGround)
            {
                calculatedFallTime = antiFallTime;

                Animator.SetBool("FreeFall", false);
                Animator.SetBool("Jump", false);

                if (mainModule.Gravity < 0) mainModule.Gravity = -2;
                
                if (mainModule.frontInput)
                {
                    Animator.SetBool("Jump", true);
                    return;
                }

                if (mainModule.IsJump && calculatedTime <= 0.0f)
                {
                    Animator.SetBool("Jump", true);
                    jumpAction?.Invoke();
                }
                    
                if (calculatedTime > 0.0f)
                    calculatedTime -= mainModule.PersonalDeltaTime;

            }
            else
            {
                calculatedTime = jumpDelay;

                if (calculatedFallTime >= 0.0f)
                {
                    calculatedFallTime -= mainModule.PersonalDeltaTime;
                }
                else
                {
                    Animator.SetBool("FreeFall", true);
                }

                mainModule.IsJump = false;
            }

            //if()
        }

        public void Jumping(float _delay)
        {
            currentDelay = _delay;
            onJump = true;
        }

        public void Jump(float _value = 0)
        {
            if (mainModule.IsChargeJumpOn) return;
            if (_value == 0) _value = JumpHeight;
            onJump = false;
            mainModule.Gravity = Mathf.Sqrt(_value * -2.2f * _GravityScale);
            Animator.SetBool("Jump", true);
        }

        public override void OnDisable()
        {
            animator = null;
            mainModule = null;
            jumpAction = null;
            base.OnDisable();
            Pool.ClassPoolManager.Instance.RegisterObject<JumpModule>(this);
        }

        public override void OnDestroy()
        {
            animator = null;
            mainModule = null;
            base.OnDestroy();
            animator = null;
            jumpAction = null;
        }
        
        public void AddJumpAction(System.Action action)
        {
            jumpAction += action;
        }
    }
}
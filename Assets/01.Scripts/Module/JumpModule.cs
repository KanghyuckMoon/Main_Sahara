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

        public bool onJump = true;
        
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
            antiFallTime = 0.25f;
        }

        public override void FixedUpdate()
        {
            //JumpCheack();
            //Weight();
        }

        public override void Update()
        {
            OnJump();
            //Delay();
        }

        void Weight()
        {
            if (gravityWeight >= mainModule.Gravity)
                gravityWeight = mainModule.Gravity;
        }

        private void OnJump()
        {
            if (mainModule.IsGround)
            {
                calculatedFallTime = antiFallTime;
                Animator.SetBool("FreeFall", false);

                if (mainModule.IsJump)
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
                    if (!Animator.GetBool("Jump") && !Animator.GetBool("DoubleJump"))
                        Animator.SetBool("FreeFall", true);
                    else
                    {
                        Animator.SetBool("FreeFall", false);
                    }
                }
            }
        }

        protected virtual void JumpCheack()
        {
            if (mainModule.IsGround)
            {
                calculatedFallTime = antiFallTime;

                Animator.SetBool("FreeFall", false);
                Animator.SetBool("Jump", false);

                if (mainModule.Gravity < 0 && onJump) mainModule.Gravity = 0f;
                
                if (mainModule.frontInput)
                {
                    if (mainModule.IsChargeJumpOn) return;
                    Animator.SetBool("Jump", true);
                }
                else if (mainModule.IsJump && calculatedTime <= 0.0f)
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
        }

        public void Jumping(float _delay)
        {
            currentDelay = _delay;
            onJump = true;
        }

        public void Jump(float _value = 0)
        {
            //if (mainModule.IsChargeJumpOn) return;
            if (_value == 0) _value = JumpHeight;
            onJump = false;
            mainModule.Gravity = Mathf.Sqrt(_value * -2f * _GravityScale);
            //Animator.SetBool("Jump", true);
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
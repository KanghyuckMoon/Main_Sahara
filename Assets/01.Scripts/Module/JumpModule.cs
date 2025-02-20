using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace Module
{
    public class JumpModule : AbBaseModule
    {

        private Animator Animator
        {
            get
            {
                animator ??= mainModule.GetModuleComponent<AnimationModule>(ModuleType.Animation).animator;
                return animator;
            }
        }
        private float JumpHeight
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
        private Animator animator;
        private float jumpHeight;
        private float _GravityScale => mainModule.GravityScale;

        private float jumpDelay;
        private float calculatedTime;
        private float currentDelay;
        private float jumpingDelay;

        private bool isJumotrue;

        private float antiFallTime;
        private float calculatedFallTime;

        private bool onJump;

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

        void JumpCheack()
        {
            if (mainModule.isGround)
            {
                calculatedFallTime = antiFallTime;

                Animator.SetBool("FreeFall", false);
                Animator.SetBool("Jump", false);

                if (mainModule.Gravity < 0) mainModule.Gravity = -2;

                if (mainModule.IsJump && calculatedTime <= 0.0f)
                {
                    Animator.SetBool("Jump", true);

                    //Jumping(0.07f);
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

        public void Jump()
        {
            onJump = false;
            mainModule.Gravity = Mathf.Sqrt(JumpHeight * -2.2f * _GravityScale);
        }

        public override void OnDisable()
        {
            animator = null;
            mainModule = null;
            base.OnDisable();
            Pool.ClassPoolManager.Instance.RegisterObject<JumpModule>("JumpModule", this);
        }

        public override void OnDestroy()
        {
            animator = null;
            mainModule = null;
            base.OnDestroy();
            animator = null;
        }
    }
}
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

        private float antiFallTime;
        private float calculatedFallTime;

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
            Jump();
            Weight();
        }

        void Weight()
        {
            if (gravityWeight >= mainModule.Gravity)
                gravityWeight = mainModule.Gravity;
        }

        void Jump()
        {
            if (mainModule.isGround)
            {
                calculatedFallTime = antiFallTime;

                Animator.SetBool("FreeFall", false);
                Animator.SetBool("Jump", false);

                if (mainModule.Gravity < 0) mainModule.Gravity = -2;

                if (mainModule.IsJump && calculatedTime <= 0.0f)
                {
                    Jumping();

                    Animator.SetBool("Jump", true);
                }

                if (calculatedTime > 0.0f)
                    calculatedTime -= Time.deltaTime;
            }
            else
            {
                calculatedTime = jumpDelay;

                if (calculatedFallTime >= 0.0f)
                {
                    calculatedFallTime -= Time.deltaTime;
                }
                else
                {
                    Animator.SetBool("FreeFall", true);
                }

                mainModule.IsJump = false;
            }

            //if()
        }

        public void Jumping()
        {
            //Debug.LogError("점ㅊ프프프프프");
            mainModule.Gravity = Mathf.Sqrt(JumpHeight * -2f * _GravityScale);
            //mainModule.characterController.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
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
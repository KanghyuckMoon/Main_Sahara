using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module
{
    public class JumpModule : AbBaseModule
    {
        private Animator animator;

        private float jumpHeight;
        private float gravityScale => mainModule.gravityScale;

        private float jumpDelay;
        private float calculatedTime;

        private float antiFallTime;
        private float calculatedFallTime;

        public JumpModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Start()
        {
            animator = mainModule.GetModuleComponent<AnimationModule>(ModuleType.Animation).animator;
            jumpHeight = mainModule.GetModuleComponent<StateModule>(ModuleType.State).JumpPower;
            jumpDelay = 0.4f;
            antiFallTime = 0.16f;
        }

        public override void FixedUpdate()
        {
            Jump();
        }

        void Jump()
        {
            if (mainModule.isGround)
            {
                calculatedFallTime = antiFallTime;

                animator.SetBool("FreeFall", false);
                animator.SetBool("Jump", false);

                if (mainModule.gravity < 0) mainModule.gravity = -2;

                if (mainModule.isJump && calculatedTime <= 0.0f)
                {
                    Jumping();

                    animator.SetBool("Jump", true);
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
                    animator.SetBool("FreeFall", true);
                }

                mainModule.isJump = false;
            }

            //if()
        }

        void Jumping()
        {
            mainModule.gravity = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
            //mainModule.characterController.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
}
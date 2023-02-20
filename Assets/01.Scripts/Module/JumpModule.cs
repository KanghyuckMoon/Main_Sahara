using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace Module
{
    public class JumpModule : AbBaseModule
    {
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
        }

        void Jump()
        {
            if (mainModule.isGround)
            {
                calculatedFallTime = antiFallTime;

                animator.SetBool("FreeFall", false);
                animator.SetBool("Jump", false);

                if (mainModule.Gravity < 0) mainModule.Gravity = -2;

                if (mainModule.IsJump && calculatedTime <= 0.0f)
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

                mainModule.IsJump = false;
            }

            //if()
        }

        void Jumping()
        {
            mainModule.Gravity = Mathf.Sqrt(jumpHeight * -2f * _GravityScale);
            //mainModule.characterController.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }

		public override void OnDestroy()
		{
			base.OnDestroy();
            animator = null;
        }
	}
}
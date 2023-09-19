using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module
{
    public class NoneAnimationJumpModule : JumpModule
    {
        protected override void JumpCheack()
        {
            if (mainModule.IsGround)
            {
                calculatedFallTime = antiFallTime;

                Animator.SetBool("FreeFall", false);
                Animator.SetBool("Jump", false);

                if (mainModule.Gravity < 0) mainModule.Gravity = -2;

                if (mainModule.IsJump && calculatedTime <= 0.0f)
                {
                    Animator.SetBool("Jump", true);
                    Jump();
                    jumpAction?.Invoke();
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
        }
    }   
}

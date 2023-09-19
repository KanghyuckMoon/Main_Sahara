using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using TimeManager;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

namespace PassiveItem
{
    public class SuperJumpAccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        private JumpModule jumpModule;
        private StateModule stateModule;
        
        float jumpStrength = 0;
        
        public SuperJumpAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
            jumpModule = mainModule.GetModuleComponent<JumpModule>(ModuleType.Jump);
            stateModule = mainModule.GetModuleComponent<StateModule>(ModuleType.State);
        }
        
        public void ApplyPassiveEffect()
        {
            mainModule.IsChargeJumpOn = true;
        }
        
        public void UpdateEffect()
        {
            if (!mainModule.IsGround) return;
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.LogError("??во??во??во");
                stateModule.AddState(State.JUMP);
                jumpStrength += Time.deltaTime;
                mainModule.Animator.SetBool("ChargeJump", true);
                mainModule.StopOrNot = 0;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                mainModule.StopOrNot = 1;
                Jump();
            }
        }

        public void ClearPassiveEffect()
        {
            mainModule.IsChargeJumpOn = false;
        }

        public void UpgradeEffect()
        {
            
        }

        private void Jump()
        {
            jumpStrength = Mathf.Clamp(1.7f, jumpStrength * 15, 19f);
            //jumpModule.Jump(jumpStrength);

            mainModule.jumpstrenght = jumpStrength;
            mainModule.Animator.SetBool("ChargeJump", false);
            //mainModule.Animator.SetBool("Jump", true);
            jumpStrength = 0;
        }
    }
}

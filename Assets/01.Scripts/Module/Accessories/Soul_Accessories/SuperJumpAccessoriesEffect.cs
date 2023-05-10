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
        
        float jumpStrength = 0;
        
        public SuperJumpAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
            jumpModule = mainModule.GetModuleComponent<JumpModule>(ModuleType.Jump);
        }
        
        public void ApplyPassiveEffect()
        {
            mainModule.IsChargeJumpOn = true;
        }
        
        public void UpdateEffect()
        {
            if (!mainModule.isGround) return;
            if (Input.GetKey(KeyCode.Space))
            {
                jumpStrength += Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
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
            jumpStrength = Mathf.Min(jumpStrength * 10, 15f);
            //jumpModule.Jump(jumpStrength);

            mainModule.jumpstrenght = jumpStrength;
            mainModule.Animator.SetBool("Jump", true);
            jumpStrength = 0;
        }
    }
}
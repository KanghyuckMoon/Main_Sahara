using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace PassiveItem
{
    public class DoubleJumpAccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        private JumpModule jumpModule;


        private bool isLand;
        
        public DoubleJumpAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
            jumpModule = mainModule.GetModuleComponent<JumpModule>(ModuleType.Jump);
        }

        public void ApplyPassiveEffect()
        {
            DoubleJump();
        }
        public void UpdateEffect()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (isLand && !mainModule.isGround)
                {
                    Jumping();
                    

                    isLand = false;
                }
            }
            //Debug.LogError("asdadffasdfag");

            if (mainModule.isGround)
                isLand = mainModule.isGround;// mainModule.Animator.SetBool("DoubleJump", false);
            
            //throw new System.NotImplementedException();
        }

        public void ClearPassiveEffect()
        {
            
        }

        public void DoubleJump()
        {

        }

        private void Jumping()
        {
            mainModule.Animator.SetBool("DoubleJump", true);
            //jumpModule.Jumping(0);
            jumpModule.Jump();
        }
    }
}
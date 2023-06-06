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
        private StateModule stateModule;

        private int maxCount = 1;
        private int count = 1;

        private bool air;

        private bool isPlayer = false;
        
        public DoubleJumpAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
            maxCount = 1;
            air = false;
            count = 1;
            jumpModule = mainModule.GetModuleComponent<JumpModule>(ModuleType.Jump);
            stateModule = mainModule.GetModuleComponent<StateModule>(ModuleType.State);

            if (mainModule.name == "Player")
            {
                isPlayer = true;
                return;
            }

            //Debug.LogError("적 임 적 임");
            isPlayer = false;
        }

        public void ApplyPassiveEffect()
        {
        }

        public void UpdateEffect()
        {
            if (!(mainModule.IsChargeJumpOn & mainModule.isGround) && isPlayer)
            {
                if (mainModule.IsJump)
                {
                    if (count > 0)
                    {
                        Jumping();
                        stateModule.AddState(State.JUMP);
                        count--;
                    }
                }
            }
            else
            {
                if (mainModule.IsJump)
                {
                    Jumping();
                    stateModule.AddState(State.JUMP);
                    count--;
                }
            }
            
            if (!mainModule.isGround)
            {
                air = true;
            }

            if (!air) return;
            if (mainModule.isGround)
                Land();
        }

        public void ClearPassiveEffect()
        {
            
        }

        public void UpgradeEffect()
        {
            maxCount++;
            count = maxCount;
        }

        private void Land()
        {
            count = maxCount;
            air = false;
        }

        private void Jumping()
        {
            mainModule.jumpstrenght = 0;
            
            if (!mainModule.isGround)
            {
                mainModule.Animator.SetBool("DoubleJump", true);
                jumpModule.Jump();
            }
            else
            {
                mainModule.Animator.SetBool("Jump", true);
                //jumpModule.Jump();
            }
        }
    }
}
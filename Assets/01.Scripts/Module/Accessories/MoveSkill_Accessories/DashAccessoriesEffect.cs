using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace PassiveItem
{
    public class DashAccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        private StateModule stateModule;
        private CharacterController characterController;

        private int dashAnimationIndex;

        public DashAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
            stateModule = mainModule.GetModuleComponent<StateModule>(ModuleType.State);
            characterController = mainModule.CharacterController;
            //dashAnimationIndex = mainModule.Animator.();
        }

        public void ApplyPassiveEffect()
        {
            //throw new System.NotImplementedException();
        }

        public void ClearPassiveEffect()
        {
            //throw new System.NotImplementedException();
        }

        public void UpdateEffect()
        {
            if (!mainModule.Animator.GetBool("Dash"))
            {
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    stateModule.AddState(State.SKILL);
                    mainModule.Animator.SetBool("Dash", true);
                    
                    
                    
                    Vector3 _inputDir = new Vector3(mainModule.ObjDir.x, 0, mainModule.ObjDir.y);
                    Vector3 _dir = mainModule.ObjRotation.eulerAngles * mainModule.ObjDir * (mainModule.PersonalDeltaTime * 28f);  
                    characterController.Move(_dir);
                }
            }
        }
    }
}
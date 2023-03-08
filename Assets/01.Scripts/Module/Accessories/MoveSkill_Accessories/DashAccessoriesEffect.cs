using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace PassiveItem
{
    public class DashAccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        private CharacterController characterController;

        private int dashAnimationIndex;

        public DashAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
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
                    Vector3 _dir = mainModule.transform.forward * Time.deltaTime * 28f;
                    characterController.Move(_dir);

                    mainModule.Animator.SetBool("Dash", true);
                }
            }
        }
    }
}
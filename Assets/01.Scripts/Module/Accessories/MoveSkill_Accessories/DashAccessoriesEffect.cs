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
                if (mainModule.IsDash && !mainModule.Animator.GetBool("ConsecutiveAttack"))
                {
                    stateModule.AddState(State.SKILL);
                    mainModule.Animator.SetBool("Dash", true);
                    
                    //Debug.LogError(TimeManager.StaticTime.PlayerDeltaTime);
                    
                    /*Vector3 _inputDir = new Vector3(mainModule.ObjDir.x, 0, mainModule.ObjDir.y);

                    float _direction = Mathf.Atan2(_inputDir.x, _inputDir.z) * Mathf.Rad2Deg +
                                       mainModule.ObjRotation.eulerAngles.y;
                    mainModule.transform.rotation = Quaternion.Euler(0, _direction, 0);*/
                    //Vector3 _realVec = Quaternion.Euler((_vec));
                    
                    //Debug.LogError((mainModule.transform.rotation.eulerAngles));
                    
                    /*Vector3 euler = mainModule.transform.eulerAngles;
                    Vector3 direction = new Vector3(Mathf.Sin(euler.y * Mathf.Deg2Rad), 0, Mathf.Cos(euler.y * Mathf.Deg2Rad)).normalized;*/

                    Vector3 _dir = (mainModule.ObjDirection.normalized * 36f * TimeManager.StaticTime.PlayerDeltaTime);
                    
                    characterController.Move(_dir);
                    mainModule.IsDash = false;
                }
            }
        }
    }
}
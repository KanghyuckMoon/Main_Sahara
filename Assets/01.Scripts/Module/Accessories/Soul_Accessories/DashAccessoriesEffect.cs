using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Pool;

namespace PassiveItem
{
    public class DashAccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        private StateModule stateModule;
        private CharacterController characterController;

        private int dashAnimationIndex;
            
        private bool dashing = false;

        private float delay;
        private float maxDelay = 0.9f;

        private GameObject dashEffect;

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

        public void UpgradeEffect()
        {
            
        }

        public void UpdateEffect()
        {
            if (!mainModule.Animator.GetBool("Dash"))
            {
                if (mainModule.IsDash && !mainModule.Animator.GetBool("ConsecutiveAttack"))
                {
                    dashEffect = ObjectPoolManager.Instance.GetObject("DashEffect");
                    dashEffect.transform.SetParent(mainModule.transform);
                    dashEffect.transform.localPosition = Vector3.up;
                    dashEffect.transform.localRotation = Quaternion.Euler(180, 0, 0);
                    dashEffect.SetActive(true);
                    
                    stateModule.AddState(State.SKILL);
                    mainModule.Animator.SetBool("Dash", true);
                    
                    delay = maxDelay;

                    Vector3 _dir = (mainModule.ObjDirection.normalized * 24f * mainModule.PersonalDeltaTime);
                    dashing = true;
                    
                    characterController.Move(_dir);
                    mainModule.IsDash = false;
                }
            }

            if (dashing)
            {
                delay -= Time.deltaTime;
                if (delay <= 0)
                {
                    Debug.LogError("chr;;;;;");
                    dashEffect.SetActive(false);
                    ObjectPoolManager.Instance.RegisterObject("DashEffect", dashEffect);
                    dashing = false;
                }
            }
        }
    }
}
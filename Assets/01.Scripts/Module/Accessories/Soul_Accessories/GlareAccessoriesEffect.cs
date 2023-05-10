using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using TimeManager;
using DG.Tweening;
using Pool;
using UnityEngine.Rendering;
using Weapon;

namespace PassiveItem
{
    public class GlareAccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        private StateModule stateModule;

        private GameObject light;

        private int lifeCount;
        
        public GlareAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
            stateModule = mainModule.GetModuleComponent<StateModule>(ModuleType.State);

            if (mainModule.name != "Player") return;
            light = mainModule.transform.Find("Point Light").gameObject;
            light.SetActive(false);
        }
        
        public void ApplyPassiveEffect()
        {
        }

        public void UpdateEffect()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && !stateModule.CheckState(State.SKILL))
            {
                mainModule.Animator.Play("GlareAnimation");
                light.SetActive(true);
            }
        }

        public void ClearPassiveEffect()
        {
        }

        public void UpgradeEffect()
        {
            
        }
    }
}
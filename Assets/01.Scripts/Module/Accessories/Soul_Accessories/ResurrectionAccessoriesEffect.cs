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
    public class ResurrectionAccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        private HitModule hitModule;

        private int lifeCount;
        
        public ResurrectionAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
            hitModule = mainModule.GetModuleComponent<HitModule>(ModuleType.Hit);
        }
        
        public void ApplyPassiveEffect()
        {
            hitModule.lifeCount++;
        }

        public void UpdateEffect()
        {
        }

        public void ClearPassiveEffect()
        {
            hitModule.lifeCount = 0;
        }

        public void UpgradeEffect()
        {
            
        }
    }
}
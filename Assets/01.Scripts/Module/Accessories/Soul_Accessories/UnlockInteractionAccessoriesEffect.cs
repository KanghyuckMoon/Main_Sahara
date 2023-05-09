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
    public class UnlockInteractionAccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;

        private int lifeCount;
        
        public UnlockInteractionAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
        }
        
        public void ApplyPassiveEffect()
        {
        }

        public void UpdateEffect()
        {
        }

        public void ClearPassiveEffect()
        {
        }

        public void UpgradeEffect()
        {
        }
    }
}
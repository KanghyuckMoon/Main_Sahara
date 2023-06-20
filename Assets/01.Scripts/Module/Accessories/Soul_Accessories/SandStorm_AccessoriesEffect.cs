using System.Collections;
using System.Collections.Generic;
using HitBox;
using UnityEngine;
using Module;

namespace PassiveItem
{
    public class SandStorm_AccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        
        public SandStorm_AccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
        }
        
        public void ApplyPassiveEffect()
        {
            IgnoreTornado();
            
        }
        public void UpdateEffect()
        {
            //throw new System.NotImplementedException();
        }

        public void ClearPassiveEffect()
        {
            UnIgnoreTornado();
        }

        public void UpgradeEffect()
        {
            
        }

        private void IgnoreTornado()
        {
            mainModule.IgnoreHitType |= HitType.SandStorm;
        }

        private void UnIgnoreTornado()
        {
            mainModule.IgnoreHitType &= ~HitType.SandStorm;
        }

    }
}
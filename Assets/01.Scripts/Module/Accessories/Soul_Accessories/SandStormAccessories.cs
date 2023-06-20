using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace PassiveItem
{
    public class SandStormAccessories : ItemPassive_Module
    {        
        private AbMainModule mainModule;
        public SandStormAccessories()
        {
            
        }

        public override void Init(AbMainModule _mainModule)
        {
            base.Init(_mainModule);
            mainModule = _mainModule;
            passiveEffects.Add(new SandStorm_AccessoriesEffect(_mainModule));
        }
        
        public override void Disable()
        {
            base.Disable();
            mainModule = null;
        }
    }
}
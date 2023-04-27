using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace PassiveItem
{

    public class Flame_Accessories : ItemPassive_Module
    {
        private AbMainModule mainModule;
        public Flame_Accessories()
        {
            
        }

        public override void Init(AbMainModule _mainModule)
        {
            base.Init(_mainModule);
            mainModule = _mainModule;
            passiveEffects.Add(new FlameAccessoriesEffect(_mainModule));
        }
        
        public override void Disable()
        {
            base.Disable();
            mainModule = null;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace PassiveItem
{
    public class FireAccessories : ItemPassive_Module
    {
        private AbMainModule mainModule;

        public FireAccessories()
        {
        }

        public override void Init(AbMainModule _mainModule)
        {
            base.Init(mainModule);
            mainModule = _mainModule;
            passiveEffects.Add(new Fire_AccessoriesEffect(mainModule));
        }
        
        public override void Disable()
        {
            base.Disable();
            mainModule = null;
        }
    }
}

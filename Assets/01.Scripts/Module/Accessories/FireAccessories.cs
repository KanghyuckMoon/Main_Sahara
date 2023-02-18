using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace PassiveItem
{
    public class FireAccessories : ItemPassive
    {
        private AbMainModule mainModule;

        public FireAccessories(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
            passiveEffects.Add(new Fire_AccessoriesEffect(mainModule));
        }
    }
}

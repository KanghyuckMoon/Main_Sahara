using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace PassiveItem
{
    public class Dash_Accessories : ItemPassive
    {
        private AbMainModule mainModule;
        public Dash_Accessories(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
            passiveEffects.Add(new DashAccessoriesEffect(_mainModule));
        }
    }
}
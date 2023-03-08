using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace PassiveItem {
    public class DoubleJump_Accessories : ItemPassive
    {
        private AbMainModule mainModule;

        public DoubleJump_Accessories(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
            passiveEffects.Add(new DoubleJumpAccessoriesEffect(_mainModule));
        }
    }
}
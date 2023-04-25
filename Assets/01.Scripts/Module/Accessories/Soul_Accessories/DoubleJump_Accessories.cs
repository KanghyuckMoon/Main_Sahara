using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace PassiveItem {
    public class DoubleJump_Accessories : ItemPassive_Module
    {
        private AbMainModule mainModule;

        public DoubleJump_Accessories()
        {
        }

        public override void Init(AbMainModule _mainModule)
        {
            base.Init(_mainModule);
            mainModule = _mainModule;
            passiveEffects.Add(new DoubleJumpAccessoriesEffect(_mainModule));
        }

        public override void Disable()
        {
            base.Disable();
            mainModule = null;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PassiveItem
{
    public class HpAccessories : ItemPassive
    {
        public HpAccessories()
        {
            passiveEffects.Add(new HpUp_AccessoriesEffect());
        }
    }
}
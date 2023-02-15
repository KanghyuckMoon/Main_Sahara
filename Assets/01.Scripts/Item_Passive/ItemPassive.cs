using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PassiveItem
{
    public class ItemPassive
    {
        protected List<IPassive> passiveEffects = new List<IPassive>();

        public ItemPassive()
        {
            
        }

        public void ApplyEffect()
        {
            foreach(IPassive _passive in passiveEffects)
            {
                _passive.ApplyPassiveEffect();
            }
        }

        public void UndoEffect()
        {
            foreach (IPassive _passive in passiveEffects)
            {
                _passive.ClearPassiveEffect();
            }
        }
    }
}
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

        // ReSharper disable Unity.PerformanceAnalysis
        public void UpdateEffect()
        {
            foreach (IPassive _passive in passiveEffects)
            {
                _passive.UpdateEffect();
            }
        }

        public void UndoEffect()
        {
            foreach (IPassive _passive in passiveEffects)
            {
                _passive.ClearPassiveEffect();
            }
        }

        public void UpgradeEffect()
        {
            foreach (IPassive _passive in passiveEffects)
            {
                _passive.UpgradeEffect();
            }
        }

        public virtual void Disable()
        {
            foreach (IPassive _passive in passiveEffects)
            {
                _passive.ClearPassiveEffect();
            }
        }

    }
}
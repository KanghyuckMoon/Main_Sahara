using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PassiveItem
{
    public interface IPassive
    {
        public void ApplyPassiveEffect();
        public void ClearPassiveEffect();
    }
}
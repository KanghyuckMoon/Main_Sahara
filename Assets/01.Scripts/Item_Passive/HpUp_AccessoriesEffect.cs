using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PassiveItem
{
    public class HpUp_AccessoriesEffect : IPassive
    {
        public void ApplyPassiveEffect()
        {
            HpUp();
        }

        public void ClearPassiveEffect()
        {
            HpDown();
        }

        private void HpUp()
        {
            Debug.Log("체력 30%업!");
        }

        private void HpDown()
        {
            Debug.Log("체력 30%다운!");
        }
    }
}
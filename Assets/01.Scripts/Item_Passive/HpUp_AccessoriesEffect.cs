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
            Debug.Log("ü�� 30%��!");
        }

        private void HpDown()
        {
            Debug.Log("ü�� 30%�ٿ�!");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Measurement;

namespace PassiveItem
{
    public class HpUp_AccessoriesEffect : IPassive
    {
        public void ApplyPassiveEffect()
        {
            HpUp();
        }
        public void UpdateEffect()
        {
            //throw new System.NotImplementedException();
        }

        public void ClearPassiveEffect()
        {
            HpDown();
        }

        public void UpgradeEffect()
        {
            
        }

        private void HpUp()
        {
            Logging.Log("ü�� 30%��!");
        }

        private void HpDown()
        {
            Logging.Log("ü�� 30%�ٿ�!");
        }

    }
}
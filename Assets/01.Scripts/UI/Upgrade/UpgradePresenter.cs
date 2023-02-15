using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;
using Inventory;
using Utill.Addressable;
using Utill.Measurement;
using Utill.Pattern;

namespace UI.Upgrade
{
    public class UpgradePresenter : MonoBehaviour, IScreen 
    {
        public void Test()
        {
            //InventoryManager.
        }
        public bool ActiveView()
        {
            return true; 
        }

        public void ActiveView(bool _isActive)
        {
        }
    }
}


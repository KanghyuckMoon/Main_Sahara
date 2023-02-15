using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Production;
using UI.ConstructorManager; 
using Inventory;
using Utill.Addressable;
using Utill.Pattern;

namespace UI.Upgrade
{
    public class UpgradeSlotPresenter
    {
        private UpgradeSlotView upgradeSlotView;
        private VisualElement parent; 
        private ItemData itemData;

        // 프로퍼티 
        public UpgradeSlotView UpgradeSlotView => upgradeSlotView;
        public VisualElement Parent => parent; 
        public UpgradeSlotPresenter()
        {
            var _production = UIConstructorManager.Instance.GetProductionUI(typeof(UpgradeSlotView));
            this.upgradeSlotView = _production.Item2 as UpgradeSlotView;
            this.parent = _production.Item1; 
        }

        public void SetItemData(ItemData _itemData)
        {
            if(this.upgradeSlotView == null)
            {
                Debug.LogWarning("UpgradeSlotView를 생성해주세요");
            }
            this.itemData = _itemData;
            upgradeSlotView.IsStackable = _itemData.IsStackble;
            upgradeSlotView.SetSpriteAndText(AddressablesManager.Instance.GetResource<Texture2D>(_itemData.spriteKey), _itemData.count);
        }
    }

}


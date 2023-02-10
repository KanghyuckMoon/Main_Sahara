using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Inventory;
using UI.Production;
using GoogleSpreadSheet;
using Utill.Addressable;
using UI.ConstructorManager; 

namespace UI.Inventory
{
    public class SlotItemPresenter
    {
        private SlotItemView slotItemView;
        private ItemData itemData; 

        public SlotItemPresenter()
        {
            //(VisualElement, AbUI_Base) _v = UIConstructorManager.Instance.GetProductionUI(typeof(SlotItemView));
            //ItemType _i = invenItemUISO.GetItemType(_itemType);

            //SlotItemView _slotView = _v.Item2 as SlotItemView;
            //_slotView.AddDragger(dragItemView.Item, () => ClickItem(_slotView));
            //itemSlotDic[_i].AddSlotView(_v.Item2 as SlotItemView);
            //SetParent(_itemType, _v.Item1);
        }
        public SlotItemPresenter(VisualElement _v)
        {
            //slotItemView = new SlotItemView()
        }

        public void SetItemData(ItemData _itemData)
        {
            this.itemData = _itemData;
            slotItemView.IsStackable = _itemData.IsStackble; 
            slotItemView.SetSprite(AddressablesManager.Instance.GetResource<Texture2D>(_itemData.spriteKey));
            slotItemView.SetText(_itemData.count); 
        }

    }

}

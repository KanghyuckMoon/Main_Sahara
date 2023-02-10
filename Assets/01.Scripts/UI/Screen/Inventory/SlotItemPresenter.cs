using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Inventory;
using UI.Production;
using GoogleSpreadSheet;
using Utill.Addressable;
using UI.ConstructorManager;
using System; 


namespace UI.Inventory
{
    public class SlotItemPresenter
    {
        private SlotItemView slotItemView;
        private VisualElement parent;
        private ItemData itemData;

        private int index;

        // 프로퍼티 
        public int Index => index;
        public SlotItemView SlotItemView => slotItemView;
        public VisualElement Parent => parent;
        public ItemData ItemData => itemData;

        /// <summary>
        /// 생성 
        /// </summary>
        public SlotItemPresenter()
        {
            (VisualElement, AbUI_Base) _v = UIConstructorManager.Instance.GetProductionUI(typeof(SlotItemView));
            this.slotItemView = _v.Item2 as SlotItemView;
            this.parent = _v.Item1;
        }

        /// <summary>
        /// 있는거 캐싱
        /// </summary>
        /// <param name="_v"></param>
        public SlotItemPresenter(VisualElement _v, int _idx)
        {
            //slotItemView = new SlotItemView(_v);
            slotItemView.InitUIParent(_v);
            slotItemView.Cashing();
            slotItemView.Init();
            this.index = _idx;
        }

        public void SetItemData(ItemData _itemData)
        {
            this.itemData = _itemData;
            slotItemView.IsStackable = _itemData.IsStackble;
            slotItemView.SetSpriteAndText(AddressablesManager.Instance.GetResource<Texture2D>(_itemData.spriteKey),
                                                              _itemData.count);
        }

        public void AddDragger(VisualElement _target, Action startCallback)
        {
            slotItemView.AddDragger(_target, startCallback);
        }
        public void AddDropper(Action _dropCallback)
        {
            slotItemView.AddDropper(_dropCallback);
        }

    }

}

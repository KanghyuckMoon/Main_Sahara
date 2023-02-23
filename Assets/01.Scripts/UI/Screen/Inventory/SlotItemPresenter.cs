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
using UI.Base; 

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
        public VisualElement Item => slotItemView.Item; 
        public ItemData ItemData => itemData;
        public Vector2 WorldPos => slotItemView.SlotWorldBound.position;
        public Vector2 ItemSize => new Vector2(slotItemView.SlotWorldBound.width, slotItemView.SlotWorldBound.height);

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
            slotItemView = new SlotItemView(_v);
            this.index = _idx;
        }

        /// <summary>
        /// 있는거 캐싱
        /// </summary>
        /// <param name="_v"></param>
        public SlotItemPresenter(VisualElement _v)
        {
            slotItemView = new SlotItemView(_v);
        }

        public void ClearData()
        {
            this.itemData = null;
            this.slotItemView.ClearUI(); 
        }
        public void SetItemData(ItemData _itemData)
        {
            this.itemData = _itemData;
            slotItemView.IsStackable = _itemData.IsStackble;
            if(_itemData.spriteKey != "")
            {
                slotItemView.SetSpriteAndText(AddressablesManager.Instance.GetResource<Texture2D>(_itemData.spriteKey),
                                                          _itemData.count);
            }
        }

        public void RemoveView()
        {
            slotItemView.RemoveView(); 
        }

        /// <summary>
        /// 클릭 이벤트 추가 
        /// </summary>
        public void AddClickEvent(Action _callback)
        {
            this.slotItemView.AddClickEvent(_callback);
        }

        public void AddDoubleClicker(Action _callback)
        {
            this.slotItemView.RemoveCurManipulator(); 
            this.slotItemView.AddManipulator(new DoubleClicker(_callback));
        }

        /// <summary>
        /// 드래거기능 추가
        /// </summary>
        /// <param name="_target"></param>
        /// <param name="startCallback"></param>
        public void AddDragger(VisualElement _target, Action startCallback)
        {
            slotItemView.AddManipulator(new Dragger(_target, startCallback));
        }
        
        /// <summary>
        /// 드롭퍼 기능추가
        /// </summary>
        /// <param name="_dropCallback"></param>
        public void AddDropper(Action _dropCallback)
        {
            slotItemView.AddManipulator(new Dropper((x) =>
            {
                _dropCallback?.Invoke();
                slotItemView.ActiveScreen(false);
            }));
        }

        /// <summary>
        /// 마우스 위에 둘시 이벤트 
        /// </summary>
        public void AddHoverEvent(Action _callback)
        {
            this.slotItemView.AddHoverEvent(_callback);
        }

        /// <summary>
        /// 마우스 위에서 떠날시 이벤트 
        /// </summary>
        /// <param name="_callback"></param>
        public void AddOutEvent(Action _callback)
        {
            this.slotItemView.AddOutEvent(_callback);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Production;
using UnityEngine.UIElements; 
using UI.Base;
using UI.ConstructorManager;
using System;
using UI.Map;
using Utill.Addressable;

namespace  UI
{
    public class MarkerSlotPr
    {
        private SlotItemView slotItemView;
        private VisualElement parent;
        private MarkerData markerData;

        private const string selectStr = "active_select"; 
        public MarkerData MarkerData => markerData;
        public Texture2D Image => slotItemView.ItemSprite;
        public VisualElement Parent
        {
            get
            {
                if (parent == null)
                {
                    parent = slotItemView.ParentElement;
                }

                return parent;
            }
        }
        public MarkerSlotPr()
        {
            (VisualElement, AbUI_Base) _v = UIConstructorManager.Instance.GetProductionUI(typeof(SlotItemView));
            this.slotItemView = _v.Item2 as SlotItemView;
            this.parent = _v.Item1;
        }

        public void SetData(MarkerData _markerData)
        {
            this.markerData = _markerData; 
            var _sprite = AddressablesManager.Instance.GetResource<Texture2D>(_markerData.spriteAddress);
            slotItemView.SetSpriteAndText(_sprite,_markerData.count);
        }
        
        public void SelectSlot(bool _isSelect)
        {
            if (_isSelect == true)
            {
                slotItemView.Select.AddToClassList(selectStr);
            }
            else
            {
                slotItemView.Select.RemoveFromClassList(selectStr);
            }
        }
        
        /// <summary>
        /// 클릭 이벤트 추가 
        /// </summary>
        public void AddClickEvent(Action _callback)
        {
            this.slotItemView.AddClickEvent(_callback);
        }
        
        /// <summary>
        /// 마우스 위에 둘시 이벤트 
        /// </summary>
        public void AddHoverEvent(Action _callback)
        {
            this.slotItemView.AddHoverEvent(_callback);
        }
        
    }    
}

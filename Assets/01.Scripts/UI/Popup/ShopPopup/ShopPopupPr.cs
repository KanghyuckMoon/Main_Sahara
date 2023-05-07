using System.Collections;
using System.Collections.Generic;
using Inventory;
using UI.ConstructorManager;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Production
{
    public class ShopPopupPr : IPopup
    {
        private ShopPopupView shopPopupView; 
        
        private VisualElement parent;
        public VisualElement Parent { get; }
        
        public ShopPopupPr()
        {
            var _prod = UIConstructorManager.Instance.GetProductionUI(typeof(ShopPopupView));
            parent = _prod.Item1; 
            shopPopupView = _prod.Item2 as ShopPopupView;
        }
        public void ActiveTween()
        {
            
        }

        public void InActiveTween()
        {
        }

        public void Undo()
        {
        }

        public void SetData(object _data)
        {
            ItemData _itemData = _data as ItemData; 
            
        }
    }    
}


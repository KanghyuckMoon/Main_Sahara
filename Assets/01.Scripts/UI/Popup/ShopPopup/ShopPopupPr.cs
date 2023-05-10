using System.Collections;
using System.Collections.Generic;
using GoogleSpreadSheet;
using Inventory;
using UI.ConstructorManager;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Addressable;
using UI.Production;
using Unity.Plastic.Antlr3.Runtime.Misc;

namespace UI.Popup
{
    public class ShopPopupPr : IPopup   
    {
        private ShopPopupView shopPopupView;

        private string itemName;
        private int price; 
        private VisualElement parent;

        private const string animateStr = "popup_inactive";
        public VisualElement Parent => parent; 
        
        public ShopPopupPr()
        {
            var _prod = UIConstructorManager.Instance.GetProductionUI(typeof(ShopPopupView));
            parent = _prod.Item1.ElementAt(0); 
            shopPopupView = _prod.Item2 as ShopPopupView;
            shopPopupView.InitUIParent(parent);
        }
        public void ActiveTween()
        {
            if (parent.ClassListContains(animateStr) == true)
            {
                parent.RemoveFromClassList(animateStr);
            }
        }

        public void InActiveTween()
        {
            parent.AddToClassList(animateStr);
        }

        public void Undo()
        {
            shopPopupView.ParentElement.RemoveFromHierarchy();
        }

        public void SetData(object _data)
        {
            ItemData _itemData = _data as ItemData;
            Texture2D _image = AddressablesManager.Instance.GetResource<Texture2D>(_itemData.spriteKey);
            itemName = TextManager.Instance.GetText(_itemData.nameKey);
            price = _itemData.price; 
        }

        public void AddDoubleClickEvent()
        {
            
        }

        public void AddClickEvent(Action _accentCallback,Action _cancelCallback)
        {
            shopPopupView.AddBtnEvent(() => _accentCallback?.Invoke(), () => _cancelCallback?.Invoke()); 
        }

        public void SetBuySell(bool _isBuy)
        {
            string _divisionStr = _isBuy ? "구매" : "판매"; 
            shopPopupView.SetTitleLabel($"{itemName}을(를) {_divisionStr}하시겠습니까?");
            shopPopupView.SetPriceLabel(price);
        }
    }    
}


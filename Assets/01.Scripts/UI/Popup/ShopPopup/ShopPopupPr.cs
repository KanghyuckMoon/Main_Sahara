using System.Collections;
using System.Collections.Generic;
using GoogleSpreadSheet;
using Inventory;
using UI.ConstructorManager;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Addressable;

namespace UI.Production
{
    public class ShopPopupPr : IPopup
    {
        private ShopPopupView shopPopupView;

        private string itemName; 
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
            Texture2D _image = AddressablesManager.Instance.GetResource<Texture2D>(_itemData.spriteKey);
            itemName = TextManager.Instance.GetText(_itemData.nameKey);
        }

        public void SetBuySell(bool _isBuy)
        {
            string _divisionStr = _isBuy ? "구매" : "판매"; 
            shopPopupView.SetTitleLabel($"{itemName}을(를) {_divisionStr}하시겠습니까?");

        }
    }    
}


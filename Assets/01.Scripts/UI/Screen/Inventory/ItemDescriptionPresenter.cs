using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Inventory;
using GoogleSpreadSheet;
using Utill.Addressable;
using UI.Production;
using Utill.Pattern;
using UI.ConstructorManager; 

namespace UI.Inventory
{
    public class ItemDescriptionPresenter 
    {
        private ItemData itemData;

        private ItemDescriptionView itemDescriptionView;

        public ItemDescriptionPresenter()
        {
            var _descView = UIConstructorManager.Instance.GetProductionUI(typeof(ItemDescriptionView));
            this.itemDescriptionView = _descView.Item2 as ItemDescriptionView;
        }


        public ItemDescriptionPresenter(VisualElement _parent)
        {
            this.itemDescriptionView = new ItemDescriptionView();
            this.itemDescriptionView.InitUIParent(_parent); 
            this.itemDescriptionView.Cashing();
            this.itemDescriptionView.Init(); 
        }

        public void ActiveView(bool _isActive)
        {
            this.itemDescriptionView.ActiveScreen(_isActive);
        }
        /// <summary>
        /// 설명창 설정 
        /// </summary>
        public void SetItemData(ItemData _itemData,Vector2 _slotPos, Vector2 _slotSize)
        {
            if (_itemData == null) return; // 빈 슬롯이면 리턴 

            this.itemData = _itemData; 
            itemDescriptionView.SetImage(AddressablesManager.Instance.GetResource<Texture2D>(_itemData.spriteKey));
            itemDescriptionView.SetNameAndDesciption(TextManager.Instance.GetText(_itemData.nameKey),
                                                                                    TextManager.Instance.GetText(_itemData.explanationKey));
            // 위치 설정 
            Vector2 _pos = new Vector2(_slotPos.x + _slotSize.x / 2, _slotPos.y + _slotSize.y);
            // 범위가 넘어가서 설명창이 안보이는 부분이 있다면 위로 올려서  
            if(_pos.y + 300/*itemDescriptionView.Height */> Screen.height)
            {
                _pos = new Vector2(_slotPos.x + _slotSize.x / 2, _slotPos.y - 300);
            }

            itemDescriptionView.SetPos(_pos); 
            ActiveView(true); 
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Inventory;
using GoogleSpreadSheet;
using Utill.Addressable; 

namespace UI.Inventory
{
    public class ItemDescriptionPresenter 
    {
        private ItemData itemData;

        private ItemDescriptionView itemDescriptionView;

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
        /// ����â ���� 
        /// </summary>
        public void SetItemData(ItemData _itemData,Vector2 _pos)
        {
            if (_itemData == null) return; // �� �����̸� ���� 

            this.itemData = _itemData; 
            itemDescriptionView.SetImage(AddressablesManager.Instance.GetResource<Texture2D>(_itemData.spriteKey));
            itemDescriptionView.SetNameAndDesciption(TextManager.Instance.GetText(_itemData.nameKey),
                                                                                    TextManager.Instance.GetText(_itemData.explanationKey));
            // ��ġ ���� 
            itemDescriptionView.SetPos(_pos); 
            ActiveView(true); 
        }
    }

}

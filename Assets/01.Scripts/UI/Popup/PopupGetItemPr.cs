using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;
using UI.Production;
using UI.ConstructorManager; 
using Inventory;
using GoogleSpreadSheet; 
using Utill.Addressable; 

namespace UI.Popup
{
    public class PopupGetItemPr : IPopup
    {
        private PopupGetItemView popupGetItemView;
        private VisualElement parent;
        
        public VisualElement Parent => parent; 
        public PopupGetItemPr()
        {
            var _prod = UIConstructorManager.Instance.GetProductionUI(typeof(PopupGetItemView));
            this.popupGetItemView = _prod.Item2 as PopupGetItemView;
            this.parent = _prod.Item1; 
            popupGetItemView.AddEventAfterImage(AnimateDetails);
            // 애니메이션 
            //  popupGetItemView.Parent.RemoveFromClassList("hide_getitem_popup");
     //       popupGetItemView.Parent.AddToClassList("hide_getitem_popup");
        }
        public void ActiveTween()
        {
            popupGetItemView.AnimateItem(true);
        }

        public void InActiveTween()
        {
            popupGetItemView.Parent.RemoveFromClassList("show_get_newitem_popup");
            popupGetItemView.Parent.AddToClassList("hide_get_newitem_popup");
            popupGetItemView.AnimateItem(false); 
        }

        public void Undo()
        {
            popupGetItemView.ParentElement.RemoveFromHierarchy();
        }

        public void SetData(object _data)
        {
            var _itemData = _data as ItemData;
            string _name = TextManager.Instance.GetText(_itemData.nameKey);
            /*if (_itemData.count > 1)
            {
                _name = _name + " X" + _itemData.count.ToString() + "개를 획득하셨습니다";
            }
            else
            {
                _name = _name + "(을)를 획득하셨습니다";
            }*/
            Texture2D _image = AddressablesManager.Instance.GetResource<Texture2D>(_itemData.spriteKey);
            PopupGetItemView.StringData _stringData = new PopupGetItemView.StringData{name = _name,sprite =_image}; 
            popupGetItemView.SetData(_stringData);
        }

        /// <summary>
        /// 이미지 하단 설명창 애니메이션 
        /// </summary>
        private void AnimateDetails()
        {
            popupGetItemView.Parent.RemoveFromClassList("show_get_newitem_popup");
            popupGetItemView.Parent.AddToClassList("hide_get_newitem_popup");
        }
        
    }

}

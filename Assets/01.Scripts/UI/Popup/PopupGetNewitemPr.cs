using System.Collections;
using System.Collections.Generic;
using GoogleSpreadSheet;
using Inventory;
using UI.ConstructorManager;
using UI.Production;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Addressable;

namespace UI.Popup
{
    public class PopupGetNewitemPr : IPopup
    {
        private PopupGetNewitemView popupGetNewitemView; 
        
        private VisualElement parent;
        
        public VisualElement Parent => parent; 
        
        public PopupGetNewitemPr()
        {
            var _prod = UIConstructorManager.Instance.GetProductionUI(typeof(PopupGetItemView));
            this.popupGetNewitemView = _prod.Item2 as PopupGetNewitemView;
            this.parent = _prod.Item1.ElementAt(0); 
            popupGetNewitemView.InitUIParent(parent);
          
            parent.RegisterCallback<TransitionEndEvent>((x) => popupGetNewitemView.ActiveTexts());
            // 애니메이션 
            //  popupGetItemView.Parent.RemoveFromClassList("hide_getitem_popup");
            //       popupGetItemView.Parent.AddToClassList("hide_getitem_popup");
        }
        public void ActiveTween()
        {
            popupGetNewitemView.Parent.RemoveFromClassList("hide_getitem_popup");
            popupGetNewitemView.Parent.AddToClassList("show_getitem_popup");
        }

        public void InActiveTween()
        {
            popupGetNewitemView.Parent.RemoveFromClassList("show_getitem_popup");
            popupGetNewitemView.Parent.AddToClassList("hide_getitem_popup");
        }

        public void Undo()
        {
            popupGetNewitemView.ParentElement.RemoveFromHierarchy();
        }

        public void SetData(object _data)
        {
            var _itemData = _data as ItemData;
            string _name = TextManager.Instance.GetText(_itemData.nameKey);
            string _datail = TextManager.Instance.GetText(_itemData.explanationKey);

            Texture2D _image = AddressablesManager.Instance.GetResource<Texture2D>(_itemData.spriteKey);
            PopupGetNewitemView.StringData _stringData = new PopupGetNewitemView.StringData{name = _name, detail = _datail,sprite =_image}; 
            popupGetNewitemView.SetData(_stringData);
        }
    }    
}


using System.Collections;
using System.Collections.Generic;
using GoogleSpreadSheet;
using Inventory;
using UI.ConstructorManager;
using UI.Production;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Addressable;
using Utill.Coroutine;

namespace UI.Popup
{
    public class PopupGetNewitemPr : IPopup
    {
        private PopupGetNewitemView popupGetNewitemView; 
        
        private VisualElement parent;

        private const string activeStr = "show_get_newitem_popup";
        private const string inactiveStr = "hide_get_newitem_popup";
        public VisualElement Parent => parent; 
        
        public PopupGetNewitemPr()
        {
            var _prod = UIConstructorManager.Instance.GetProductionUI(typeof(PopupGetNewitemView));
            this.popupGetNewitemView = _prod.Item2 as PopupGetNewitemView;
            this.parent = _prod.Item1.ElementAt(0);
            
            if (popupGetNewitemView != null)
            {
                popupGetNewitemView.InitUIParent(parent);

                popupGetNewitemView.AddEventAfterImage(AnimateDetails);
                parent.RegisterCallback<TransitionEndEvent>((x) =>
                {
                    if (parent.ClassListContains(activeStr))
                    {
                        popupGetNewitemView.ActiveTexts();
                    }
                });
            }
        }
        public void ActiveTween()
        {
            StaticCoroutineManager.Instance.InstanceDoCoroutine(popupGetNewitemView.AnimateItemCo(true));
            //popupGetNewitemView.AnimateItem(true);
        }

        public void InActiveTween()
        {
            popupGetNewitemView.Parent.RemoveFromClassList("show_get_newitem_popup");
            popupGetNewitemView.Parent.AddToClassList("hide_get_newitem_popup");
            popupGetNewitemView.ActiveTexts();
            StaticCoroutineManager.Instance.InstanceDoCoroutine(popupGetNewitemView.AnimateItemCo(false));
            //popupGetNewitemView.AnimateItem(false); 
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
        
        /// <summary>
        /// 이미지 하단 설명창 애니메이션 
        /// </summary>
        private void AnimateDetails()
        {
            popupGetNewitemView.Parent.RemoveFromClassList("hide_get_newitem_popup");
            popupGetNewitemView.Parent.AddToClassList("show_get_newitem_popup");
        }
    }    
}


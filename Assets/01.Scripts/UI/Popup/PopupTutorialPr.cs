using System;
using System.Collections;
using System.Collections.Generic;
using GoogleSpreadSheet;
using UI.ConstructorManager;
using UI.Production;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Addressable;

namespace UI.Popup
{
    /*
     * 어차피 하나만 나타날거긴 해
     * 그러면.. 팝업으로 안해도 되고
     * 하나만 스크린 형태로 만들고 껐다 켜도 해도 됨
     *
     * 스크린 형태 -> 그냥 싱글톤으로 만들고 조정하면 됨
     * 
     */
    public class PopupTutorialPr : IPopup
    {
        private PopupTutorialView popupTutorialView; 
        private Action onInactiveEvt = null;
        private VisualElement parent;

        private PopupTutorialData popupTutoData; 
        public Action OnInactiveEvt
        {
            get => onInactiveEvt;
            set => onInactiveEvt = value;
        }

        public VisualElement Parent => parent;
        public PopupTutorialData Data => popupTutoData; 
        
        public PopupTutorialPr()
        {
            var _prod = UIConstructorManager.Instance.GetProductionUI(typeof(PopupTutorialView));
            this.popupTutorialView = _prod.Item2 as PopupTutorialView;
            this.parent = _prod.Item1; //.ElementAt(0);

            /*if (popupTutorialView != null)
            {
                popupTutorialView.InitUIParent(parent);
            }*/
        }
        public void SetData(object _data)
        {
            PopupTutorialData _tData = _data as PopupTutorialData;
            popupTutoData = _tData; 
            popupTutorialView.SetTitle(_tData.titleAddress);
            popupTutorialView.SetDetail(TextManager.Instance.GetText(_tData.detailAddress));
            popupTutorialView.SetDetailImage(AddressablesManager.Instance.GetResource<Sprite>(_tData.detailImageAddress) );
        }

        public void SetDetail(string _detailStr)
        {
            popupTutorialView.SetDetail(_detailStr); 
        }
        public void SetDetailImage(string _detailStr)
        {
            popupTutorialView.SetDetailImage(AddressablesManager.Instance.GetResource<Sprite>(_detailStr)); 
        }

        public void ActiveTween()
        {
        }

        public void InActiveTween()
        {
        }

        public void Undo()
        {
            popupTutorialView.ParentElement.RemoveFromHierarchy();

        }


    }
}
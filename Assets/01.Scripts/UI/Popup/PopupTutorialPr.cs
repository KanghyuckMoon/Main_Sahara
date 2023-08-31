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
     * ������ �ϳ��� ��Ÿ���ű� ��
     * �׷���.. �˾����� ���ص� �ǰ�
     * �ϳ��� ��ũ�� ���·� ����� ���� �ѵ� �ص� ��
     *
     * ��ũ�� ���� -> �׳� �̱������� ����� �����ϸ� ��
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
            popupTutorialView.SetDetail(TextManager.Instance.GetText(_detailStr)); 
        }
        public void SetDetailImage(string _detailStr)
        {
            popupTutorialView.SetDetailImage(AddressablesManager.Instance.GetResource<Sprite>(_detailStr)); 
        }

        public void ActiveGuideLabel(bool _isActive)
        {
            popupTutorialView.ActiveGuideLabel(_isActive);
        }

        public void ActiveButton(bool _isLeft, bool _isActive)
        {
            popupTutorialView.ActiveButton(_isLeft, _isActive);
        }

        public void AddButtonEvt(PopupTutorialView.Buttons _btnType, Action _callback)
        {
            popupTutorialView.AddButtonEventToDic(_btnType, _callback);
        }

        public void SetButtonEvts()
        {
            popupTutorialView.AddButtonEvents(); 
        }

        private const string activeStr = "active_popupTuto";
        private const string inactiveStr = "inactive_popupTuto";
        public void ActiveTween()
        {
            // ������
            parent.RemoveFromClassList(inactiveStr);
            parent.AddToClassList(activeStr);
        }

        public void InActiveTween()
        {
            parent.RemoveFromClassList(activeStr);
            parent.AddToClassList(inactiveStr);
        }

        public void Undo()
        {
            parent.RemoveFromHierarchy();
            //popupTutorialView.ParentElement.style.display = DisplayStyle.None;
        }


    }
}
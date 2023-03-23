using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;
/*using Inventory;
using UI.Popup;
using UI.Production; */

namespace UI.Popup
{
    /// <summary>
    /// Hud와 동일 선상인 UI 
    /// </summary>
    [RequireComponent(typeof(UIDocument))]
    public class PopupHudPr : MonoBehaviour,IPopupPr
    {
        private  UIDocument uiDocument;

        [SerializeField]
        private PopupHudView popupHudView;

        public PopupType PopupType => PopupType.GetItem;

        private void Awake()
        {
            uiDocument = GetComponent<UIDocument>(); 
        }

        private void OnEnable()
        {
            popupHudView.InitUIDocument(uiDocument);
            popupHudView.Cashing();
            popupHudView.Init(); 
        }
        
        public void SetParent(VisualElement _v)
        {
            popupHudView.SetParent(_v);
        }
        
        public void CreatePopup()
        {
            //var _p = PopupUIManager.Instance.CreatePopup(PopupType.GetItem) as PopupGetItemView;
            
            // 팝업
        }

    }

}


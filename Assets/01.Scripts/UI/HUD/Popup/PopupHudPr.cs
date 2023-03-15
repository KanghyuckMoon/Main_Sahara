using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Inventory;
using UI.Popup;
using UI.Production; 

namespace UI.Hud
{
    [RequireComponent(typeof(UIDocument))]
    public class PopupHudPr : MonoBehaviour
    {
        private  UIDocument uiDocument;

        private PopupHudView popupHudView;

        private void Awake()
        {
            uiDocument = GetComponent<UIDocument>(); 
        }

        public void CreatePopup(ItemData _itemData)
        {
            var _p = PopupUIManager.Instance.CreatePopup(PopupType.GetItem) as PopupGetItemView;
            
            // 팝업
        }
    }

}


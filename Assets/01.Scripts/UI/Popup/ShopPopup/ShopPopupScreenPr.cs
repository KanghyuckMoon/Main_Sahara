using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace  UI.Popup
{
    public class ShopPopupScreenPr : MonoBehaviour, IPopupPr
    {
        private UIDocument uiDocument;
        [FormerlySerializedAs("shopPopupView")] [SerializeField]
        private ShopPopupScreenView shopPopupScreenView; 
        
        public PopupType PopupType => PopupType.Shop; 

        private void OnEnable()
        {
            shopPopupScreenView.InitUIDocument(uiDocument);
            shopPopupScreenView.Cashing();
        }

        private void Awake()
        {
            uiDocument = GetComponent<UIDocument>(); 
        }

        public void SetParent(VisualElement _v)
        {
            shopPopupScreenView.SetParent(_v);
        }

    }    
}


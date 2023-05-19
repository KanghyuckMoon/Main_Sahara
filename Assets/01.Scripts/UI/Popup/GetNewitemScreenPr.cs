using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Popup
{
    public class GetNewitemScreenPr : MonoBehaviour,IPopupPr
    {
        [SerializeField]
        private UIDocument uiDocument;
        [SerializeField]
        private GetNewitemScreenView getNewItemScreenView; 
        
        public PopupType PopupType => PopupType.GetNewItem; 

        private void OnEnable()
        {
            getNewItemScreenView.InitUIDocument(uiDocument);
            getNewItemScreenView.Cashing();
        }

        private void Awake()
        {
            uiDocument = GetComponent<UIDocument>(); 
        }

        public void SetParent(VisualElement _v)
        {
            getNewItemScreenView.SetParent(_v);
        }
    }
    
}

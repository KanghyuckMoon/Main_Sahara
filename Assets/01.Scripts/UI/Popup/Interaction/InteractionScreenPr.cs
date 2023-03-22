using System;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Popup
{
    public class InteractionScreenPr : MonoBehaviour,IPopupPr
    {
        private UIDocument uiDocument;
        [SerializeField]
        private InteractionScreenView interactionScreenView; 
        
        // 프로퍼티 
        public PopupType PopupType => PopupType.Interaction;

        private void OnEnable()
        {
            interactionScreenView.InitUIDocument(uiDocument);
            interactionScreenView.Cashing();
        }

        private void Awake()
        {
            uiDocument = GetComponent<UIDocument>(); 
        }

        public void SetParent(VisualElement _v)
        {
            interactionScreenView.SetParent(_v);
        }

    }
}

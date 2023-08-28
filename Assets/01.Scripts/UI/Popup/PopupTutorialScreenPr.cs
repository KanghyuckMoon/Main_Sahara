using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UI.Production;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace UI.Popup
{
    public class PopupTutorialScreenPr : MonoBehaviour,IPopupPr
    {
        [SerializeField]
        private UIDocument uiDocument;
        [FormerlySerializedAs("popupTutorialView")] [SerializeField]
        private PopupTutorialScreenView popupTutorialScreenView; 
        
       public PopupType PopupType => PopupType.Tutorial; 

        private void OnEnable()
        {
            popupTutorialScreenView.InitUIDocument(uiDocument);
            popupTutorialScreenView.Cashing();
        }

        private void Awake()
        {
            uiDocument = GetComponent<UIDocument>(); 
        }

        public void SetParent(VisualElement _v)
        {
            popupTutorialScreenView.SetParent(_v);
        }

        public void Active(bool _isActive)
        {
            popupTutorialScreenView.ActiveScreen(_isActive); 
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace UI.Option
{
    public class OptionPresenter : MonoBehaviour,IScreen
    {
        private UIDocument uiDocument;
        
        [SerializeField]
        private OptionView optionView; 
        
        private Action onActiveScreenEvt = null;

        // 프로퍼티 
        public Action OnActiveScreen
        {
            get => onActiveScreenEvt;
            set => onActiveScreenEvt = value;
        }
        public IUIController UIController { get; set; }

        private void Awake()
        {
            this.uiDocument = GetComponent<UIDocument>(); 
        }

        private void OnEnable()
        {
            optionView.InitUIDocument(uiDocument);
            optionView.Cashing();
            optionView.Init();
        }

        public bool ActiveView()
        {
            return optionView.ActiveScreen();
        }

        public void ActiveView(bool _isActive)
        {
            optionView.ActiveScreen(_isActive);
        }
    }
}



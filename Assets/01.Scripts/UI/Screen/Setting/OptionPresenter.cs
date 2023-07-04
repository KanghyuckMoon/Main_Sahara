using System;
using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using TimeManager;


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
            
            optionView.AddButtonEventToDic(OptionView.Buttons.continue_button, () =>
            {
                ActiveView(false); 
            });
            optionView.AddButtonEventToDic(OptionView.Buttons.exit_button, Application.Quit);
            optionView.Init();


        }

        private void OnDisable()
        {
            optionView.RemoveButtonEvents();
        }

        public bool ActiveView()
        {
            bool _isActive = optionView.ActiveScreen(); 
            StaticTime.UITime = _isActive ? 0f : 1f; 

            return _isActive;
        }

        public void ActiveView(bool _isActive)
        {
            StaticTime.UITime = _isActive ? 0f : 1f; 

            optionView.ActiveScreen(_isActive);
        }
    }
}



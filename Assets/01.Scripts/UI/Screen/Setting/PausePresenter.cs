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
    public class PausePresenter : MonoBehaviour,IScreen
    {
        private UIDocument uiDocument;
        
        [FormerlySerializedAs("optionView")] [SerializeField]
        private PauseView pauseView; 
        
        private Action onActiveScreenEvt = null;

        private IUIController owner; 
        // 프로퍼티 
        public Action OnActiveScreen
        {
            get => onActiveScreenEvt;
            set => onActiveScreenEvt = value;
        }
        public IUIController UIController
        {
            get => owner;
            set => owner = value;
        }

        private void Awake()
        {
            this.uiDocument = GetComponent<UIDocument>(); 
        }

        private void OnEnable()
        {
            pauseView.InitUIDocument(uiDocument);
            pauseView.Cashing();
            
            pauseView.AddButtonEventToDic(PauseView.Buttons.continue_button, () =>
            {
                OnActiveScreen?.Invoke();
            });
            pauseView.AddButtonEventToDic(PauseView.Buttons.option_button, () =>
            {
                var _optionPr = UIController.GetScreen<OptionPresenter>(ScreenType.Option);
                _optionPr.OnActiveScreen(); 
            });
            pauseView.AddButtonEventToDic(PauseView.Buttons.exit_button, Application.Quit);
            pauseView.Init();


        }

        private void OnDisable()
        {
            pauseView.RemoveButtonEvents();
        }

        public bool ActiveView()
        {
            bool _isActive = pauseView.ActiveScreen(); 
            
            StaticTime.UITime = _isActive ? 0f : 1f; 

            return _isActive;
        }

        public void ActiveView(bool _isActive)
        {
            StaticTime.UITime = _isActive ? 0f : 1f; 

            pauseView.ActiveScreen(_isActive);
        }
    }
}



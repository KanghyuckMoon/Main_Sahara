using System;
using System.Collections;
using System.Collections.Generic;
using UI.Base;
using Unity.VisualScripting.YamlDotNet.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Option
{
    public class OptionPresenter : MonoBehaviour, IScreen
    {
        private UIDocument uiDocument;

        [SerializeField]
        private OptionVIew optionView; 
        
        private Action onActiveScreen = null;
        public Action OnActiveScreen
        {
            get => onActiveScreen;
            set => onActiveScreen = value;
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

        private void OnDisable()
        {
        }

        public bool ActiveView()
        {
            //OnActiveScreen?.Invoke();

            bool _isActive = optionView.ActiveScreen(); 
            return _isActive; 
        }

        public void ActiveView(bool _isActive)
        {
            //OnActiveScreen?.Invoke();
            optionView.ActiveScreen(_isActive); 
        }
    }
   
}

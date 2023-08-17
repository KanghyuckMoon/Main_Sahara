using System;
using System.Collections;
using System.Collections.Generic;
using Option;
using UI.Base;
using Unity.VisualScripting.YamlDotNet.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Option
{
    public class OptionPresenter : MonoBehaviour, IScreen
    {
        // 데이터 관련 
        private GraphicsSetting grahpicSetting;
        private SoundSetting soundSetting; 
        
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
            this.grahpicSetting = GetComponent<GraphicsSetting>();
            this.soundSetting = GetComponent<SoundSetting>(); 
        }

        private void OnEnable()
        {
            optionView.InitUIDocument(uiDocument);
            optionView.Cashing();
            optionView.Init();
            
        }

        private void OnDisable()
        {
            optionView.RemoveButtonEvents();
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

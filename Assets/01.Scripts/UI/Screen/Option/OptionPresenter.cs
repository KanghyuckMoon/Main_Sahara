 using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Option;
using UI.Base;
using UI.Production;
using Unity.VisualScripting.YamlDotNet.Core;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Addressable;
 using Utill.Pattern;

 namespace UI.Option
{
    public interface IOptionEntry
    {
        public OptionData OptionData { get;  }
    }
    
    // Monosingleton<> 
    public class OptionPresenter : MonoSingleton<OptionPresenter>, IScreen
    {
        // ������ ���� 
        private GraphicsSetting grahpicSetting;
        private SoundSetting soundSetting; 
        
        private UIDocument uiDocument;

        [SerializeField]
        private OptionVIew optionView;
        
        
        private OptionDataSO optionDataSO; 
        private Dictionary<OptionType, Action<int>> optionEntryCallbackDic = new Dictionary<OptionType, Action<int>>();
        private Dictionary<OptionType, Action> optionEntryCallbackDic2 = new Dictionary<OptionType, Action>();

        private OptionBtnEntryPr a; 
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

            optionDataSO = AddressablesManager.Instance.GetResource<OptionDataSO>("OptionDataSO");

        }

        private void Start()
        {
//            soundSetting.InitSlider();
            optionEntryCallbackDic[OptionType.None] = (x) => { }; 
            optionEntryCallbackDic[OptionType.Resolution] = (x) => {grahpicSetting.SetResolution(x); }; 
            optionEntryCallbackDic[OptionType.AntiAliasing] = (x) => { grahpicSetting.SetAntiAliasing(x);}; 
            optionEntryCallbackDic[OptionType.RefreshRate] = (x) => { }; 
            optionEntryCallbackDic[OptionType.ShadowQuality] = (x) => {grahpicSetting.SetShadowQuality(x); }; 
            optionEntryCallbackDic[OptionType.IsFullScreen] = (x) => {grahpicSetting.SetFullscreen(x); }; 
            optionEntryCallbackDic[OptionType.TextureQuality] = (x) => {grahpicSetting.SetTextureQuality(x); }; 

            optionEntryCallbackDic2[OptionType.MasterVolume] = soundSetting .ApplySettingSound;
            optionEntryCallbackDic2[OptionType.BackgroundVolume] = soundSetting .ApplySettingSound;
            optionEntryCallbackDic2[OptionType.EffVolume] = soundSetting .ApplySettingSound;
            optionEntryCallbackDic2[OptionType.EnvironmentVolume] = soundSetting .ApplySettingSound;

            CreateEntry();
            ConnectUIAndData(); 
        }

        [ContextMenu("������� ����")]
        private void Apply()
        {
            soundSetting.ApplySettingSound();
        }
        [ContextMenu("UI, Data ����")]
        private void ConnectUIAndData()
        {
            var _masterVolume = GetOptionEntry<OptionBarEntryPr>(OptionType.MasterVolume);
            var _backgroundVolume = GetOptionEntry<OptionBarEntryPr>(OptionType.BackgroundVolume);
            var _effVolume = GetOptionEntry<OptionBarEntryPr>(OptionType.EffVolume);
            var _environmentVolume = GetOptionEntry<OptionBarEntryPr>(OptionType.EnvironmentVolume);
            soundSetting.InitSlider(_masterVolume.Slider, _backgroundVolume.Slider, _effVolume.Slider,_environmentVolume.Slider
            ,_masterVolume.Text, _backgroundVolume.Text, _effVolume.Text, _environmentVolume.Text);
        }

        [ContextMenu("����")]
        private void CreateEntry()
        {
            VisualElement _parent = null; 
            foreach (var optionData in optionDataSO.optionDataDic)
            {
                switch (optionData.Key)
                {
                    case OptionCategoryType.Graphics:
                        _parent = optionView.GraphicPanel;
                        break;
                    case OptionCategoryType.Sound:
                        _parent = optionView.SoundPanel;
                        break;
                    case OptionCategoryType.GameInfo:
                        _parent = optionView.GameInfoPanel;
                        break;
                    case OptionCategoryType.Help:
                        _parent = optionView.HelpPanel;
                        break;
                }
                CreateUI(optionData.Value.optionDataList,_parent);
            }


            // ī�װ�����  ���� 
            // ����Ǵ� �Լ��� dictinoary�� ���� �������� 
            // SO �� �����Ͱ� ��� �־�/ 
            // ��ư, ��, ��Ӵٿ� 
            // ��ư, ��Ӵٿ��� ���� 
            // �׷� ��Ӵٿ�, �� 
            // ��Ӵٿ��� �� ��Ҹ��� �Լ��� �ʿ��� 
            // �׶� �׋� � �Լ��� �����ų�� 
            // �ٵ� �̰� �� �����ϴ� �� ��ü���� index�� �޾ƾ��� ���ݾ� 
            // 
        }
        
        /*
         * OptionType �� ������
         * BtnEntry�� BarEntry�� �������� �;�

         * ������ �� as �� ����ȯ
         * GetPr(OptionType _optionType)
         * {
         *  return Interface ; 
         * }
         */

        public T GetOptionEntry<T>(OptionType _optionType) where T : class
        {
            IOptionEntry _entry = optionEntryList.Find((x) => x.OptionData.optionType == _optionType);
            return _entry as T; 
        }

  
        private List<IOptionEntry> optionEntryList =new List<IOptionEntry>(); 
 
        /// <summary>
        ///  �������� UI �����ϰ� UI ���� 
        /// </summary>
        /// <param name="_optionDataList"></param>
        private void CreateUI(List<OptionData> _optionDataList, VisualElement _parent)
        {
            VisualElement _newOptionEntry; 
            foreach (var _optionData in _optionDataList)
            {
                // ������ �� �����ְ� 
                switch (_optionData.optionModifyType)
                {
                    case OptionModifyType.Dropdown:
                        OptionBtnEntryPr _entryPr = new OptionBtnEntryPr();
                        _parent.Add(_entryPr.Parent);
                        SetOptionEntryData(_entryPr,_optionData); 
                        optionEntryList.Add(_entryPr);
                        break;
                    case OptionModifyType.Bar:
                        OptionBarEntryPr _barEntryPr = new OptionBarEntryPr(); 
                        _parent .Add(_barEntryPr.Parent);
                        SetOptionEntryData(_barEntryPr, _optionData);
                        optionEntryList.Add(_barEntryPr);
                        break;
                }
            }
        }

        /// <summary>
        /// �����Ѱ� ������ �־��ֱ� 
        /// </summary>
        private void SetOptionEntryData(OptionBarEntryPr _pr, OptionData _optionData)
        {
            KeyValuePair<OptionType, Action> _callback = new KeyValuePair<OptionType,Action>(); 
             _callback = optionEntryCallbackDic2.First(x => x.Key == _optionData.optionType);
             if (_callback.Equals(new KeyValuePair<OptionType,Action>()) == false)
             {
                 _pr.SetOptionData(_optionData);
                 _pr.AddEvent(_callback.Value);
             }
            // ���� �̸� ����
            //_optionData.name
            // OptionType���� �Լ� ���� 
            //_optionData.optionType
            // Dropdown ��� ���� 
            // _optionData.dropdownList
                    
        }
        /// <summary>
        /// �����Ѱ� ������ �־��ֱ� 
        /// </summary>
        private void SetOptionEntryData(OptionBtnEntryPr _pr, OptionData _optionData)
        {
            KeyValuePair<OptionType, Action<int>> _callback = new KeyValuePair<OptionType,Action<int>>(); 
            _callback = optionEntryCallbackDic.First(x => x.Key == _optionData.optionType);
            if(_callback.Equals(new KeyValuePair<OptionType,Action<int>>()) == false)
            {
                _pr.SetData(_callback.Value, _optionData.name, _optionData.dropdownList,_optionData);
            }
            
            // ���� �̸� ����
            //_optionData.name
            // OptionType���� �Լ� ���� 
            //_optionData.optionType
            // Dropdown ��� ���� 
            // _optionData.dropdownList
                    
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

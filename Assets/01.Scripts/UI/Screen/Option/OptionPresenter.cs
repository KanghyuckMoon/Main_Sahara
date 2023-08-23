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
        // 데이터 관련 
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

        [ContextMenu("변경사항 적용")]
        private void Apply()
        {
            soundSetting.ApplySettingSound();
        }
        [ContextMenu("UI, Data 연결")]
        private void ConnectUIAndData()
        {
            var _masterVolume = GetOptionEntry<OptionBarEntryPr>(OptionType.MasterVolume);
            var _backgroundVolume = GetOptionEntry<OptionBarEntryPr>(OptionType.BackgroundVolume);
            var _effVolume = GetOptionEntry<OptionBarEntryPr>(OptionType.EffVolume);
            var _environmentVolume = GetOptionEntry<OptionBarEntryPr>(OptionType.EnvironmentVolume);
            soundSetting.InitSlider(_masterVolume.Slider, _backgroundVolume.Slider, _effVolume.Slider,_environmentVolume.Slider
            ,_masterVolume.Text, _backgroundVolume.Text, _effVolume.Text, _environmentVolume.Text);
        }

        [ContextMenu("생성")]
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


            // 카테고리마다  정렬 
            // 적용되는 함수를 dictinoary를 톨해 가져오고 
            // SO 에 데이터가 담겨 있어/ 
            // 버튼, 바, 드롭다운 
            // 버튼, 드롭다운은 통합 
            // 그럼 드롭다운, 바 
            // 드롭다운은 각 요소마다 함숭가 필요해 
            // 그때 그떄 어떤 함술를 적용시킬지 
            // 근데 이건 그 조절하는 거 자체에서 index로 받아야지 맞잖아 
            // 
        }
        
        /*
         * OptionType 만 가지고
         * BtnEntry든 BarEntry든 가져오고 싶어

         * 가져올 때 as 로 형변환
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
        ///  실질적인 UI 생성하고 UI 세팅 
        /// </summary>
        /// <param name="_optionDataList"></param>
        private void CreateUI(List<OptionData> _optionDataList, VisualElement _parent)
        {
            VisualElement _newOptionEntry; 
            foreach (var _optionData in _optionDataList)
            {
                // 생성할 거 정해주고 
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
        /// 생성한거 데이터 넣어주기 
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
            // 설정 이름 설정
            //_optionData.name
            // OptionType으로 함수 적용 
            //_optionData.optionType
            // Dropdown 요소 적용 
            // _optionData.dropdownList
                    
        }
        /// <summary>
        /// 생성한거 데이터 넣어주기 
        /// </summary>
        private void SetOptionEntryData(OptionBtnEntryPr _pr, OptionData _optionData)
        {
            KeyValuePair<OptionType, Action<int>> _callback = new KeyValuePair<OptionType,Action<int>>(); 
            _callback = optionEntryCallbackDic.First(x => x.Key == _optionData.optionType);
            if(_callback.Equals(new KeyValuePair<OptionType,Action<int>>()) == false)
            {
                _pr.SetData(_callback.Value, _optionData.name, _optionData.dropdownList,_optionData);
            }
            
            // 설정 이름 설정
            //_optionData.name
            // OptionType으로 함수 적용 
            //_optionData.optionType
            // Dropdown 요소 적용 
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

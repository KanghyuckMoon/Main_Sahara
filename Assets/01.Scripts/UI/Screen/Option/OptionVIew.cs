using System;
using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UI.UtilManager;
using UnityEngine;
using UnityEngine.UIElements;

namespace  UI.Option
{

    public class SoundOptionView
    {
        enum Sliders
        {
            master_slider, 
            background_slider, 
            eff_slider, 
            envir_slider, 
        }

        enum Labels
        {
            master_label, 
            background_label, 
            eff_label, 
            envir_label, 
        }
        
    }
    
    [Serializable]
    public class OptionVIew : AbUI_Base 
    {
        /*
         * 옵션 타입 
         * 옵션 조정 타입 
         *  옵션 카테고리 타입
         * 
         * 이름   
         *
         * 드롭다운
         * ->
         * 이름 리스트 생성하는그거 choices랑 이벤트 설정  
         * 
         */
        enum PanelElements
        {
            
        }
        enum Elements
        {
            graphics_panel, 
            sound_panel,
            gameinfo_panel,
            help_panel
        }

        public enum Buttons
        {
            graphics_button, 
            sound_button, 
            gameinfo_button, 
            help_button, 
        }
        enum Labels
        {
            
        }

        enum ListViews
        {
            option_scroll_view,
        }

        enum Sliders
        {
            
        }
        private Dictionary<Buttons, Action> callbackDic = new Dictionary<Buttons, Action>();

        private List<(Buttons, Elements)> buttonPanelConnect = new List<(Buttons, Elements)>();

        private Buttons activeBtn;  
        private Elements activePanel; 
        
        // 프로퍼티 
        public VisualElement GraphicPanel => GetVisualElement((int)Elements.graphics_panel);
        public VisualElement SoundPanel => GetVisualElement((int)Elements.sound_panel);
        public VisualElement GameInfoPanel => GetVisualElement((int)Elements.gameinfo_panel);
        public VisualElement HelpPanel => GetVisualElement((int)Elements.help_panel);
        
        public override void Cashing()
        {
            base.Cashing();
            BindButtons(typeof(Buttons)); 
            BindListViews(typeof(ListViews));
            BindVisualElements(typeof(Elements));
        }

        private Dictionary<Elements,OptionBtnEntryPr> optionBtnEntryPrList = new Dictionary<Elements, OptionBtnEntryPr>(); 
        
        public override void Init()
        {
            base.Init();
            SetCategoryBtnEvent();
            AddButtonEvents();
            InActiveAllPanels(); 
            UIUtil.SendEvent(GetButton((int)Buttons.graphics_button));
        
            // 옵션 패널 안에서 바UI들을 가져온다 
            
        }

        private const string optionModifyStr = "option_modify";

        private void CashingOptionBars()
        {
            foreach (var _panel in Enum.GetValues(typeof(Elements)))
            {
                var a= GetVisualElement((int)_panel).Query<VisualElement>(className:optionModifyStr);
                foreach (var bar in a.ToList())
                {
                    optionBtnEntryPrList.Add((Elements)_panel, new OptionBtnEntryPr(bar));
                    //리스트에서 각각의 요소 가져오기 
                    

                }                                                                                                                                                                                                       
            }
        }
        
        /// <summary>
        /// 상단 카테고리 버튼 이벤트 설정
        /// </summary>
        private void SetCategoryBtnEvent()
        {
            AddButtonEventToDic(Buttons.graphics_button, () => ActivePanel(Buttons.graphics_button, Elements.graphics_panel));
            AddButtonEventToDic(Buttons.sound_button, () => ActivePanel(Buttons.sound_button, Elements.sound_panel));
            AddButtonEventToDic(Buttons.gameinfo_button, () => ActivePanel(Buttons.gameinfo_button, Elements.gameinfo_panel));
            AddButtonEventToDic(Buttons.help_button, () => ActivePanel(Buttons.help_button, Elements.help_panel));
        }

        private bool isFirstActivePn = true; 
        private const string activeStr = "active_label"; 
        private void ActivePanel(Buttons _btn, Elements _panel)
        {
            if (isFirstActivePn == false)
            {
                // 처음이 아니면 활성화 됐던거 지우기 (버튼색, 패널) 
                GetButton((int)activeBtn).RemoveFromClassList(activeStr);
                ShowVisualElement(GetVisualElement((int)activePanel), false);
            }
            else
            {
                isFirstActivePn = false; 
            }
            activeBtn = _btn;
            activePanel = _panel; 
            
            GetButton((int)activeBtn).AddToClassList(activeStr);
            ShowVisualElement(GetVisualElement((int)activePanel), true);
        }

        private void InActiveAllPanels()
        {
            foreach (var _panel in Enum.GetValues(typeof(Elements)))
            {
                ShowVisualElement(GetVisualElement((int)_panel), false);
            }
        }
        private void SetListviewEvents()    
        {
            
        }

        private void AddButtonEvents()
        {
            //AddButtonEvent<ClickEvent>((int)Buttons.graphics_button, callbackDic[Buttons.graphics_button]);
            AddButtonEvent<ClickEvent>((int)Buttons.sound_button, callbackDic[Buttons.sound_button]);
            AddButtonEvent<ClickEvent>((int)Buttons.gameinfo_button, callbackDic[Buttons.gameinfo_button]);
            AddButtonEvent<ClickEvent>((int)Buttons.help_button, callbackDic[Buttons.help_button]);

            GetButton((int)Buttons.graphics_button).clicked += callbackDic[Buttons.graphics_button];
        }

        public void RemoveButtonEvents()
        {
            //RemoveButtonEvent<ClickEvent>((int)Buttons.graphics_button, callbackDic[Buttons.graphics_button]);
            RemoveButtonEvent<ClickEvent>((int)Buttons.sound_button, callbackDic[Buttons.sound_button]);
            RemoveButtonEvent<ClickEvent>((int)Buttons.gameinfo_button, callbackDic[Buttons.gameinfo_button]);
            RemoveButtonEvent<ClickEvent>((int)Buttons.help_button, callbackDic[Buttons.help_button]);
       
            GetButton((int)Buttons.graphics_button).clicked -= callbackDic[Buttons.graphics_button];

        }
        
        public void AddButtonEventToDic(Buttons buttonType, Action callback)
        {
            callbackDic[buttonType] = callback;
        }
        //, 리스트뷰로 하면 퀘스트 코드 비슷하게 가져와서 하기 가능 
    }
    
}

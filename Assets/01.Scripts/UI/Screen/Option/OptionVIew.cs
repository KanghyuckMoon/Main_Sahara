using System;
using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UI.UtilManager;
using UnityEngine;
using UnityEngine.UIElements;

namespace  UI.Option
{
    [Serializable]
    public class OptionVIew : AbUI_Base 
    {
        public class OptionDataSO
        {
            public Dictionary<string, OptionData> optionDataDic = new Dictionary<string, OptionData>(); 
            
            // 키별로 묶어서 
            
        }
        public class OptionData
        {
            public string optionType;
            public string optionModifyType;
            public string optionCategoryType;
            public string name; 
        }
        
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
        enum Elements
        {
            graphic_panel, 
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

        private Dictionary<Buttons, Action> callbackDic = new Dictionary<Buttons, Action>();

        private List<(Buttons, Elements)> buttonPanelConnect = new List<(Buttons, Elements)>();

        private Buttons activeBtn;
        private Elements activePanel; 
        
        public override void Cashing()
        {
            base.Cashing();
            BindButtons(typeof(Buttons)); 
            BindListViews(typeof(ListViews));
            BindVisualElements(typeof(Elements));
        }

        public override void Init()
        {
            base.Init();
            SetCategoryBtnEvent(); 
            UIUtil.SendEvent(GetButton((int)Buttons.graphics_button));
        }

        /// <summary>
        /// 상단 카테고리 버튼 이벤트 설정
        /// </summary>
        private void SetCategoryBtnEvent()
        {
            // 패널 비활성화 활성화 
            // 버튼 활성화 비활성화 
            
            // 오직 하나만 활성화 
            // 버튼과 패널 연결 
            // 나 끄고 
            
            // 클릭한 버튼만 활성화 
            
            AddButtonEventToDic(Buttons.graphics_button, () => ActivePanel(Buttons.graphics_button, Elements.graphic_panel));
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
                // 처음이 아니면 
                GetButton((int)_btn).RemoveFromClassList(activeStr);
                ShowVisualElement(GetVisualElement((int)_panel), false);
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
        private void SetListviewEvents()
        {
            
        }

        private void AddButtonEvents()
        {
            AddButtonEvent<ClickEvent>((int)Buttons.graphics_button, callbackDic[Buttons.graphics_button]);
            AddButtonEvent<ClickEvent>((int)Buttons.sound_button, callbackDic[Buttons.sound_button]);
            AddButtonEvent<ClickEvent>((int)Buttons.gameinfo_button, callbackDic[Buttons.gameinfo_button]);
            AddButtonEvent<ClickEvent>((int)Buttons.help_button, callbackDic[Buttons.help_button]);

        }

        public void RemoveButtonEvents()
        {
            RemoveButtonEvent<ClickEvent>((int)Buttons.graphics_button, callbackDic[Buttons.graphics_button]);
            RemoveButtonEvent<ClickEvent>((int)Buttons.sound_button, callbackDic[Buttons.sound_button]);
            RemoveButtonEvent<ClickEvent>((int)Buttons.gameinfo_button, callbackDic[Buttons.gameinfo_button]);
            RemoveButtonEvent<ClickEvent>((int)Buttons.help_button, callbackDic[Buttons.help_button]);
        }
        
        public void AddButtonEventToDic(Buttons buttonType, Action callback)
        {
            callbackDic[buttonType] = callback;
        }
        //, 리스트뷰로 하면 퀘스트 코드 비슷하게 가져와서 하기 가능 
    }
    
}

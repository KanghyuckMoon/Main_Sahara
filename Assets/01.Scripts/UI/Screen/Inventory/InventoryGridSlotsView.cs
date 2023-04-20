using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using Inventory;
using UI.Production;
using UI.ConstructorManager;
using System;
using UI.Base;

namespace UI.Inventory
{
    /// <summary>
    /// 인벤토리의 슬롯들 보여준는 부분 
    /// </summary>
    public class InventoryGridSlotsView : AbUI_Base
    {
        public enum InvenPanelElements
        {
            // 패널들만 넣어야해 
            weapon_panel,
            armor_panel,
            consumation_panel,
            skill_panel,
            accessories_panel,
            material_panel,
            valuable_panel
        }

        public enum RadioButtons
        {
            weapon_button,
            armor_button,
            consumation_button,
            skill_button,
            accessories_button,
            material_button,
            valuable_button
            //장비
            //소비
            //기타
        }
        enum ScrollViews
        {
            inventory_scroll_panel
        }

        private InvenPanelElements curPanelType; // 현재 활성화중인 패널 

        // 프로퍼티 
        public InvenPanelElements CurPanelType => curPanelType;
        public override void Cashing()
        {
            //base.Cashing();
            BindVisualElements(typeof(InvenPanelElements));
            BindRadioButtons(typeof(RadioButtons));
            BindScrollViews(typeof(ScrollViews));
        }

        public override void Init()
        {
            base.Init();
            AddButtonEvents();
            SendEvent();
        }

        /// <summary>
        /// RadioButton  가져오기 
        /// </summary>
        public VisualElement GetRBtn(RadioButtons _type)
        {
            return GetRadioButton((int)_type);
        }
        public void SendEvent()
        {
            RadioButton _btn = GetRadioButton((int)RadioButtons.armor_button);
            using (var e = new NavigationSubmitEvent() { target = _btn })
                _btn.SendEvent(e);
            _btn = GetRadioButton((int)RadioButtons.weapon_button);
            using (var e = new NavigationSubmitEvent() { target = _btn })
                _btn.SendEvent(e);

        }
        public VisualElement GetPanel(InvenPanelElements _type)
        {
            return GetVisualElement((int)_type);
        }
        /// <summary>
        /// 특정 인벤토리 창에 슬롯 생성 
        /// </summary>
        /// <param name="_itemType"></param>
        /// <param name="_v"></param>
        public void SetParent(InvenPanelElements _itemType, VisualElement _v)
        {
            GetVisualElement((int)_itemType).Add(_v);
        }

        /// <summary>
        /// 버튼 이벤트 추가
        /// </summary>
        private void AddButtonEvents()
        {
            // 패널 활성화 
            foreach (var _p in Enum.GetValues(typeof(InvenPanelElements)))
            {
                AddRadioBtnChangedEvent((int)_p, (x) =>
                {
                    ActiveInventoryPanel((InvenPanelElements)_p, x);
                    ActiveRadioBtn((RadioButtons)_p, x);
                });

            }
        }

        /// <summary>
        /// 인벤토리 패널 활성화or비활성화 시키기 
        /// </summary>
        /// <param name="_elementType"></param>
        private void ActiveInventoryPanel(InvenPanelElements _elementType, bool _isActive)
        {
            // 바뀌었으면 
            if (curPanelType != _elementType && _isActive == true)
            {
                // 스크롤 초기화 
           
                curPanelType = _elementType;
                GetScrollView((int)ScrollViews.inventory_scroll_panel).scrollOffset = Vector2.zero;
            }

            VisualElement _v = GetVisualElement((int)_elementType);
            ShowVisualElement(_v, _isActive);
        }

        /// <summary>
        /// 버튼 이벤트 추가 
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_callback"></param>
        public void AddButtonEvent(RadioButtons _type, Action<bool> _callback)
        {
            AddRadioBtnChangedEvent((int)_type, (x) =>
            {
                _callback?.Invoke(x);
            });
        }

        private void ActiveRadioBtn(RadioButtons _type, bool _isActive)
        {
            float _v = _isActive ? 1.15f : 1f;
            Color _c = new Color(243f / 255f, 153f / 255f, 104f / 255f, 1f);

            Color _tC = _isActive ? _c : Color.white;
            GetRadioButton((int)_type).style.scale = new StyleScale(new Scale(new Vector2(_v, _v)));
            VisualElement _b = GetRadioButton((int)_type).Q(className: "unity-radio-button__checkmark-background");
            _b.style.unityBackgroundImageTintColor = new StyleColor(_tC);
        }
    }
}


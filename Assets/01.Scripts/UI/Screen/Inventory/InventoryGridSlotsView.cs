using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using Inventory;
using UI.Production;
using UI.ConstructorManager;
using System;

namespace UI.Inventory
{
    /// <summary>
    /// �κ��丮�� ���Ե� �����ش� �κ� 
    /// </summary>
    public class InventoryGridSlotsView : AbUI_Base
    {
        public enum InvenPanelElements
        {
            // �гε鸸 �־���� 
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
            //���
            //�Һ�
            //��Ÿ
        }
        enum ScrollViews
        {
            inventory_scroll_panel
        }

        private InvenPanelElements curPanelType; // ���� Ȱ��ȭ���� �г� 

        // ������Ƽ 
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
        /// Ư�� �κ��丮 â�� ���� ���� 
        /// </summary>
        /// <param name="_itemType"></param>
        /// <param name="_v"></param>
        public void SetParent(InvenPanelElements _itemType, VisualElement _v)
        {
            GetVisualElement((int)_itemType).Add(_v);
        }

        /// <summary>
        /// ��ư �̺�Ʈ �߰�
        /// </summary>
        private void AddButtonEvents()
        {
            // �г� Ȱ��ȭ 
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
        /// �κ��丮 �г� Ȱ��ȭor��Ȱ��ȭ ��Ű�� 
        /// </summary>
        /// <param name="_elementType"></param>
        private void ActiveInventoryPanel(InvenPanelElements _elementType, bool _isActive)
        {
            // �ٲ������ 
            if (curPanelType != _elementType)
            {
                // ��ũ�� �ʱ�ȭ 
                curPanelType = _elementType;
                GetScrollView((int)ScrollViews.inventory_scroll_panel).scrollOffset = Vector2.zero;
            }

            VisualElement _v = GetVisualElement((int)_elementType);
            ShowVisualElement(_v, _isActive);
        }

        /// <summary>
        /// ��ư �̺�Ʈ �߰� 
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


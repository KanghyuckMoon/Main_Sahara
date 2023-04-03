using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;
using UI.Production;
using System;

namespace UI.Upgrade
{
    public class UpgradePickView : AbUI_Base
    {
        enum Elements
        {
            slot_parent,
            absolute_parent
        }
        enum Buttons
        {
            compose_button, 
        }

        private const string inActiveStr = "upgrade_pick_inactive";
        public override void Cashing()
        {
            //base.Cashing();
            BindVisualElements(typeof(Elements)); 
            BindButtons(typeof(Buttons)); 
        }

        public override void Init()
        {
            base.Init();
        }

        public void ActiveUpgradeButton(bool _isActive)
        {
            // 버튼 클릭 할 수 있는지 설정  
            GetButton((int)Buttons.compose_button).pickingMode = _isActive ? PickingMode.Position : PickingMode.Ignore;
            GetButton((int)Buttons.compose_button).style.opacity = _isActive ? 1f : 0.7f; 
        }
        /// <summary>
        /// 합성하기 버튼에 이벤트 추가
        /// </summary>
        /// <param name="_callback"></param>
        public void AddComposeButtonEvent(Action _callback)
        {
            AddButtonEvent<ClickEvent>((int)Buttons.compose_button, _callback);
        }

        public void SetParent(VisualElement _v)
        {
            GetVisualElement((int)Elements.slot_parent).Add(_v); 
        }

        public override void ActiveScreen(bool _isActive)
        {
            base.ActiveScreen(_isActive);
            /*if (_isActive == true)
            {
                parentElement.AddToClassList(inActiveStr);
            }
            else
            {
                parentElement.RemoveFromClassList(inActiveStr);
            }*/
        }

        public void SetAbsoluteParent(VisualElement _v)
        {
            GetVisualElement((int)Elements.absolute_parent).Add(_v); 
        }


    }

}


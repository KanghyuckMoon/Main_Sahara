using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System; 

namespace UI.Upgrade
{
    public class UpgradeCtrlView : AbUI_Base
    {
        enum Buttons
        {
            left_button,
            right_button
        }

        enum Labels
        {
            left_label,
            mid_label,
            right_label
        }

        public Button LeftButton => GetButton((int)Buttons.left_button);
        public Button RightButton => GetButton((int)Buttons.right_button);

        public Label LeftLabel => GetLabel((int)Labels.left_label);
        public Label RightLabel => GetLabel((int)Labels.right_label);
        public Label MidLabel => GetLabel((int)Labels.mid_label);

        public override void Cashing()
        {
            //base.Cashing();
            BindButtons(typeof(Buttons));
            BindLabels(typeof(Labels));
        }

        public override void Init()
        {
            base.Init();
        }

        public void ActiveThing(VisualElement _v,bool _isActive)
        {
            ShowVisualElement(_v, _isActive);
        }

        /// <summary>
        /// 비활성화 라벨 
        /// </summary>
        /// <param name="_type"></param>
        public void InActiveLabel(PosType _type)
        {
            Label _selectLabel = new Label(); 
            switch (_type)
            {
                case PosType.left:
                    _selectLabel = GetLabel((int)Labels.left_label);
                    break;
                case PosType.mid:
                    _selectLabel = GetLabel((int)Labels.mid_label);
                    break;
                case PosType.right:
                    _selectLabel = GetLabel((int)Labels.right_label);
                    break;
                default:
                    Debug.LogWarning("PosType이 잘못되었습니다" + _type);
                    break;
            }
            ShowVisualElement(_selectLabel, false); 
        }

        /// <summary>
        /// 상단에 나타날 라벨 설정 
        /// </summary>
        /// <param name="_posType"></param>
        /// <param name="_text"></param>
        public void SetLabel(PosType _posType, string _text)
        {
            Label _selectLabel = new Label();
            switch (_posType)
            {
                case PosType.left:
                    _selectLabel = GetLabel((int)Labels.left_label);
                    break;
                case PosType.mid:
                    _selectLabel = GetLabel((int)Labels.mid_label);
                    break;
                case PosType.right:
                    _selectLabel = GetLabel((int)Labels.right_label);
                    break;
                default:
                    Debug.LogWarning("PosType이 잘못되었습니다" + _posType);
                    break;
            }
            ShowVisualElement(_selectLabel, true); 
            _selectLabel.text = _text;
        }

        /// <summary>
        /// 버튼 활성화 
        /// </summary>
        /// <param name="_isLeft"></param>
        public void ActiveButton(bool _isLeft,bool _isActive)
        {
            Buttons _type = _isLeft ? Buttons.left_button : Buttons.right_button;
            ShowVisualElement(GetButton((int)_type), _isActive); 
        }
        /// <summary>
        /// 좌우 이동 버튼 이벤트 등록  
        /// </summary>
        /// <param name="_isLeft"></param>
        /// <param name="_callback"></param>
        public void AddButtonEvent(bool _isLeft, Action _callback)
        {
            Buttons _type = _isLeft ? Buttons.left_button : Buttons.right_button;
            AddButtonEvent<ClickEvent>((int)_type, _callback);
        }


    }
}



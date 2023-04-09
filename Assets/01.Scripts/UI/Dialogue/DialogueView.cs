using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System; 
using UI.Base;

namespace UI.Dialogue
{
    [System.Serializable]
    public class DialogueView : AbUI_Base
    {
        enum Elements
        {
            select_panel
        }
        enum Labels
        {
            name_label,
            dialogue_label 
        }

        private static VisualElement parent; 
        private static Label name;
        private static Label dialogue;

        public static List<Button> selectButtonList = new List<Button>();
        private static List<(Button, Action)> activeButtonList = new List<(Button, Action)>();

        private readonly string inActiveStr = "inactive_select";
        public override void Cashing()
        {
            base.Cashing();
            parent = rootElement.Q<VisualElement>(parentElementName);
            BindLabels(typeof(Labels));
            BindVisualElements(typeof(Elements));

            selectButtonList = GetVisualElement((int)Elements.select_panel).Query<Button>(className:"select_button").ToList();
            foreach(var b in selectButtonList)
            {
                b.AddToClassList(inActiveStr);
                //b.style.display = DisplayStyle.None; 
            }
        }

        public override void Init()
        {
            base.Init();
            name = GetLabel((int)Labels.name_label);
            dialogue = GetLabel((int)Labels.dialogue_label);
            parent.style.display = DisplayStyle.None; 
        }

        public void SetNameText(string _str)
        {
            GetLabel((int)Labels.name_label).text = _str; 
        }
        public void SetDialogueText(string _str)
        {
            // 이전에 있던거 천천히 사라지고 
            // 텍스트 바뀌고 
            // 텍스트 쭈욱 나오게   
            GetLabel((int)Labels.dialogue_label).text = _str; 
        }

        public void SetNameTextA(string _str)
        {
            name.text = _str;
        }
        public  void SetDialogueTextA(string _str)
        {
            // 이전에 있던거 천천히 사라지고 
            // 텍스트 바뀌고 
            // 텍스트 쭈욱 나오게   
            dialogue.text = _str;
        }

        /// <summary>
        /// 선택 버튼 활성화(나타내기) 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_callback"></param>
        public void ActiveSelectButton(string _name,Action _callback)
        {
            for(int i=0;i < selectButtonList.Count;i++)
            {
                Button _b = selectButtonList[i]; 
                if (_b.ClassListContains(inActiveStr) == true)
                {
                    _b.text = _name;
                    _b.RegisterCallback<ClickEvent>((x) => _callback?.Invoke());
                    //_b.style.display = DisplayStyle.Flex;
                    _b.RemoveFromClassList(inActiveStr);

                    activeButtonList.Add((_b, _callback));
                    return; 
                }
            }
        }

        /// <summary>
        /// 모든 선택 버튼 초기화(비활성화, 콜백 등록 취소 ) 
        /// </summary>
        public void ResetSelectButtons()
        {
            foreach(var _b in activeButtonList)
                                            {
                                                //_b.Item1.style.display = DisplayStyle.None;
                                                _b.Item1.AddToClassList(inActiveStr);
                                                _b.Item1.UnregisterCallback<ClickEvent>((x) =>_b.Item2?.Invoke());
                                            }
                                        }
                                        public void ActiveView()
        {
            ShowVisualElement(parentElement, !IsVisible());
        }

        public void ActiveView(bool _isActive)
        {
            ShowVisualElement(parentElement, _isActive);
        }
        public void ActiveViewS(bool _isActive)
        {
            float targetV, nowV;
            targetV = _isActive ? 1 : 0;
            nowV = _isActive ? 0 : 1; 

            if(_isActive == true)
            {
                parent.style.opacity = new StyleFloat(0f);
                ShowVisualElement(parent, _isActive);
                DOTween.To(() => nowV, (x) => parent.style.opacity = new StyleFloat(x), targetV, 0.5f);
            
            }
            DOTween.To(() => nowV, (x) => parent.style.opacity = new StyleFloat(x), targetV, 0.5f)
                .OnComplete(() => ShowVisualElement(parent, _isActive));
        }
    }

}

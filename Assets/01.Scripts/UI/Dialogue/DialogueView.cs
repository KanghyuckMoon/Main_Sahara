using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System; 
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
        public override void Cashing()
        {
            base.Cashing();
            parent = rootElement.Q<VisualElement>(parentElementName);
            BindLabels(typeof(Labels));
            BindVisualElements(typeof(Elements));

            selectButtonList = GetVisualElement((int)Elements.select_panel).Query<Button>(className:"select_button").ToList();
            foreach(var b in selectButtonList)
            {
                b.style.display = DisplayStyle.None; 
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
            // ������ �ִ��� õõ�� ������� 
            // �ؽ�Ʈ �ٲ�� 
            // �ؽ�Ʈ �޿� ������   
            GetLabel((int)Labels.dialogue_label).text = _str; 
        }

        public static void SetNameTextA(string _str)
        {
            name.text = _str;
        }
        public static void SetDialogueTextA(string _str)
        {
            // ������ �ִ��� õõ�� ������� 
            // �ؽ�Ʈ �ٲ�� 
            // �ؽ�Ʈ �޿� ������   
            dialogue.text = _str;
        }

        /// <summary>
        /// ���� ��ư Ȱ��ȭ(��Ÿ����) 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_callback"></param>
        public static void ActiveSelectButton(string _name,Action _callback)
        {
            for(int i=0;i < selectButtonList.Count;i++)
            {
                Button _b = selectButtonList[i]; 
                if (_b.style.display == DisplayStyle.None)
                {
                    _b.text = _name;
                    _b.RegisterCallback<ClickEvent>((x) => _callback?.Invoke());
                    _b.style.display = DisplayStyle.Flex;

                    activeButtonList.Add((_b, _callback));
                    return; 
                }
            }
        }

        /// <summary>
        /// ��� ���� ��ư �ʱ�ȭ(��Ȱ��ȭ, �ݹ� ��� ��� ) 
        /// </summary>
        public static void ResetSelectButtons()
        {
            foreach(var _b in activeButtonList)
            {
                _b.Item1.style.display = DisplayStyle.None;
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
        public static void ActiveViewS(bool _isActive)
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

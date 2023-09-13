using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using UI.Base;
using UI.Dialogue;
using System;

namespace UI.PublicManager
{
    /// <summary>
    /// UI관련이 아닌 외부에서 UI에 접근하기 위해 사용하는 Manager 
    /// </summary>
    public class PublicUIManager : MonoSingleton<PublicUIManager>
    {
        private IUIController screenUIController = null;

        public IUIController ScreenUIController
        {
            get
            {
                if (screenUIController is null)
                {
                    GameObject _parent = GameObject.FindWithTag("UIParent");
                    if (_parent is not null)
                    {
                        screenUIController = _parent.GetComponentInChildren<IUIController>();
                        return screenUIController;
                    }
                }

                return screenUIController;
            }
        }

        public void Init()
        {
            screenUIController = null;
            StartCoroutine(InitScreenController());
        }

        private IEnumerator InitScreenController()
        {
            while (true)
            {
                if (screenUIController == null)
                {
                    GameObject _parent = GameObject.FindWithTag("UIParent");
                    if (_parent is not null)
                    {
                        screenUIController = _parent.GetComponentInChildren<IUIController>();
                    }
                    yield return null; 
                }
                else
                {
                    yield break;
                }
            }

        }
        public void ActiveOptionPr(bool _isActive)
        {
            ScreenUIController.ActiveScreen(Keys.OptionUI);
            //var screen = ScreenUIController.GetScreen<OptionPresenter>(ScreenType.Option);
            //screen.OnActiveScreen(); 
        }

        /// <summary>
        /// 처음 대화 텍스트 설정
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_dialogue"></param>
        /// <param name="_callback"></param>
        public void SetTexts(string _name, string _dialogue, Action _callback = null)
        {
            ScreenUIController.GetScreen<DialoguePresenter>(ScreenType.Dialogue)
                .StartDialogue(_name, _dialogue, _callback);
        }

        public bool IsDialogue()
        {
            return ScreenUIController.GetScreen<DialoguePresenter>(ScreenType.Dialogue).IsDialogue;
        }

        public void UpdateQuestUI()
        {
            //   ScreenUIController.GetScreen<QuestPresenter>(ScreenType.Quest).UpdateUI(); 
        }
    }
}
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
    /// UI������ �ƴ� �ܺο��� UI�� �����ϱ� ���� ����ϴ� Manager 
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

        /// <summary>
        /// ó�� ��ȭ �ؽ�Ʈ ����
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_dialogue"></param>
        /// <param name="_callback"></param>
        public void SetTexts(string _name, string _dialogue, Action _callback = null)
        {
            ScreenUIController.GetScreen<DialoguePresenter>(ScreenType.Dialogue).SetTexts(_name, _dialogue, _callback);
        }

    }

}


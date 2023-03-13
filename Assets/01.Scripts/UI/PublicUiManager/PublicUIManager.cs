using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using UI.Base; 

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

        public void SetTexts()
        {
           
        }

    }

}


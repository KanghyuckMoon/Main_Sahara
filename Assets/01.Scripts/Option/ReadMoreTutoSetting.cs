 using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Option;
using UI.Base;
 using UI.Popup;
 using UnityEngine;
 using UnityEngine.UI;
 using UnityEngine.UIElements;
using Utill.Addressable;
 using Utill.Pattern;

namespace UI.Option
{

    public class ReadMoreTutorialSetting : MonoBehaviour
    {
        private AllPopupTutorialDataSO allPopupTutorialDataSO;

        public AllPopupTutorialDataSO AllPopupTutorialDataSo => allPopupTutorialDataSO; 
        private void Awake()
        {
            allPopupTutorialDataSO =
                AddressablesManager.Instance.GetResource<AllPopupTutorialDataSO>("AllPopupTutorialDataSO");
        }
        public void ActiveTutorialPopup(string _key)
        {
            //PopupTutorialDataSO popupTutorialDataSo; 
            //popupTutorialDataSo.Send();
            GameEventManager.Instance.GetGameEvent(_key).Raise();
        }
    }
}
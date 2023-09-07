using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;

namespace UI.Popup
{
    public class TutorialPopupEventManager : MonoSingleton<TutorialPopupEventManager>
    {
        public AllPopupTutorialDataSO allTutorialPopupDataSO; 
        public Dictionary<string, bool> popupTutorialDic = new Dictionary<string, bool>();

        private void Start()
        {
            Init(); 
        }

        public void Init()
        {
            allTutorialPopupDataSO ??= AddressablesManager.Instance.GetResource<AllPopupTutorialDataSO>("AllPopupTutorialDataSO");
            foreach (var _popupTuto in allTutorialPopupDataSO.popupTutorialDataSoList)
            {
                if(!popupTutorialDic.ContainsKey(_popupTuto.key))
                {
                    popupTutorialDic.Add(_popupTuto.key,false);
                }
            }
            //
        }
        
        /// <summary>
        /// 팝업 활성화 
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_callback"></param>
        /// <param name="_isMustExe">True하면 한 번 실행했어도 실행</param>
        public void ActiveTutoPopup(string _key, Action _callback, bool _isMustExe = false)
        {
            // 이미 한 번 실행 했으면 리턴 
            if (popupTutorialDic.TryGetValue(_key, out bool _value) == true)
            {
                if (_value == true && _isMustExe == false)
                {
                    return; 
                }
            }
            _callback?.Invoke();
            popupTutorialDic[_key] = true; 
            // 여기서 실행하고 체크 
        }
    }
}
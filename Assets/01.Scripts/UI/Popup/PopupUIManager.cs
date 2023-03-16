using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using UI.ConstructorManager;
using System;

namespace UI.Popup
{
    public enum PopupType
    {
        GetItem, //  아이템 획득시 
        FindItem, // 아이템 주변 다가갔을 때
    }

    public class PopupUIManager : MonoSingleton<PopupUIManager>
    {
        private Dictionary<PopupType, Type> popupChangeDic = new Dictionary<PopupType, Type>();
        private Dictionary<PopupType, IPopup> popupDic = new Dictionary<PopupType, IPopup>();


        public void Awake()
        {
            base.Awake();
            InitPopupDic(); 
        }
        public AbUI_Base CreatePopup(PopupType _popupType)
        {
            var _prod = UIConstructorManager.Instance.GetProductionUI(GetPopupType(_popupType));
            return _prod.Item2; 
        }

        public void AddToDic<T>(PopupType _popupType,IPopup _iPopup)
        {
            this.popupDic.Add(_popupType, _iPopup);
        }

        private void InitPopupDic()
        {
            popupChangeDic.Clear();

            popupChangeDic.Add(PopupType.GetItem, typeof(PopupGetItemPr));
        }


        private Type GetPopupType(PopupType _popupType)
        {
            if(popupChangeDic.TryGetValue(_popupType, out Type _type) == true)
            {
                return _type; 
            }
            return null; 
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Pattern;
using UI.ConstructorManager;
using System;
using UI.Base;
using UI.Popup;
using UI.Production; 
using System.Linq; 

namespace UI.Popup
{


    public class PopupUIManager : MonoSingleton<PopupUIManager>
    {
        private Transform popupParent;

        private PopupHudPr popupHudPr;
        private List<IPopupPr> popupPrList = new List<IPopupPr>(); 

        public Transform PopupParent
        {
            get
            {
                if (popupParent == null)
                {
                    popupParent = GameObject.FindWithTag("UIParent").transform.Find("PopupScreens");
                    SetPresenters(); 
                }

                return popupParent;
            }
        }

        private bool isInit = false; 
        public override void Awake()
        {
            base.Awake();
            Init(); 
        }

        public void CreatePopup<T>(PopupType _popupType, object _data = null,float _time = 2f) where  T : IPopup,new()
        {
            if (isInit == false)    
            {
                Init();
                isInit = true; 
            }
            T _popupGetItemPr = new T();
            
            popupPrList.First(x => x.PopupType == _popupType).SetParent(_popupGetItemPr.Parent);
            StartCoroutine(_popupGetItemPr.TimerCo(2f));
            _popupGetItemPr.SetData(_data);
        }
        private void Init()
        {
            GameObject _parent = GameObject.FindWithTag("UIParent");
            if (_parent == null) return; 
            popupParent = _parent.transform.Find("PopupScreens");
            SetPresenters(); 

        }

        private void SetPresenters()
        {
            popupHudPr = popupParent.GetComponentInChildren<PopupHudPr>(); 
            popupPrList.Add(popupHudPr);
        }
        /*private Dictionary<PopupType, Type> popupChangeDic = new Dictionary<PopupType, Type>();
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
        }*/
    }

}

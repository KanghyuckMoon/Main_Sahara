using System;
using System.Collections;
using System.Collections.Generic;
using TimeManager;
using UI.EventManage;
using UI.Production;
using UnityEngine;

namespace UI.Popup
{
    public class TutorialPopup : MonoBehaviour, Observer
    {
        [SerializeField]
        private PopupTutorialDataSO popupTutorialDataSO ; 
        private PopupTutorialScreenPr popupTutorialScreenPr;

        private IPopup curStopPopup;

        private void Awake()
        {
            popupTutorialScreenPr = GetComponent<PopupTutorialScreenPr>(); 
            popupTutorialDataSO.AddObserver(this);
        }

        public void Receive()
        {
            CreatePopupTimeStop(new PopupTutorialData(popupTutorialDataSO));
        }

        public void CreatePopupTimeStop( PopupTutorialData _data = null) 
        {
            PopupTutorialPr _popupGetItemPr = new PopupTutorialPr();
            curStopPopup = _popupGetItemPr;
            
            popupTutorialScreenPr.Active(true);
            popupTutorialScreenPr.SetParent(_popupGetItemPr.Parent);
            _popupGetItemPr.ActiveTween();
            _popupGetItemPr.SetData(_data);

            // UI 입력 멈추고
            //UIMan.
            EventManager.Instance.TriggerEvent(EventsType.SetUIInput, false);
            EventManager.Instance.TriggerEvent(EventsType.SetPlayerCam, true);
            StaticTime.UITime = 0f;

            StartCoroutine(StayTimeStopPopupCo(_popupGetItemPr));

        }

        private IEnumerator StayTimeStopPopupCo(PopupTutorialPr _popup)
        {
            int _count = _popup.Data.detailImageAddressList.Count;
            int _idx = 1; 
            while (true)
            {
                // 설명 페이지가 2장 이상이고 마지막 페이지가 아니라면  
                if (_count >= 2 && _idx <_count)
                {
                    // 1보다 크면 
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _popup.SetDetail(_popup.Data.detailImageAddressList[_idx++]);
                    }
                    yield return null;
                }

                // Space를 누르세요 비활성화 
                // ESC를 누르세요 활성화 
                
                if (Input.GetKeyDown((KeyCode.Escape)))
                {
                    StaticTime.UITime = 1f;
                    popupTutorialScreenPr.Active(false);
                    curStopPopup.InActiveTween();
                    curStopPopup.Undo();
                    EventManager.Instance.TriggerEvent(EventsType.SetUIInput, true);
                    EventManager.Instance.TriggerEvent(EventsType.SetPlayerCam, false);
                    // UI 입력 멈춘거 풀고
                    // UI 닫고 
                }
                yield return null;

            }
        }


    }
}
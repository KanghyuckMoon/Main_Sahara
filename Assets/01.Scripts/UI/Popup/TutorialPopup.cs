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

            // UI �Է� ���߰�
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
                // ���� �������� 2�� �̻��̰� ������ �������� �ƴ϶��  
                if (_count >= 2 && _idx <_count)
                {
                    // 1���� ũ�� 
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _popup.SetDetail(_popup.Data.detailImageAddressList[_idx++]);
                    }
                    yield return null;
                }

                // Space�� �������� ��Ȱ��ȭ 
                // ESC�� �������� Ȱ��ȭ 
                
                if (Input.GetKeyDown((KeyCode.Escape)))
                {
                    StaticTime.UITime = 1f;
                    popupTutorialScreenPr.Active(false);
                    curStopPopup.InActiveTween();
                    curStopPopup.Undo();
                    EventManager.Instance.TriggerEvent(EventsType.SetUIInput, true);
                    EventManager.Instance.TriggerEvent(EventsType.SetPlayerCam, false);
                    // UI �Է� ����� Ǯ��
                    // UI �ݰ� 
                }
                yield return null;

            }
        }


    }
}
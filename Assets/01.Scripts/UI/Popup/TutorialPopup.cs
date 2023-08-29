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
        [SerializeField] private PopupTutorialDataSO popupTutorialDataSO;
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

        public void CreatePopupTimeStop(PopupTutorialData _data = null)
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
            //UIManager.Instance.ActiveCursor(true);
            
            StartCoroutine(StayTimeStopPopupCo(_popupGetItemPr));
        }


        private IEnumerator StayTimeStopPopupCo(PopupTutorialPr _popup)
        {
            int _count = _popup.Data.page;
            int _idx = 0;
            _popup.AddButtonEvt(PopupTutorialView.Buttons.left_button,() => ButtonEvt(false));
            _popup.AddButtonEvt(PopupTutorialView.Buttons.right_button,() => ButtonEvt(true));
            
            while (true)
            {
                if (_count >= 2 && _idx + 1 < _count)
                {
                    // ���� �������� 2�� �̻��̰� ������ �������� �ƴ϶��  
                    if (_idx != 0)
                    {
                        if (Input.GetKeyDown(KeyCode.RightArrow))
                            ButtonEvt(false);
                    }

                    if (_idx != _count)
                    {
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                            ButtonEvt(true);
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
                    //UIManager.Instance.ActiveCursor(false);
                    // UI �Է� ����� Ǯ��
                    // UI �ݰ� 
                    yield break;
                }

                yield return null;
            }

            void ButtonEvt(bool _isUp)
            {
                if (_isUp == true)
                {
                    ++_idx;
                    // ������ ��������� 
                    if (_idx + 1 >= _count)
                    {
                        _idx = _count - 1;
                        _popup.ActiveGuideLabel(true);
                        _popup.ActiveButton(false, false);
                    }

                    _popup.ActiveButton(true, true);
                }
                else
                {
                    --_idx;
                    // ù��° ��������� 
                    if (_idx <= 0)
                    {
                        _idx = 0;
                        _popup.ActiveButton(true, false);
                    }

                    _popup.ActiveGuideLabel(false);
                    _popup.ActiveButton(false, true);
                }

                _popup.SetDetail(_popup.Data.detailAddressList[_idx]);
                _popup.SetDetailImage(_popup.Data.detailImageAddressList[_idx]);
            }
        }
    }
}
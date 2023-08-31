using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Popup
{
    public class PopupTutorialEvent : MonoBehaviour
    {
        public PopupTutorialDataSO popupTutorialDataSo;

        [ContextMenu("이벤트 테스트")]
        public void OnEvent()
        {
            string _key = popupTutorialDataSo.key;
            GameEventManager.Instance.GetGameEvent(_key).Raise();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && popupTutorialDataSo != null)
            {
                OnEvent();
            }
        }
    }
}
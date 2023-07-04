using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Json;
using UI.EventManage;

namespace LoadScene
{
    public class DeadSceneLoad : MonoBehaviour
    {
        public void OnEnable()
        {
            EventManager.Instance.StartListening(EventsType.ActiveDeadCanvas, ActiveObj);
        }

        public void OnDisable()
        {
            EventManager.Instance.StopListening(EventsType.ActiveDeadCanvas, ActiveObj);
        }

        public void ActiveObj(object _isActive)
        {
            gameObject.SetActive((bool)_isActive);
        }

        public void ReStart()
        {
            SaveManager.Instance.IsContinue = true;
            SaveManager.Instance.isLoadSuccess = false;
            SceneManager.LoadScene("LoadingScene");
        }

        public void GotoTitle()
        {
            SceneManager.LoadScene("Title");
        }
    }
}
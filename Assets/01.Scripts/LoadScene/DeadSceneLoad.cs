using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Json;
using UI.EventManage;
using UI.Manager;

namespace LoadScene
{
    public class DeadSceneLoad : MonoBehaviour
    {
        public void Awake()
        {
            EventManager.Instance.StartListening(EventsType.ActiveDeadCanvas, ActiveObj);
            ActiveObj(false);
        }

        public void OnDestroy()
        {
            EventManager.Instance.StopListening(EventsType.ActiveDeadCanvas, ActiveObj);
        }

        public void ActiveObj(object _isActive)
        {
            gameObject.SetActive((bool)_isActive);
            UIManager.Instance.ActiveCursor((bool)_isActive);
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
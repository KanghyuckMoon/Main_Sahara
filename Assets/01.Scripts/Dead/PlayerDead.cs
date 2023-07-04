using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utill.Pattern;
using TimeManager;
using UI.EventManage;

namespace Module
{
    public class PlayerDead : MonoBehaviour, Observer
    {
        public float sceneMoveTime;
        public string sceneMoveName;

        private AbMainModule abMainModule;


        private void Start()
        {
            abMainModule = GetComponent<AbMainModule>();
            abMainModule.AddObserver(this);
        }

        private void ChangeScene()
        {
            SceneManager.LoadScene(sceneMoveName);
        }

		public void Receive()
        {
            if (abMainModule.IsDead)
            {
                StaticTime.EntierTime = 0f;
                EventManager.Instance.TriggerEvent(EventsType.ActiveDeadCanvas, true);
                //여기에 캔버스 활성화되는 코드 넣어주세요.
                //StartCoroutine(SceneMove());
            }
        }

        private IEnumerator SceneMove()
        {
            while (sceneMoveTime <= 0)
            {
                sceneMoveTime -= Time.deltaTime;
                yield return null;
            }
            ChangeScene();
        }
	}
}
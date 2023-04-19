using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

namespace Quest
{
    /// <summary>
    /// 퀘스트와 관련된 오브젝트, 퀘스트가 발견가능 상태가 되면 활성화된다.
    /// </summary>
    public class QuestObject : MonoBehaviour, Observer
    {
        [SerializeField]
        private string questKey;
        
        void Awake()
        {
            CheckQuestState();
        }

        private void CheckQuestState()
        {
            switch (QuestManager.Instance.questDataDic[questKey].QuestState)
            {
                default:
                case QuestState.Disable:
                    QuestManager.Instance.AddObserver(this);
                    gameObject.SetActive(false);
                    break;
                case QuestState.Discoverable:
                case QuestState.Active:
                case QuestState.Achievable:
                case QuestState.Clear:
                    QuestManager.Instance.RemoveObserver(this);
                    gameObject.SetActive(true);
                    break;
            }
        }

        public void Receive()
        {
            CheckQuestState();
        }
        
    }   
}

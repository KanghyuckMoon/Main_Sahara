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

        [SerializeField] 
        private bool isInvert = false;

        [SerializeField] private bool noneIncludeDiscoverable = false;
        
        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            CheckQuestState();
        }

        private void CheckQuestState()
        {
            var _questData = QuestManager.Instance.questDataDic[questKey];
            switch (_questData.QuestState)
            {
                default:
                case QuestState.Disable:
                    QuestManager.Instance.AddObserver(this);
                    gameObject.SetActive(isInvert);
                    break;
                case QuestState.Discoverable:
                    if (!noneIncludeDiscoverable)
                    {
                        QuestManager.Instance.RemoveObserver(this);
                        gameObject.SetActive(!isInvert);
                    }
                    break;
                case QuestState.Active:
                case QuestState.Achievable:
                case QuestState.Clear:
                    QuestManager.Instance.RemoveObserver(this);
                    gameObject.SetActive(!isInvert);
                    break;
            }
        }

        public void Receive()
        {
            CheckQuestState();
        }
        
    }   
}

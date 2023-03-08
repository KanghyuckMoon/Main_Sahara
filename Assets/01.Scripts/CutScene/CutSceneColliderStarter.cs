using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using Utill.Pattern;
using UnityEngine.Playables;
using Utill.Addressable;
using Module.Talk;
using Module;
using Cinemachine;
using Quest;
using CondinedModule;

namespace CutScene
{
    public class CutSceneColliderStarter : MonoBehaviour
    {
        [SerializeField]
        private CutSceneDataList cutSceneDataList = new CutSceneDataList();

        [SerializeField]
        private string questKey;
        [SerializeField]
        private QuestState questState;

        [SerializeField]
        private bool isPlayCutScene;

        private void OnTriggerEnter(Collider other)
        {
            if (!isPlayCutScene && other.CompareTag("Player") && Condition())
            {
                CutSceneManager.Instance.SetCutScene(cutSceneDataList);
                CutSceneManager.Instance.PlayCutScene();
                isPlayCutScene = true;
            }
        }

        private bool Condition()
        {
            if (questKey is null || questKey is "")
			{
                return true;
			}
            QuestData questData = QuestManager.Instance.GetQuestData(questKey);
            if (questState == questData.QuestState)
            {
                return true;
            }
            return false;
        }


    }
}

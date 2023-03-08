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

namespace CutScene
{
    public class CutSceneColliderStarter : MonoBehaviour
    {
        //CutScene
        private CutSceneType cutSceneType;

        //Target
        private Transform target1;
        private Transform target2;

        //Track
        private Transform trackTarget;
        private CinemachineSmoothPath smoothPath;

        //Talk
        private TalkModule talkModule;

        //Condition
        private string questKey;
        private QuestState questState;
        private bool isPlayCutScene;

        private void OnTriggerEnter(Collider other)
        {
            if(!isPlayCutScene && other.CompareTag("Player") && Condition())
            {
                CutSceneManager.Instance.SetCutScene(cutSceneType);
                switch (cutSceneType)
                {
                    case CutSceneType.PlayerToTarget:
                        CutSceneManager.Instance.SetTarget1(target1);
                        break;
                    case CutSceneType.PlayerToTrack:
                        CutSceneManager.Instance.SetTrackTarget(trackTarget);
                        CutSceneManager.Instance.SetCinemachineSmoothPath(smoothPath);
                        break;
                    case CutSceneType.PlayerToCutTarget:
                        CutSceneManager.Instance.SetTarget1(target1);
                        break;
                    case CutSceneType.PlayerToZoomInOut:
                        CutSceneManager.Instance.SetTarget1(target1);
                        break;
                    case CutSceneType.TargetToPlayer:
                        CutSceneManager.Instance.SetTarget1(target1);
                        break;
                    case CutSceneType.CutTargetToCutTarget:
                        CutSceneManager.Instance.SetTarget1(target1);
                        CutSceneManager.Instance.SetTarget2(target2);
                        break;
                    case CutSceneType.CutTargetToTarget:
                        CutSceneManager.Instance.SetTarget1(target1);
                        CutSceneManager.Instance.SetTarget2(target2);
                        break;
                }
                CutSceneManager.Instance.PlayCutScene();
                isPlayCutScene = true;
            }
        }

        private bool Condition()
        {
            QuestData questData = QuestManager.Instance.GetQuestData(questKey);
            if (questState == questData.QuestState)
            {
                return true;
            }
            return false;
        }


    }
}

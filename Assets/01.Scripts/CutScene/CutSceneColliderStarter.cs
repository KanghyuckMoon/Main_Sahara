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
        //CutScene
        [SerializeField]
        private CutSceneType cutSceneType;

        //Target
        [SerializeField]
        private Transform target1;
        [SerializeField]
        private Transform target2;

        //Track
        [SerializeField]
        private Transform trackTarget;
        [SerializeField]
        private CinemachineSmoothPath smoothPath;

        //Talk
        private TestTalkNPC testTalk;
        private TalkModule talkModule;

        //Condition
        [SerializeField]
        private bool isTalk;
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
                if (isTalk)
                {
                    talkModule = testTalk.GetModuleComponent<TalkModule>(ModuleType.Talk);
                    CutSceneManager.Instance.SetTalkModule(talkModule);
                    talkModule = null;
                }
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

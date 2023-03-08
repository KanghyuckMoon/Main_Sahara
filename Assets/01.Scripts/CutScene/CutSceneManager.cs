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

namespace CutScene
{
    public enum CutSceneType
    {
        None,
        PlayerToTarget = 1,
        PlayerToTrack = 2,
        PlayerToCutTarget = 3,
        PlayerToZoomInOut = 4,
        //PlayerToRotate = 5,
        TargetToPlayer = 6,
        CutTargetToCutTarget = 7, 
        //RotateToPlayer = 8,
        CutTargetToTarget = 9,
    }

    public class CutSceneManager : MonoSingleton<CutSceneManager>
    {
        [SerializeField]
        private PlayableDirector playableDirector;

        //Target
        private Transform target1;
        private Transform target2;

        //Track
        private Transform trackTarget;
        private CinemachineSmoothPath smoothPath;

        //Talk
        private TalkModule talkModule;

        //Base
        public void SetCutScene(CutSceneType _cutSceneType)
        {
            AllPropertyReset();
            playableDirector.playableAsset = AddressablesManager.Instance.GetResource<PlayableAsset>(_cutSceneType.ToString());
        }

        public void PlayCutScene()
        {
            playableDirector.Play();
        }
        public void ResumeCutScene()
        {
            playableDirector.Resume();
        }

        public void PauseCutScene()
        {
            playableDirector.Pause();
        }
        public void StopCutScene()
        {
            playableDirector.Stop();
        }

        public void AllPropertyReset()
        {
            target1 = null;
            target2 = null;
            trackTarget = null;
            smoothPath = null;
            talkModule = null;
        }

        //Target

        public void SetTarget1(Transform _target)
        {
            target1 = _target;
        }
        public void SetTarget2(Transform _target)
        {
            target2 = _target;
        }

        //Track
        public void SetTrackTarget(Transform _target)
        {
            trackTarget = _target;
        }
        public void SetCinemachineSmoothPath(CinemachineSmoothPath _smoothPath)
        {
            smoothPath = _smoothPath;
        }

        //Talk

        public void SetTalkModule(TalkModule _talkModule)
        {
            talkModule = _talkModule;
        }

        public void StartTalk()
        {
            if (talkModule is null)
            {
                return;
            }
            talkModule.Talk();
        }

    }
}

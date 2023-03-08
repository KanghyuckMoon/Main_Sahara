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
        public CamList CamList
		{
            get
			{
                camList ??= FindObjectOfType<CamList>();
                return camList;
            }
		}
        public PlayableDirector PlayableDirector
		{
            get
			{
                playableDirector ??= FindObjectOfType<PlayableDirector>();
                return playableDirector;
			}
            set
			{
                playableDirector = value;

            }
		}
		private CamList camList;
        private TimelineAsset timelineAsset;
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
            string _address = _cutSceneType.ToString();
            timelineAsset = AddressablesManager.Instance.GetResource<TimelineAsset>(_address);
        }

        public void PlayCutScene()
        {
            PlayableDirector.Play(timelineAsset);
        }
        public void ResumeCutScene()
        {
            PlayableDirector.Resume();
        }

        public void PauseCutScene()
        {
            PlayableDirector.Pause();
        }
        public void StopCutScene()
        {
            PlayableDirector.Stop();
        }

        public void AllPropertyReset()
        {
            playableDirector = null;
            camList = null;
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
            CamList.GetCam(CamType.TargetCam1).Follow = _target;
            CamList.GetCam(CamType.CutScneeZoomCam).Follow = _target;
        }
        public void SetTarget2(Transform _target)
        {
            target2 = _target;
            CamList.GetCam(CamType.TargetCam2).Follow = _target;
        }

        //Track
        public void SetTrackTarget(Transform _target)
        {
            trackTarget = _target;
            CamList.GetCam(CamType.TrackCam).Follow = _target;
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

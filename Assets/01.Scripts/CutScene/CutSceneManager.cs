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
using DG.Tweening;
using Quest;
using Module;
using Module.Talk;
using CondinedModule;
using EventObject;

namespace CutScene
{
    public enum CutSceneType
    {
        None,
        PlayerToTarget = 1,
        PlayerToTrack = 2,
        PlayerToTrack10f = 10,
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
        private CutSceneDataList cutSceneDataList;
        private int index = 0;
        private CamType praviouslastCamType = CamType.PlayerCam;
        private CamType lastCamType = CamType.PlayerCam;

        //Target
        private Transform target1;
        private Transform target2;

        //Track
        private Transform trackTarget;
        private CinemachineSmoothPath smoothPath;
        private float pathLength = 0f;
        private bool isTrack = false;
        private bool isForward = false;
        
        //Talk
        private TalkModule talkModule;
        private string talkKey;

        //ZoonInOut
        private float zoomInOutDistance;

        //Base
        public void SetCutScene(CutSceneDataList _cutSceneDataList)
        {
            AllPropertyReset();
            cutSceneDataList = _cutSceneDataList;
            index = 0;
            TalkModuleCutSceneOn();
        }

        public void SetCutScene(TimelineAsset _timelineAsset)
        {
            AllPropertyReset();
            index = 0;
            timelineAsset = _timelineAsset;
            playableDirector.Play(timelineAsset);
        }

        public void PlayCutScene()
        {
            CutSceneData _cutSceneData = cutSceneDataList.cutSceneDataList[index];
            string _address = _cutSceneData.cutSceneType.ToString();
            timelineAsset = AddressablesManager.Instance.GetResource<TimelineAsset>(_address);

            SettingParameterCutSceneData(_cutSceneData);


            if (index == 0)
			{
                PlayableDirector.Play(timelineAsset);
            }
            else
            {
                PlayableDirector.Pause();
                PlayableDirector.playableAsset = timelineAsset;
                playableDirector.Play();
                Playable playable = playableDirector.playableGraph.GetRootPlayable(0);
                playable.SetSpeed(0);
                PlayableDirector.time = 0.1F;
                playable.SetSpeed(1);
                PlayableDirector.Resume();
            }

            if (isTrack)
            {
                var _cinemachineTrackedDolly = CamList.GetCam(CamType.TrackCam).GetCinemachineComponent<CinemachineTrackedDolly>();
                _cinemachineTrackedDolly.m_PathPosition = 0f;
                float _time = 0f;
                if (_cutSceneData.cutSceneType == CutSceneType.PlayerToTrack)
                {
                    _time = 3f;
                }
                else if(_cutSceneData.cutSceneType == CutSceneType.PlayerToTrack10f)
                {
                    _time = 10f;
                }
                DOTween.To(() => _cinemachineTrackedDolly.m_PathPosition, x => _cinemachineTrackedDolly.m_PathPosition = x, 1, _time);
                if (isForward)
                {
                    var _cart = CamList.GetCameraDollyCart();
                    DOTween.To(() => _cart.m_Position, x => _cart.m_Position = x, 1, _time - 0.1f);
                }
            }

            if (_cutSceneData.eventObj != null)
            {
                _cutSceneData.eventObj.GetComponent<IEventObj>()?.PlayEvent();
            }
        }
        private void SettingParameterCutSceneData(CutSceneData _cutSceneData)
		{
            switch (_cutSceneData.cutSceneType)
            {
                case CutSceneType.PlayerToTarget:
                    CutSceneManager.Instance.SetTarget1(_cutSceneData.target1);
                    ChangeLastCam(CamType.TargetCam1);
                    break;
                case CutSceneType.PlayerToTrack:
                case CutSceneType.PlayerToTrack10f:
                    if (_cutSceneData.isNotUseTrackLookAt)
                    {
                        CutSceneManager.Instance.SetTrackTarget(null);
                    }
                    else
                    {
                        CutSceneManager.Instance.SetTrackTarget(_cutSceneData.trackTarget);
                    }
                    CutSceneManager.Instance.SetCinemachineSmoothPath(_cutSceneData.smoothPath);
                    ChangeLastCam(CamType.TrackCam);
                    break;
                case CutSceneType.PlayerToCutTarget:
                    CutSceneManager.Instance.SetTarget1(_cutSceneData.target1);
                    ChangeLastCam(CamType.TargetCam1);
                    break;
                case CutSceneType.PlayerToZoomInOut:
                    CutSceneManager.Instance.SetTarget1(_cutSceneData.target1);
                    CutSceneManager.Instance.SetZoomInOutDistance(_cutSceneData.zoomInOutDistance);
                    ChangeLastCam(CamType.CutSceneZoomCam);
                    break;
                case CutSceneType.TargetToPlayer:
                    CutSceneManager.Instance.SetTarget1(_cutSceneData.target1);
                    ChangeLastCam(CamType.PlayerCam);
                    break;
                case CutSceneType.CutTargetToCutTarget:
                    CutSceneManager.Instance.SetTarget1(_cutSceneData.target1);
                    CutSceneManager.Instance.SetTarget2(_cutSceneData.target2);
                    ChangeLastCam(CamType.TargetCam2);
                    break;
                case CutSceneType.CutTargetToTarget:
                    CutSceneManager.Instance.SetTarget1(_cutSceneData.target1);
                    CutSceneManager.Instance.SetTarget2(_cutSceneData.target2);
                    ChangeLastCam(CamType.TargetCam2);
                    break;
            }
            if (_cutSceneData.isTalk)
            {
                _cutSceneData.talkModule = _cutSceneData.testTalk.GetModuleComponent<TalkModule>(ModuleType.Talk);
                CutSceneManager.Instance.SetTalkModule(_cutSceneData.talkModule, _cutSceneData.talkKey);
                talkModule.SetCutScene(true);
            }
            else
			{
                talkModule = null;
			}
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
        public void NextCutScene()
        {
            //퀘스트 조건이 달려있으면 퀘스트 클리어
            if(cutSceneDataList.cutSceneDataList[index].questKey != null)
			{
				switch (cutSceneDataList.cutSceneDataList[index].questState)
				{
					case QuestState.Discoverable:
                        QuestManager.Instance.ChangeQuestDiscoverable(cutSceneDataList.cutSceneDataList[index].questKey);
                        break;
					case QuestState.Active:
                        QuestManager.Instance.ChangeQuestActive(cutSceneDataList.cutSceneDataList[index].questKey);
                        break;
					case QuestState.Achievable:
					case QuestState.Clear:
                        QuestManager.Instance.ChangeQuestClear(cutSceneDataList.cutSceneDataList[index].questKey);
                        break;
				}
			}

			if (cutSceneDataList.cutSceneDataList[index].isTalk)
            {
                PlayableDirector.Pause();
                StartCoroutine(WaitEndTalk());
            }
            else
            {
                if (cutSceneDataList is null)
                {
                    ResetCam();
                    return;
                }
                if (cutSceneDataList.cutSceneDataList.Count > index + 1)
                {
                    SetContinueCam();
                    index++;
                    PlayCutScene();
                }
                else
                {
                    ResetCam();
                    TalkModuleCutSceneOff();
                    return;
                }
            }

		}

		private IEnumerator WaitEndTalk()
		{
            while(!talkModule.IsEndTalk)
			{
                yield return null;
            }
            if (cutSceneDataList.cutSceneDataList.Count > index + 1)
            {
                SetContinueCam();
                index++;
                PlayCutScene();
            }
            else
            {
                TalkModuleCutSceneOff();
                ResetCam();
            }

        }

        public void ResetCam()
		{
            CamList.GetCam(CamType.PlayerCam).gameObject.SetActive(true);
            
            if (lastCamType is not CamType.PlayerCam)
			{
                CamList.GetCam(lastCamType).gameObject.SetActive(false);
            }
            if (praviouslastCamType is not CamType.PlayerCam)
			{
                CamList.GetCam(praviouslastCamType).gameObject.SetActive(false);
			}
        }

        public void SetContinueCam()
        {
            CamList.GetCam(lastCamType).gameObject.SetActive(false);
            CamList.GetCam(lastCamType).gameObject.SetActive(true);
            CamList.GetCam(praviouslastCamType).gameObject.SetActive(false);
        }

        public void ChangeLastCam(CamType _camType)
		{
            praviouslastCamType = lastCamType;
            lastCamType = _camType;
        }

        public void AllPropertyReset()
        {
            praviouslastCamType = CamType.PlayerCam;
            lastCamType = CamType.PlayerCam;
            pathLength = 0f;
            isTrack = false;
            cutSceneDataList = null;
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
            CamList.GetCam(CamType.CutSceneZoomCam).Follow = _target;
        }
        public void SetTarget2(Transform _target)
        {
            target2 = _target;
            CamList.GetCam(CamType.TargetCam2).Follow = _target;
        }

        public void SetZoomInOutDistance(float _distance)
        {
            zoomInOutDistance = _distance;
            CamList.GetCam(CamType.CutSceneZoomCam).GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = _distance;
        }

        //Track
        public void SetTrackTarget(Transform _target)
        {
            if(_target is not null)
            {
                trackTarget = _target;
                CamList.GetCam(CamType.TrackCam).LookAt = trackTarget;
                isForward = false;
            }
            else
            {
                trackTarget = CamList.GetCameraForwardTarget();
                CamList.GetCam(CamType.TrackCam).LookAt = trackTarget;
                isForward = true;
            }
        }
        public void SetCinemachineSmoothPath(CinemachineSmoothPath _smoothPath)
        {
            smoothPath = _smoothPath;
            CamList.GetCam(CamType.TrackCam).GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = _smoothPath;
            CamList.GetCameraDollyCart().m_Path = _smoothPath;
            pathLength = _smoothPath.PathLength;
            isTrack = true;
        }

        //Talk

        public void SetTalkModule(TalkModule _talkModule, string _talkKey)
        {
            talkModule = _talkModule;
            talkKey = _talkKey;
        }

        public void StartTalk()
        {
            if (talkModule is null)
            {
                return;
            }
            talkModule.CutSceneTalk(talkKey);
        }

        public void TalkModuleCutSceneOff()
		{
            foreach (var _obj in cutSceneDataList.cutSceneDataList)
			{
                if(_obj.isTalk)
                {
                    _obj?.testTalk?.GetModuleComponent<TalkModule>(ModuleType.Talk)?.SetCutScene(false);
				}
			}
        }
        public void TalkModuleCutSceneOn()
        {
            foreach (var _obj in cutSceneDataList.cutSceneDataList)
            {
                if(_obj.isTalk)
                {
                    _obj?.testTalk?.GetModuleComponent<TalkModule>(ModuleType.Talk)?.SetCutScene(true);
                }
            }
        }

	}

	[System.Serializable]
    public class CutSceneDataList
	{
        public List<CutSceneData> cutSceneDataList = new List<CutSceneData>();
    }

    [System.Serializable]
    public class CutSceneData
    {
        //CutScene
        public CutSceneType cutSceneType;

        //Target
        public Transform target1;
        public Transform target2;

        //Track
        public Transform trackTarget;
        public CinemachineSmoothPath smoothPath;

        //Talk
        public TestTalkNPC testTalk;
        public TalkModule talkModule;
        public string talkKey;
        
        //ZoomInOut
        public float zoomInOutDistance;

        //Clear Quest
        public string questKey;
        public QuestState questState;

        //Event
        public GameObject eventObj;

        //Condition
        public bool isTalk;
        public bool isNotUseTrackLookAt;
        
        

    }
}

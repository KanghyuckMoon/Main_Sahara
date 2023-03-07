using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using Utill.Pattern;
using UnityEngine.Playables;
using Utill.Addressable;
using Module.Talk;
using Module;

namespace CutScene
{
    public enum CutSceneType
    {
        None,
        PlayerToTarget,
    }

    public class CutSceneManager : MonoSingleton<CutSceneManager>
    {
        [SerializeField]
        private PlayableDirector playableDirector;

        //Talk
        private TalkModule talkModule;

        public void SetCutScene(CutSceneType _cutSceneType)
        {
            playableDirector.playableAsset = AddressablesManager.Instance.GetResource<PlayableAsset>(_cutSceneType.ToString());
        }

        public void StartCutScene()
        {
            playableDirector.Play();
        }

        public void StopCutScene()
        {
            playableDirector.Stop();
        }

        public void SetTalkModule(TalkModule _talkModule)
        {
            talkModule = _talkModule;
        }

        public void StartTalk()
        {
            talkModule.Talk();
        }

        private void AllPropertyReset()
        {
            talkModule = null;
        }

    }
}

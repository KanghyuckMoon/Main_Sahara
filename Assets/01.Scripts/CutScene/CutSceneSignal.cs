using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CutScene
{
    public class CutSceneSignal : MonoBehaviour
    {
        public void CutScenePause()
        {
            CutSceneManager.Instance.PauseCutScene();
        }
        public void CutSceneStop()
        {
            CutSceneManager.Instance.StopCutScene();
        }
        public void CutScenePlay()
        {
            CutSceneManager.Instance.PlayCutScene();
        }
        public void CutSceneResume()
        {
            CutSceneManager.Instance.ResumeCutScene();
        }
        public void CutSceneTalk()
        {
            CutSceneManager.Instance.StartTalk();
        }
        public void CutNextCutScene()
        {
            CutSceneManager.Instance.NextCutScene();
        }
        public void CutSetCam()
        {
            CutSceneManager.Instance.SetContinueCam();
        }
    }
}

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
    public class CutSceneHaver : MonoBehaviour
    {
        public string Key
		{
            get
			{
                return key;
			}
		}

		private string key;

        [SerializeField]
        private CutSceneDataList cutSceneDataList = new CutSceneDataList();

        public void PlayCutScene()
        {
            CutSceneManager.Instance.SetCutScene(cutSceneDataList);
            CutSceneManager.Instance.PlayCutScene();
        }

    }
}
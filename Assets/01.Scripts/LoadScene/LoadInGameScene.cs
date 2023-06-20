using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utill.Measurement;
using Utill.Coroutine;
using Utill.Addressable;
using UpdateManager;
using Json;
using Pool;
using Streaming;
using UI.Manager;
using TimeManager;

namespace LoadScene
{
    public class LoadInGameScene : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //UpdateManager.UpdateManager.Clear();
            //AddressablesManager.Instance.LodedSceneClear();
            //ClassPoolManager.Instance.Clear();
            //ObjectPoolManager.Instance.Clear();
            //System.GC.Collect(); 
            //UIManager.Instance.Init();
            //StaticCoroutineManager.Instance.InstanceDoCoroutine(LoadingScene());
            SceneManager.LoadScene("InGame");
        }
    }

}
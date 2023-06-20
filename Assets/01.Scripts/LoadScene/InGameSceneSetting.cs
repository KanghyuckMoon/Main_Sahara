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

public class InGameSceneSetting : MonoBehaviour
{
    private static bool isLoading;
        // Start is called before the first frame update
        void Start()
        {
            if (isLoading)
            {
                return;
            }

            isLoading = true;
            UpdateManager.UpdateManager.Clear();
            AddressablesManager.Instance.LodedSceneClear();
            ClassPoolManager.Instance.Clear();
            ObjectPoolManager.Instance.Clear();
            System.GC.Collect(); 
            UIManager.Instance.Init();
            StartCoroutine(LoadingScene());
            //StaticCoroutineManager.Instance.InstanceDoCoroutine(LoadingScene());
            //SceneManager.LoadScene("InGame");
        }


        private IEnumerator LoadingScene()
        {
            Debug.Log("Time Stop");
            Time.timeScale = 1;
            StaticTime.EntierTime = 0f;
            
            Debug.Log("Scene Check Start");
            while (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("InGame"))
            {
                Debug.Log("Scene Checking");
                yield return null;
            }

            //var cam = Camera.main;
            //cam.gameObject.SetActive(false);
            Debug.Log("Scene Check end");
            
            Debug.Log("Load TipScene Start");
            var op6 = SceneManager.LoadSceneAsync("TipScene", LoadSceneMode.Additive);
            op6.allowSceneActivation = false;
            while (op6.progress < 0.9f)
            {
                Debug.Log("Loading TipScene");
                Logging.Log(op6.progress);
                yield return null;
            }
            op6.allowSceneActivation = true;
            Debug.Log("Load TipScene Success");
            var op3 = SceneManager.LoadSceneAsync("Player", LoadSceneMode.Additive);
            op3.allowSceneActivation = false;
            
            Debug.Log("PlayerScene Start");
            while (op3.progress < 0.9f)
            {
                Logging.Log(op3.progress);
                yield return null;
            }
            op3.allowSceneActivation = true;
            Debug.Log("PlayerScene End");
            
            var op4 = SceneManager.LoadSceneAsync("Quest", LoadSceneMode.Additive);
            op4.allowSceneActivation = false;
            Debug.Log("Quest Scene Start");
            while (op4.progress < 0.9f)
            {
                Logging.Log(op4.progress);
                yield return null;
            }
            op4.allowSceneActivation = true;
            Debug.Log("Quest Scene End");
            
            var op5 = SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);
            op5.allowSceneActivation = false;
            Debug.Log("UI Scene Start");
            while (op5.progress < 0.9f)
            {
                Logging.Log(op4.progress);
                yield return null;
            }
            op5.allowSceneActivation = true;
            Debug.Log("UI Scene End");

            while (PlayerObj.Player == null)
            {
                Debug.Log("PlayerObj Loading");
                yield return null;
            }
            
            Streaming.StreamingManager.Instance.IsSetting = false;
            Streaming.StreamingManager.Instance.LoadReadyScene();
            Debug.Log("SceneManager LoadReady");
            
            //yield return new WaitForSeconds(1f);
            
            Debug.Log("SceneManager IsSetting Start");
            while (!Streaming.StreamingManager.Instance.IsCurrentSceneSetting)
            {
                Debug.Log("CurrentSetting");
                yield return null;
            }
            Debug.Log("SceneManager IsSetting Success");


            //if (SaveManager.Instance.IsContinue)
            //{
            //    while (!SaveManager.Instance.isLoadSuccess)
            //    {
            //        try
            //        {
            //            SaveManager.Instance.Load(SaveManager.Instance.TestDate);
            //        }
            //        catch
            //        {
            //        }
            //        yield return new WaitForSecondsRealtime(1f);
            //    }
			//}
            //Debug.Log("IsLoadEnd Start");
            //while (!StreamingManager.Instance.IsLoadEnd())
            //{
            //    Debug.Log("Loading");
            //    yield return null;
            //}
            Debug.Log("Loading Success");
            Debug.Log("Unloading TipScene");
            //cam.gameObject.SetActive(true);
            var uop2 = SceneManager.UnloadSceneAsync("TipScene", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            Debug.Log("Unload TipScene");
            while (!Streaming.StreamingManager.Instance.IsSetting)
            {
                Debug.Log("Setting");
                yield return null;
            }
            StaticTime.EntierTime = 1f;
            Time.timeScale = 1;
            //GameManager.GamePlayerManager.Instance.IsPlaying = true;
            isLoading = false;
            Debug.Log("Time Start");
        }
}

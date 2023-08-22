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
using Utill.Measurement;

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
            Logging.Log("Time Stop");
            Time.timeScale = 1;
            StaticTime.EntierTime = 0f;
            
            //Debug.Log("Scene Check Start");
            while (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("InGame"))
            {
                Logging.Log("Scene Checking");
                yield return null;
            }

            //var cam = Camera.main;
            //cam.gameObject.SetActive(false);
            //Debug.Log("Scene Check end");
            
            Logging.Log("Load TipScene Start");
            SceneManager.LoadScene("TipScene", LoadSceneMode.Additive);
            yield return new WaitForSeconds(3f);
            //var op6 = SceneManager.LoadSceneAsync("TipScene", LoadSceneMode.Additive);
            //op6.allowSceneActivation = false;
            //while (op6.progress < 0.9f)
            //{
            //    Debug.Log("Loading TipScene");
            //    Logging.Log(op6.progress);
            //    yield return null;
            //}
            //op6.allowSceneActivation = true;
            Logging.Log("Load TipScene Success");
            var op3 = SceneManager.LoadSceneAsync("Player", LoadSceneMode.Additive);
            op3.allowSceneActivation = false;
            
            Logging.Log("PlayerScene Start");
            while (op3.progress < 0.9f)
            {
                Logging.Log(op3.progress);
                yield return null;
            }
            op3.allowSceneActivation = true;
            Logging.Log("PlayerScene End");
            yield return new WaitForSeconds(1f);
            
            var op4 = SceneManager.LoadSceneAsync("Quest", LoadSceneMode.Additive);
            op4.allowSceneActivation = false;
            Logging.Log("Quest Scene Start");
            while (op4.progress < 0.9f)
            {
                Logging.Log(op4.progress);
                yield return null;
            }
            op4.allowSceneActivation = true;
            Logging.Log("Quest Scene End");
            yield return new WaitForSeconds(1f);
            
            var op5 = SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);
            op5.allowSceneActivation = false;
            Logging.Log("UI Scene Start");
            while (op5.progress < 0.9f)
            {
                Logging.Log(op4.progress);
                yield return null;
            }
            op5.allowSceneActivation = true;
            Logging.Log("UI Scene End");
            yield return new WaitForSeconds(1f);

            while (PlayerObj.Player == null)
            {
                Logging.Log("PlayerObj Loading");
                yield return null;
            }

            GameEventManager.Instance.GetGameEvent("0.InitSceneLoadStart");
            Streaming.StreamingManager.Instance.IsSetting = false;
            Streaming.StreamingManager.Instance.LoadReadyScene();
            Logging.Log("SceneManager LoadReady");
            
            yield return new WaitForSeconds(1f);
            
            Logging.Log("SceneManager IsSetting Start");
            while (!Streaming.StreamingManager.Instance.IsCurrentSceneSetting)
            {
                Logging.Log("CurrentSetting");
                yield return null;
            }
            Logging.Log("SceneManager IsSetting Success");


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
            Logging.Log("Loading Success");
            Logging.Log("Unloading TipScene");
            //cam.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            
            var uop2 = SceneManager.UnloadSceneAsync("TipScene", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            Logging.Log("Unload TipScene");
            while (!Streaming.StreamingManager.Instance.IsSetting)
            {
                Logging.Log("Setting");
                yield return null;
		}
		    GameEventManager.Instance.GetGameEvent("1.InitSceneLoadEnd");
		    StaticTime.EntierTime = 1f;
            Time.timeScale = 1;
            //GameManager.GamePlayerManager.Instance.IsPlaying = true;
            isLoading = false;
            Logging.Log("Time Start");
        }
}

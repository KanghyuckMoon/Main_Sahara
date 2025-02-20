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
using UI.Manager;

namespace LoadScene
{
    public class LoadInGameScene : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            UpdateManager.UpdateManager.Clear();
            AddressablesManager.Instance.LodedSceneClear();
            ClassPoolManager.Instance.Clear();
            ObjectPoolManager.Instance.Clear();
            System.GC.Collect(); 
            UIManager.Instance.Init();
            StaticCoroutineManager.Instance.InstanceDoCoroutine(LoadingScene());
            SceneManager.LoadScene("InGame");
        }


        private IEnumerator LoadingScene()
        {
            Time.timeScale = 0;
            while (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("InGame"))
			{
                yield return new WaitForSecondsRealtime(1f);
			}
            //var op2 = SceneManager.LoadSceneAsync("InGame", LoadSceneMode.Additive);
            var op6 = SceneManager.LoadSceneAsync("TipScene", LoadSceneMode.Additive);
            //op2.priority = 3;
            //op2.allowSceneActivation = false;
            op6.allowSceneActivation = false;
            //Time.timeScale = 0;
            //while (op2.progress < 0.9f)
            //{
            //    Logging.Log(op2.progress);
            //    yield return null;
            //}
            while (op6.progress < 0.9f)
            {
                Logging.Log(op6.progress);
                yield return null;
            }
            op6.allowSceneActivation = true;
            //SceneManager.UnloadScene("LoadingScene");
            //var uop = SceneManager.UnloadSceneAsync("LoadingScene", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);

            //op2.allowSceneActivation = true;
            StaticCoroutineManager.Instance.InstanceDoCoroutine(Streaming.StreamingManager.Instance.LoadReadyScene());
            while (!Streaming.StreamingManager.Instance.IsSetting)
			{
                yield return null;
			}
            yield return new WaitForSecondsRealtime(1);

            var op3 = SceneManager.LoadSceneAsync("Player", LoadSceneMode.Additive);
            var op4 = SceneManager.LoadSceneAsync("Quest", LoadSceneMode.Additive);
            var op5 = SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);
            op3.allowSceneActivation = false;
            op4.allowSceneActivation = false;
            op5.allowSceneActivation = false;

            while (op3.progress < 0.9f)
            {
                Logging.Log(op3.progress);
                yield return null;
            }
            while (op4.progress < 0.9f)
            {
                Logging.Log(op4.progress);
                yield return null;
            }
            while (op5.progress < 0.9f)
            {
                Logging.Log(op4.progress);
                yield return null;
            }
            op3.allowSceneActivation = true;
            op4.allowSceneActivation = true;
            op5.allowSceneActivation = true;

            if (SaveManager.Instance.IsContinue)
            {
                while (!SaveManager.Instance.isLoadSuccess)
                {
                    try
                    {
                        SaveManager.Instance.Load(SaveManager.Instance.TestDate);
                    }
                    catch
                    {
                    }
                    yield return new WaitForSecondsRealtime(1f);
                }
			}

            var uop2 = SceneManager.UnloadSceneAsync("TipScene", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            Time.timeScale = 1;
        }
    }

}
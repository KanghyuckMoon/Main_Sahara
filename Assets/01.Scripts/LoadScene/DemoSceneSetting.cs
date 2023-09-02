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

public class DemoGameSceneSetting : MonoBehaviour
{
	void Start()
	{
		UpdateManager.UpdateManager.Clear();
		AddressablesManager.Instance.LodedSceneClear();
		ClassPoolManager.Instance.Clear();
		ObjectPoolManager.Instance.Clear();
		System.GC.Collect();
		UIManager.Instance.Init();
		StartCoroutine(LoadingScene());
	}


	private IEnumerator LoadingScene()
	{
		//Debug.Log("Time Stop");
		//Time.timeScale = 1;
		//StaticTime.EntierTime = 0f;


		//Debug.Log("Load TipScene Start");
		//SceneManager.LoadScene("TipScene", LoadSceneMode.Additive);
		//yield return new WaitForSeconds(1f);

		var op5 = SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);
		op5.allowSceneActivation = false;
		Debug.Log("UI Scene Start");
		while (op5.progress < 0.9f)
		{
			yield return null;
		}

		op5.allowSceneActivation = true;
		//Debug.Log("UI Scene End");

		//GameEventManager.Instance.GetGameEvent("0.InitSceneLoadStart").Raise();

		//yield return new WaitForSeconds(1f);

		//var uop2 = SceneManager.UnloadSceneAsync("TipScene", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
		//GameEventManager.Instance.GetGameEvent("1.InitSceneLoadEnd").Raise();
		//StaticTime.EntierTime = 1f;
		//Time.timeScale = 1;
		////isLoading = false;
		//Debug.Log("Time Start");
	}
}

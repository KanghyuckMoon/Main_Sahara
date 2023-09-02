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
		//UpdateManager.UpdateManager.Clear();
		AddressablesManager.Instance.LodedSceneClear();
		ClassPoolManager.Instance.Clear();
		ObjectPoolManager.Instance.Clear();
		System.GC.Collect();
		UIManager.Instance.Init();
		SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);
		//StartCoroutine(LoadingScene());
	}


	//private IEnumerator LoadingScene()
	//{
	//	var op5 = SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);
	//	op5.allowSceneActivation = false;
	//	Debug.Log("UI Scene Start");
	//	while (op5.progress < 0.9f)
	//	{
	//		yield return null;
	//	}
	//
	//	op5.allowSceneActivation = true;
	//}
}

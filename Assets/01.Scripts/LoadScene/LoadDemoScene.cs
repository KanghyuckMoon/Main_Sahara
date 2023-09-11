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
	public class LoadDemoScene : MonoBehaviour
	{
		void Start()
		{
			StartCoroutine(LoadingDemo());
		}

		private IEnumerator LoadingDemo()
		{
			yield return null;
			var op = SceneManager.LoadSceneAsync("DemoScene");
			op.allowSceneActivation = false;
			float timer = 0.0f;
			while (!op.isDone)
			{
				yield return null;
				timer += Time.deltaTime;
				if (op.progress < 0.9f)
				{

				}
				else
				{
					op.allowSceneActivation = true;
					yield break;
				}
			}

		}
	}

}
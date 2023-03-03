using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Json;

namespace LoadScene
{

	public class DeadSceneLoad : MonoBehaviour
	{
		public void ReStart()
		{
			SaveManager.Instance.IsContinue = true;
			SaveManager.Instance.isLoadSuccess = false;
			SceneManager.LoadScene("LoadingScene");
		}

		public void GotoTitle()
		{
			SceneManager.LoadScene("Title");
		}
	}
}

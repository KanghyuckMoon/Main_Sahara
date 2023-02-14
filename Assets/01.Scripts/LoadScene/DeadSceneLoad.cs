using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadScene
{

	public class DeadSceneLoad : MonoBehaviour
	{
		public void ReStart()
		{
			SceneManager.LoadScene("LoadingScene");
		}

		public void GotoTitle()
		{
			SceneManager.LoadScene("Title");
		}


	}
}

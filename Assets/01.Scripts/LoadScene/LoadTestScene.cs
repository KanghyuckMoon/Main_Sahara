using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadScene
{
	public class LoadTestScene : MonoBehaviour
	{
		public string sceneName;

		[ContextMenu("LoadScene")]
		public void LoadScene()
		{
			SceneManager.LoadScene(sceneName);
		}


	}
}

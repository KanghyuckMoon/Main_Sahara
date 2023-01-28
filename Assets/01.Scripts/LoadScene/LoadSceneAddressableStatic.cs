using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;

namespace LoadScene
{
	public static class LoadSceneAddressableStatic
	{
		public static void LoadScene(string sceneName)
		{
			AddressablesManager.Instance.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single, null);
		}
		public static void LoadSceneAsync(string sceneName)
		{
			AddressablesManager.Instance.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single, null);
		}
	}

}
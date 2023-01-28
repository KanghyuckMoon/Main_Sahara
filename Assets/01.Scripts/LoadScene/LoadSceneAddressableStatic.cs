using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;

namespace LoadScene
{
	public static class LoadSceneAddressableStatic
	{
		public static void LoadScene(string sceneName, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode)
		{
			AddressablesManager.Instance.LoadScene(sceneName, loadSceneMode, null);
		}
		public static void LoadSceneAsync(string sceneName, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode)
		{
			AddressablesManager.Instance.LoadSceneAsync(sceneName, loadSceneMode, null);
		}
	}

}
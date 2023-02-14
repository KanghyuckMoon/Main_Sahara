using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadScene
{
    public class LoadSubSceneAddressable : MonoBehaviour
    {
        [SerializeField]
        private List<string> subSceneAddressList = new List<string>();

		private void Start()
		{
			foreach (string address in subSceneAddressList)
			{
				SceneManager.LoadScene(address, UnityEngine.SceneManagement.LoadSceneMode.Additive);
				//LoadSceneAddressableStatic.LoadScene(address, UnityEngine.SceneManagement.LoadSceneMode.Additive);
			}
		}
	}
}

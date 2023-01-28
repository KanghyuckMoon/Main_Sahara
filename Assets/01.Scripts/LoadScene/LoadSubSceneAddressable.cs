using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
				LoadSceneAddressableStatic.LoadScene(address, UnityEngine.SceneManagement.LoadSceneMode.Additive);
			}
		}
	}
}

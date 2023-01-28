using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoadScene
{
    public class LoadInGameScene : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            LoadSceneAddressableStatic.LoadSceneAsync("InGame", UnityEngine.SceneManagement.LoadSceneMode.Single);
            LoadSceneAddressableStatic.LoadSceneAsync("PlayerScene", UnityEngine.SceneManagement.LoadSceneMode.Additive);
            LoadSceneAddressableStatic.LoadSceneAsync("UIScene", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
    }

}
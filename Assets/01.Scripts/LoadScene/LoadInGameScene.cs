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
            //LoadSceneAddressableStatic.LoadSceneAsync("UIScene");
            LoadSceneAddressableStatic.LoadSceneAsync("InGame");
        }
    }

}
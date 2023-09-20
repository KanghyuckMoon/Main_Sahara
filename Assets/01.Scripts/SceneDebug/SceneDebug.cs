using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utill.Pattern;

public class SceneDebug : MonoSingleton<SceneDebug>
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
		{
            //SceneManager.LoadScene("DeadScene");
		}
    }
}

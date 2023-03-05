using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Json;

public class SaveLoadComponent : MonoBehaviour
{
    void Start()
    {
        SaveManager.Instance.IsContinue = true;
        SaveManager.Instance.isLoadSuccess = false;
        SceneManager.LoadScene("LoadingScene");
    }

}

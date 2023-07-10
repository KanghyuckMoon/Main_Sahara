using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddTipScene : MonoBehaviour
{
public void Start()
{
    SceneManager.LoadScene("TipScene", LoadSceneMode.Additive);
}
}

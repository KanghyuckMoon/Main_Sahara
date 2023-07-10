using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Measurement;

public class PlayerObj : MonoBehaviour
{
    public static GameObject Player;

    public void Awake()
    {
        Player = gameObject;
        Logging.Log("Set Player Obj");
    }
    
    public void OnDestroy()
    {
        Player = null;
        Logging.Log("Destroy Player Obj");
    }
}

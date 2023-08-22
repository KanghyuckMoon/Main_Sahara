using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : MonoBehaviour
{
    public static GameObject Player;

    public void Awake()
    {
        Player = gameObject;
        Debug.Log("Set Player Obj");
    }
    
    public void OnDestroy()
    {
        Player = null;
        Debug.Log("Destroy Player Obj");
    }
}

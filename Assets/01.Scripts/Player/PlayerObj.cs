using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : MonoBehaviour
{
    public static GameObject Player;

    public void Awake()
    {
        Player = gameObject;
    }
}

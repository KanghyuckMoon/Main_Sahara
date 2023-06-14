using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Col : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Player"))
        {
            Debug.LogError("플레이어 닿았다!!!!! 플레이어 닿았다!!");
        }
    }
}

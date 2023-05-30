using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyComponent
{
public class LookAtPlayer : MonoBehaviour
{
    private void LateUpdate()
    {
        if (PlayerObj.Player is null)
        {
            return;
        }
        transform.LookAt(PlayerObj.Player.transform.position + Vector3.up * 2f); 
    }
}
}

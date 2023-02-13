using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Test_Test : MonoBehaviour
{
    [Tooltip("카메라의 방향")] public Vector2 camPos;

    public float speed;
    CharacterController characterController;
    private void Start()
    {
        characterController = GetComponentInParent<CharacterController>();
    }

    public void Move(Vector2 pos)
    {
        Vector3 direction = new Vector3(pos.x, 0, pos.y).normalized;

        characterController.SimpleMove(direction * speed);
    }

    public void RotateCamera(Vector2 direct)
    {
        camPos = direct;
    }
}

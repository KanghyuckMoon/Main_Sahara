using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullMapCamController : MonoBehaviour
{
    // ĳ�� ���� 
    private Camera cam;

    // �ν����� ���� 
    [SerializeField]
    private Transform minPos;
    [SerializeField]
    private Transform maxPos;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float zoomSpeed;
    [SerializeField]
    private float maxZoomValue;
    [SerializeField]
    private float minZoomValue; 

    // �����̺� 
    private float zoomValue;
    private float xMoveValue;
    private float yMoveValue;

    // ������Ƽ 
    private Vector2 MoveDir => new Vector2(xMoveValue, yMoveValue).normalized;



    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        KeyInput(); 
        ControlCam(); 
    }

    /// <summary>
    /// ī�޶� ����
    /// </summary>
    private void ControlCam()
    {
        MoveCam();
        ZoomCam(); 
    }

    private void KeyInput()
    {
        // ������ 
        if (Input.GetKey(KeyCode.W))
        {
            yMoveValue = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            xMoveValue = -1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            yMoveValue = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            xMoveValue = 1f;
        }

        // Ű ������ �ʱ�ȭ 
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            yMoveValue = 0f;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            xMoveValue = 0f;
        }

        zoomValue = Input.GetAxis("Mouse ScrollWheel");
    }
    /// <summary>
    /// ī�޶� ������ (�����¿�)
    /// </summary>
    private void MoveCam()
    {
        float x = 0f,y = 0f;      
        transform.Translate(MoveDir * (moveSpeed + cam.orthographicSize) * Time.deltaTime);
        x = Mathf.Clamp(transform.position.x, minPos.position.x, maxPos.position.x);
        y = Mathf.Clamp(transform.position.y, minPos.position.y, maxPos.position.y);
        transform.position = new Vector2(x, y); 
    }

    /// <summary>
    /// ī�޶� Ȯ�� ��� 
    /// </summary>
    private void ZoomCam()
    {
        float v = zoomValue * (zoomSpeed+ cam.orthographicSize) * Time.deltaTime * -1;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + v, minZoomValue, maxZoomValue);

    }
}

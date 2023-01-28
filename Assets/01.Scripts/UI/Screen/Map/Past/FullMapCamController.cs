using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullMapCamController : MonoBehaviour
{
    // 캐싱 변수 
    private Camera cam;

    // 인스펙터 참조 
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

    // 프라이빗 
    private float zoomValue;
    private float xMoveValue;
    private float yMoveValue;

    // 프로퍼티 
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
    /// 카메라 조작
    /// </summary>
    private void ControlCam()
    {
        MoveCam();
        ZoomCam(); 
    }

    private void KeyInput()
    {
        // 움직임 
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

        // 키 뗐을때 초기화 
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
    /// 카메라 움직임 (상하좌우)
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
    /// 카메라 확대 축소 
    /// </summary>
    private void ZoomCam()
    {
        float v = zoomValue * (zoomSpeed+ cam.orthographicSize) * Time.deltaTime * -1;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + v, minZoomValue, maxZoomValue);

    }
}

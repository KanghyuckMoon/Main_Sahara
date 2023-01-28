using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utill.Measurement
{
    public class FreeCameraUtill : MonoBehaviour
    {
        [SerializeField, Header("F를 누르면 마우스 고정 변경")]
        private bool isMouseLock = false;
        [SerializeField]
        private float moveSpeed = 15f;
        [SerializeField]
        private float rotateSpeed = 4f;

        private Vector3 pos;
        private float xRotate;

        // Update is called once per frame
        void Update()
        {
            Rotation();
            Move();
            ChangeMouseMode();

            if (isMouseLock)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

        }

        private void Move()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float upDown = 0f;

            if (Input.GetKey(KeyCode.Space))
            {
                upDown = 1f;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                upDown = -1f;
            }

            Vector3 right = transform.right * horizontal;
            Vector3 up = Vector3.up * upDown;
            Vector3 forward = transform.forward * vertical;
            right.y = 0;
            forward.y = 0;

            pos = right + up + forward;
            pos *= moveSpeed;
            pos += transform.position;

            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
        }

        private void Rotation()
        {
            // 좌우로 움직인 마우스의 이동량 * 속도에 따라 카메라가 좌우로 회전할 양 계산
            float yRotateSize = Input.GetAxis("Mouse X") * rotateSpeed;
            // 현재 y축 회전값에 더한 새로운 회전각도 계산
            float yRotate = transform.eulerAngles.y + yRotateSize;

            // 위아래로 움직인 마우스의 이동량 * 속도에 따라 카메라가 회전할 양 계산(하늘, 바닥을 바라보는 동작)
            float xRotateSize = -Input.GetAxis("Mouse Y") * rotateSpeed;
            // 위아래 회전량을 더해주지만 -45도 ~ 80도로 제한 (-45:하늘방향, 80:바닥방향)
            // Clamp 는 값의 범위를 제한하는 함수
            xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

            // 카메라 회전량을 카메라에 반영(X, Y축만 회전)
            transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
        }

        private void ChangeMouseMode()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isMouseLock = !isMouseLock;
            }
        }


    }

}
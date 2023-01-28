using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utill.Measurement
{
    public class FreeCameraUtill : MonoBehaviour
    {
        [SerializeField, Header("F�� ������ ���콺 ���� ����")]
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
            // �¿�� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� �¿�� ȸ���� �� ���
            float yRotateSize = Input.GetAxis("Mouse X") * rotateSpeed;
            // ���� y�� ȸ������ ���� ���ο� ȸ������ ���
            float yRotate = transform.eulerAngles.y + yRotateSize;

            // ���Ʒ��� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� ȸ���� �� ���(�ϴ�, �ٴ��� �ٶ󺸴� ����)
            float xRotateSize = -Input.GetAxis("Mouse Y") * rotateSpeed;
            // ���Ʒ� ȸ������ ���������� -45�� ~ 80���� ���� (-45:�ϴù���, 80:�ٴڹ���)
            // Clamp �� ���� ������ �����ϴ� �Լ�
            xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

            // ī�޶� ȸ������ ī�޶� �ݿ�(X, Y�ุ ȸ��)
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
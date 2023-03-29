using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Cinemachine;
using ForTheTest;

namespace LockOn
{
    public class ZoomInCamera : MonoBehaviour
    {
        public GameObject zoomInCam;

        public LockOnCamera lockOnCamera;

        [SerializeField]
        private float moveDelay = 6;

        private float currentDelay;
        private AbMainModule mainModule;
        private Transform zoomInTarget;

        private bool my_coroutine_is_running = false;

        private Vector3 originPos;
        private Quaternion originRot;

        private ThirdPersonCameraController thirdPersonCameraController;
        private ThirdPersonCameraController lockOnCameraController;

        private void Start()
        {
            mainModule = GetComponent<AbMainModule>();
            originPos = zoomInCam.transform.localPosition;
            originRot = zoomInCam.transform.localRotation;

            thirdPersonCameraController = zoomInCam.GetComponent<ThirdPersonCameraController>();
            zoomInTarget = zoomInCam.transform.Find("ZoomPlayerInTarget");
        }

        public void Zoom(int _isOn)
        {
            //bool _on = _isOn > 0 ? true : false;
            CameraZoomDelay(_isOn);

            //currentDelay = moveDelay * _isOn;

            //if (_on)
            //    CameraZoomDelay(true);
            //else

            //                      if(_)
        }

        private void CameraZoomDelay(int _isOn)
        {
            int _weight = _isOn > 0 ? -10 : 10;
            bool _on = _isOn > 0 ? true : false;

            lockOnCamera.currentCamera.gameObject.SetActive(!_on);

            zoomInCam.SetActive(_on);

            mainModule.LockOn = _on;

            if (_on)
            {
                //zoomInCam.transform.position = originPos + transform.position;
                //zoomInCam.transform.rotation = originRot * transform.rotation;

                thirdPersonCameraController.cameraY = lockOnCamera.currentCamera.GetComponent<ThirdPersonCameraController>().cameraY;
                mainModule.LockOnTarget = zoomInTarget;
                //zoomInCam.transform.localPosition = new Vector3(0.6461196f, 2.805113f, -0.981263f);
                //zoomInCam.transform.localRotation = Quaternion.Euler(new Vector3(40, 0, 0));
            }
            else
            {
                mainModule.LockOnTarget = null;
                lockOnCameraController = lockOnCamera.currentCamera.GetComponent<ThirdPersonCameraController>();
                lockOnCameraController.cameraY = thirdPersonCameraController.cameraY;
            }
        }
    }
}
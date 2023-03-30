using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Cinemachine;
using ForTheTest;
using DG.Tweening;

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

        public CinemachineVirtualCamera cinemachineVirtualCamera;

        private void Start()
        {
            mainModule = GetComponent<AbMainModule>();
            originPos = zoomInCam.transform.localPosition;
            originRot = zoomInCam.transform.localRotation;

            thirdPersonCameraController = zoomInCam.GetComponent<ThirdPersonCameraController>();
            cinemachineVirtualCamera = zoomInCam.GetComponent<CinemachineVirtualCamera>();
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

                ThirdPersonCameraController _thirdPersonCameraController =
                    lockOnCamera.currentCamera.GetComponent<ThirdPersonCameraController>();

                if (_thirdPersonCameraController.isActiveAndEnabled)
                    thirdPersonCameraController.cameraY =
                        lockOnCamera.currentCamera.GetComponent<ThirdPersonCameraController>().cameraY;
                else
                {
                    cinemachineVirtualCamera.m_LookAt = lockOnCamera.currentCamera.LookAt;
                    thirdPersonCameraController.enabled = false;
                }

                mainModule.LockOnTarget = zoomInTarget;
                //zoomInCam.transform.localPosition = new Vector3(0.6461196f, 2.805113f, -0.981263f);
                //zoomInCam.transform.localRotation = Quaternion.Euler(new Vector3(40, 0, 0));
            }
            else
            {
                mainModule.LockOnTarget = null;
                cinemachineVirtualCamera.m_LookAt = null;
                thirdPersonCameraController.enabled = true;
                lockOnCameraController = lockOnCamera.currentCamera.GetComponent<ThirdPersonCameraController>();
                lockOnCameraController.cameraY = thirdPersonCameraController.cameraY;
            }
        }

        public void CameraZoomIn(float _duration, string _strengh)
        {
            CinemachineVirtualCamera _currentCamera = lockOnCamera.currentCamera;

            float _originSize = _currentCamera.m_Lens.FieldOfView;
            float _targetSize = float.Parse(_strengh) * _originSize;

            DOTween.To(
                () => _originSize, (x2) => _currentCamera.m_Lens.FieldOfView = x2, _targetSize, 0.3f
            ).SetLoops(1, LoopType.Yoyo);
        }
    }
}
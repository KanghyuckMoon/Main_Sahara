using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Cinemachine;
using ForTheTest;
using DG.Tweening;
using Data;

namespace LockOn
{
    public class ZoomInCamera : MonoBehaviour
    {
        [SerializeField] private ZoomInDataSO zoomInDataSO;
        
        public GameObject zoomInCam;
        public GameObject zoomInCam_Lock;

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

        private CinemachineComposer composer;
        //composer

        [SerializeField]
        private Transform originTarget = null;

        [SerializeField] 
        private bool isNotUse = false;

        private void Start()
        {
            if (isNotUse)
            {
                return;
            }
            mainModule = GetComponent<AbMainModule>();
            originPos = zoomInCam.transform.localPosition;
            originRot = zoomInCam.transform.localRotation;

            thirdPersonCameraController = zoomInCam.GetComponent<ThirdPersonCameraController>();
            cinemachineVirtualCamera = zoomInCam_Lock.GetComponent<CinemachineVirtualCamera>();
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
            bool _on = _isOn > 0;
            
            lockOnCamera.currentCamera.gameObject.SetActive(!_on);
            ThirdPersonCameraController _thirdPersonCameraController =
                lockOnCamera.currentCamera.GetComponent<ThirdPersonCameraController>();

            if (_thirdPersonCameraController.enabled)
                zoomInCam.SetActive(_on);
            else zoomInCam_Lock.SetActive(_on);
            
            if (_on)
            {
                if (_thirdPersonCameraController.enabled)
                {
                    thirdPersonCameraController.cameraY =
                        lockOnCamera.currentCamera.GetComponent<ThirdPersonCameraController>().cameraY;
                }
                else
                {
                    cinemachineVirtualCamera.m_LookAt = lockOnCamera.currentCamera.LookAt;
                    thirdPersonCameraController.enabled = false;
                }

                if (_thirdPersonCameraController.enabled)
                {
                    mainModule.LockOnTarget = zoomInTarget;
                    originTarget = null;
                }
                else
                {
                    originTarget = mainModule.LockOnTarget;
                    cinemachineVirtualCamera.m_LookAt = _thirdPersonCameraController.GetComponent<CinemachineVirtualCamera>().m_LookAt;
                    //CinemachineComposer composer = cinemachineVirtualCamera.gameObject.AddComponent<CinemachineComposer>();

                    //cinemachineVirtualCamera. = _thirdPersonCameraController.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>()
                    //composer = cinemachineVirtualCamera;
                }
            }
            else
            {
                mainModule.LockOnTarget = originTarget;
                //cinemachineVirtualCamera.m_LookAt = null;
                thirdPersonCameraController.enabled = true;
                lockOnCameraController = lockOnCamera.currentCamera.GetComponent<ThirdPersonCameraController>();
                //Debug.LogError("AKJAHEJAEKAAKAJGRAWKGWekjegjawkeje");
                if (_isOn >= 0)
                {
                    lockOnCameraController.cameraY = thirdPersonCameraController.cameraY;
                }
            }

            mainModule.LockOn = false;
        }

        public void CameraZoomIn(string _key)//, string _strengh)
        {
            if (isNotUse)
            {
                return;
            }
            //Debug.LogError("줌임줌인줌인");

            //lockOnCamera.currentCamera;
            
            //if()
            
            float _originSize = 32;
            float _targetSize = zoomInDataSO.ZoomInData[_key].value * _originSize;

            DOTween.To(
                () => _originSize, (x2) => lockOnCamera.currentCamera.m_Lens.FieldOfView = x2, _targetSize, zoomInDataSO.ZoomInData[_key].duration_Zoom
            ).OnComplete(() =>
            {

                DOTween.To(
                    () => _targetSize, (x2) => lockOnCamera.currentCamera.m_Lens.FieldOfView = x2, _originSize, zoomInDataSO.ZoomInData[_key].duration_Back
                );
            });
        }
    }
}
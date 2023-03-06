using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Cinemachine;

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

        private void Start()
        {
            mainModule = GetComponent<AbMainModule>();
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

            lockOnCamera.currentCamera.Priority = _weight;//.SetActive(!_isOn);

            //zoomInCam.SetActive(_on);

            mainModule.LockOn = _on;

            if (_on)
                mainModule.LockOnTarget = zoomInTarget;
            else
                mainModule.LockOnTarget = null;
        }
    }
}
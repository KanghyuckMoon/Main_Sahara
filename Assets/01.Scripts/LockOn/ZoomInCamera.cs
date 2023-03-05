using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;

namespace LockOn
{
    public class ZoomInCamera : MonoBehaviour
    {
        public GameObject zoomInCam;

        public LockOnCamera lockOnCamera;

        private AbMainModule mainModule;
        private Transform zoomInTarget;

        private void Start()
        {
            mainModule = GetComponent<AbMainModule>();
            zoomInTarget = transform.Find("ZoomInTarget");
        }

        public void Zoom(int _isOn)
        {
            bool _on = _isOn > 0 ? true : false;
            lockOnCamera?.currentCamera.SetActive(!_on);
            zoomInCam.SetActive(_on);

            mainModule.LockOn = _on;

            //Transform _transform = new Transform();

            if (_on)
                mainModule.LockOnTarget = zoomInTarget;
            else
                mainModule.LockOnTarget = null;//lockOnCamera.target;
        }
    }
}
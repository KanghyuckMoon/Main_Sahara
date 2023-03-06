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
            bool _on = _isOn > 0 ? true : false;


            currentDelay = moveDelay;
            currentDelay -= (currentDelay * _isOn);

            //StopCoroutine(CameraZoomDelay(true));

            StartCoroutine(CameraZoomDelay(_on));

            //Transform _transform = new Transform();
        }

        IEnumerator CameraZoomDelay(bool _isOn)
        {
            my_coroutine_is_running = true;
            yield return new WaitForSeconds(currentDelay);
            my_coroutine_is_running = false;


            lockOnCamera?.currentCamera.SetActive(!_isOn);
            zoomInCam.SetActive(_isOn);

            mainModule.LockOn = _isOn;

            if (_isOn)
                mainModule.LockOnTarget = zoomInTarget;
            else
                mainModule.LockOnTarget = null;//lockOnCamera.target;
        }
    }
}
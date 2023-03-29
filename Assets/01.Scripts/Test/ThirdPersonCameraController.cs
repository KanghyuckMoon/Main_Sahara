using Cinemachine;
using UnityEngine;
using DG.Tweening;

namespace ForTheTest
{

    public class ThirdPersonCameraController : MonoBehaviour
    {
        [Header("Camera Rotates around this")]
        public Transform invisibleCameraOrigin;
        [Header("Our 3rd person Vcam")]
        public CinemachineVirtualCamera vcam;                   // the main vcam that we're using

        [Header("Vertical Camera Extents")]
        public float verticalRotateMin = -80f;
        public float verticalRotateMax = 80f;

        [Header("Camera Movement Multiplier")]
        public float cameraVerticalRotationMultiplier = 2f;
        public float cameraHorizontalRotationMultiplier = 2f;

        [Header("Camera Input Values")]
        public float cameraInputHorizontal;
        public float cameraInputVertical;

        [Header("Invert Camera Controls")]
        public bool invertHorizontal = false;
        public bool invertVertical = false;

        [Header("Toggles which side the camera should start on. 1 = Right, 0 = Left")]
        public float cameraSide = 1f;

        [Header("Allow toggling left to right shoulder")]
        public bool allowCameraToggle = true;

        [Header("How fast we should transition from left to right")]
        public float cameraSideToggleSpeed = 1f;

        private Cinemachine3rdPersonFollow followCam;           // so we can manipulate the 'camera side' property dynamically

        // current camera rotation values
        [SerializeField]
        private float cameraX = 0f;
        [SerializeField]
        public float cameraY = 0f;

        // if we are switching sides
        private bool doCameraSideToggle = false;
        private float sideToggleTime = 0f;
        // where we are in the transition from side to side
        private float desiredCameraSide = 1f;

        private void Start()
        {
            if (vcam == null)
            {
                // try to grab the vcam from this object
                vcam = GetComponent<CinemachineVirtualCamera>();

                Transform _player = GameObject.Find("Player").transform.Find("Target");
                vcam.LookAt = _player;
                vcam.Follow = _player;
            }
            else
            {
                Debug.Log("Need to connect your 3rd person vcam to the CameraController!");
            }


        }

        public void ChangeCamera(float x, float y)
		{
            DOTween.To(() => cameraX, a => 
            { 
                cameraX = a;
                invisibleCameraOrigin.eulerAngles = new Vector3(-cameraX, -cameraY, 0.0f);
            }, x, 1f);
            DOTween.To(() => cameraY, a => 
            {
                cameraY = a;
                invisibleCameraOrigin.eulerAngles = new Vector3(-cameraX, -cameraY, 0.0f);
            }
            , y, 1f);
        }

        private void Update()
        {
            // make sure we have a handle to the follow component
            if (followCam == null)
            {
                followCam = vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            }

            cameraInputHorizontal = -Input.GetAxis("Mouse X");
            cameraInputVertical = Input.GetAxis("Mouse Y");
            
            if(allowCameraToggle)
            {
                var side = Input.GetButtonDown("CameraSide");
                if (side)
                {
                    doCameraSideToggle = true;
                }

                if (doCameraSideToggle)
                {
                    sideToggleTime = 0f;
                    //cameraSide = followCam.CameraSide;
                    if (cameraSide > 0.1)
                    {
                        desiredCameraSide = 0f;
                    }
                    else
                    {
                        desiredCameraSide = 1f;
                    }
                    doCameraSideToggle = false;
                }

                //followCam.CameraSide = Mathf.Lerp(cameraSide, desiredCameraSide, sideToggleTime);
                sideToggleTime += cameraSideToggleSpeed * Time.deltaTime;

            }

            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.blue, 1.0f);
            Debug.DrawRay(invisibleCameraOrigin.position, invisibleCameraOrigin.forward, Color.red, 1.0f);

            if (invisibleCameraOrigin != null)
            {
                if (invertHorizontal)
                {
                    cameraX -= cameraVerticalRotationMultiplier * cameraInputVertical;
                }
                else
                {
                    cameraX += cameraVerticalRotationMultiplier * cameraInputVertical;
                }

                if (invertVertical)
                {
                    cameraY -= cameraHorizontalRotationMultiplier * cameraInputHorizontal;
                }
                else
                {
                    cameraY += cameraHorizontalRotationMultiplier * cameraInputHorizontal;
                }
                cameraX = Mathf.Clamp(cameraX, verticalRotateMin, verticalRotateMax);
                invisibleCameraOrigin.eulerAngles = new Vector3(-cameraX, -cameraY, 0.0f);
            }
        }
    }
}
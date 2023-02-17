using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SingletonObject;
using Utill.Pattern;
using Cinemachine;
using Utill.Coroutine;

namespace Module
{
    public class CameraModule : AbBaseModule
    {
        public CinemachineVirtualCamera follawVCam;
        public CinemachineVirtualCamera groupVCam;

        public Camera MainCamera
		{
            get
            {
                camera ??= Camera.main;
                return camera;
			}
		}
        private Camera camera;

        private CinemachineBasicMultiChannelPerlin followCamNoise;
        private CinemachineBasicMultiChannelPerlin groupCamNoise;

        private float currentShakeDuration = 0;
        //private PlayerFollowCamera camInstance;
				 //public CinemachineVirtualCamera followVCam;

		//[Header("Camera Rotates around this")]
		//public Transform invisibleCameraOrigin;
		//[Header("Our 3rd person Vcam")]
		//public CinemachineVirtualCamera vcam;                   // the main vcam that we're using

		//[Header("Vertical Camera Extents")]
		//public float verticalRotateMin = -80f;
		//public float verticalRotateMax = 80f;

		//[Header("Camera Movement Multiplier")]
		//public float cameraVerticalRotationMultiplier = 2f;
		//public float cameraHorizontalRotationMultiplier = 2f;

		//[Header("Camera Input Values")]
		//public float cameraInputHorizontal;
		//public float cameraInputVertical;

		//[Header("Invert Camera Controls")]
		//public bool invertHorizontal = false;
		//public bool invertVertical = false;

		//[Header("Toggles which side the camera should start on. 1 = Right, 0 = Left")]
		//public float cameraSide = 1f;

		//[Header("Allow toggling left to right shoulder")]
		//public bool allowCameraToggle = true;

		//[Header("How fast we should transition from left to right")]
		//public float cameraSideToggleSpeed = 1f;      // so we can manipulate the 'camera side' property dynamically

		//// current camera rotation values
		//private float cameraX = 0f;
		//private float cameraY = 0f;

		//// if we are switching sides
		//private bool doCameraSideToggle = false;
		//private float sideToggleTime = 0f;
		//// where we are in the transition from side to side
		//private float desiredCameraSide = 1f;

		public CameraModule(AbMainModule _mainModule) : base(_mainModule)
        {

        }

        public override void Awake()
        {
            //camInstance = PlayerFollowCamera.Instance;
            follawVCam = GameObject.Find("PlayerCam").GetComponent<CinemachineVirtualCamera>();//camInstance.GetComponent<CinemachineVirtualCamera>();
            groupVCam = GameObject.Find("GroupCam").GetComponent<CinemachineVirtualCamera>();//camInstance.GetComponent<CinemachineVirtualCamera>();

            followCamNoise = follawVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            groupCamNoise = groupVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            groupVCam.gameObject.SetActive(false);
            //mainModule.objRotation = mainCam.transform.rotation;
        }

        public override void LateUpdate()
        {
            if (follawVCam.gameObject.activeSelf)
			{
                mainModule.ObjRotation = follawVCam.transform.rotation;
			}
            else
            {
                mainModule.ObjRotation = groupVCam.transform.rotation;
            }
            //float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;
            //float size = nomalCom.m_Lens.OrthographicSize;

            //if (distance != 0)
            //{
            //    size += distance;
            //    if (size <= 1 || size >= 6)
            //        size -= distance;
            //}

            //nomalCom.m_Lens.OrthographicSize = size;
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                ShakeCam(0.2f, 0.3f, 7);
            }
        }

        public void ShakeCam(float duration, float strengh, float shakeFrequent)
        {
            StaticCoroutineManager.Instance.InstanceDoCoroutine(ShakingCam(duration, strengh, shakeFrequent));
        }

        private IEnumerator ShakingCam(float duration, float strengh, float shakeFrequent)
        {
            followCamNoise.m_AmplitudeGain = groupCamNoise.m_AmplitudeGain = strengh;//.Priority.
            followCamNoise.m_FrequencyGain = groupCamNoise.m_FrequencyGain = shakeFrequent;
            yield return new WaitForSeconds(duration);
            followCamNoise.m_AmplitudeGain = followCamNoise.m_FrequencyGain = 0;//.Priority.
            groupCamNoise.m_AmplitudeGain = groupCamNoise.m_FrequencyGain = 0;//.Priority.
        }

        //public override void Awake()
        //{
        //    camInstance = PlayerFollowCamera.Instance;
        //    followVCam = camInstance.GetComponent<CinemachineVirtualCamera>();
        //    //mainCam = Camera.main;
        //    //mainModule.objRotation = mainCam.transform.rotation;
        //}

        //public override void FixedUpdate()
        //{
        //}

        //public override void Start()
        //{
        //}

        //public override void LateUpdate()
        //{
        //    //if (followVCam == null)
        //    //{
        //    //    followVCam = vcam.GetCinemachineComponent<CinemachineVirtualCamera>();
        //    //}

        //    //cameraInputHorizontal = -Input.GetAxis("Mouse X");
        //    //cameraInputVertical = Input.GetAxis("Mouse Y");

        //    cameraInputHorizontal = mainModule.objRotation.x;
        //    cameraInputVertical = mainModule.objRotation.y;

        //    if (allowCameraToggle)
        //    {
        //        var side = Input.GetButtonDown("CameraSide");
        //        if (side)
        //        {
        //            doCameraSideToggle = true;
        //        }

        //        if (doCameraSideToggle)
        //        {
        //            sideToggleTime = 0f;
        //            //cameraSide = followCam.CameraSide;
        //            if (cameraSide > 0.1)
        //            {
        //                desiredCameraSide = 0f;
        //            }
        //            else
        //            {
        //                desiredCameraSide = 1f;
        //            }
        //            doCameraSideToggle = false;
        //        }

        //        //followCam.CameraSide = Mathf.Lerp(cameraSide, desiredCameraSide, sideToggleTime);
        //        sideToggleTime += cameraSideToggleSpeed * Time.deltaTime;

        //    }

        //    Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.blue, 1.0f);
        //    Debug.DrawRay(invisibleCameraOrigin.position, invisibleCameraOrigin.forward, Color.red, 1.0f);

        //    if (invisibleCameraOrigin != null)
        //    {
        //        if (invertHorizontal)
        //        {
        //            cameraX -= cameraVerticalRotationMultiplier * cameraInputVertical;
        //        }
        //        else
        //        {
        //            cameraX += cameraVerticalRotationMultiplier * cameraInputVertical;
        //        }

        //        if (invertVertical)
        //        {
        //            cameraY -= cameraHorizontalRotationMultiplier * cameraInputHorizontal;
        //        }
        //        else
        //        {
        //            cameraY += cameraHorizontalRotationMultiplier * cameraInputHorizontal;
        //        }
        //        cameraX = Mathf.Clamp(cameraX, verticalRotateMin, verticalRotateMax);
        //        invisibleCameraOrigin.eulerAngles = new Vector3(-cameraX, -cameraY, 0.0f);
        //    }
        //}
    }
}
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
	    public GameObject CurrentCamera { get; private set; }

	    CinemachineVirtualCamera FollawVCam
        {
            get
            {
                follawVCam ??= GameObject.Find("PlayerCam").GetComponent<CinemachineVirtualCamera>();
                return follawVCam;
            }
        }
        CinemachineVirtualCamera GroupVCam
        {
            get
            {
                groupVCam ??= GameObject.Find("GroupCam").GetComponent<CinemachineVirtualCamera>();
                return groupVCam;
            }
        }
        CinemachineVirtualCamera ZoomVCam
        {
            get
            {
                zoomVCam ??= GameObject.Find("ZoomCam").GetComponent<CinemachineVirtualCamera>();
                return zoomVCam;
            }
        }
        

        public CinemachineVirtualCamera follawVCam;
        public CinemachineVirtualCamera groupVCam;
        public CinemachineVirtualCamera zoomVCam;
        public CinemachineVirtualCamera zoomVCam_Lock;

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
        private CinemachineBasicMultiChannelPerlin zoomCamNoise;

        private GameObject currentCamera;

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
        public CameraModule() : base()
        {

        }


        public override void Start()
        {
            //camInstance = PlayerFollowCamera.Instance;
            follawVCam = GameObject.Find("PlayerCam").GetComponent<CinemachineVirtualCamera>();//camInstance.GetComponent<CinemachineVirtualCamera>();
            groupVCam = GameObject.Find("GroupCam").GetComponent<CinemachineVirtualCamera>();//camInstance.GetComponent<CinemachineVirtualCamera>();
            zoomVCam = GameObject.Find("ZoomCam").GetComponent<CinemachineVirtualCamera>();//camInstance.GetComponent<CinemachineVirtualCamera>();
            zoomVCam_Lock = GameObject.Find("ZoomCam_Lock").GetComponent<CinemachineVirtualCamera>();//camInstance.GetComponent<CinemachineVirtualCamera>();

            followCamNoise = follawVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            groupCamNoise = groupVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            zoomCamNoise = zoomVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            CurrentCamera = follawVCam.gameObject;
            groupVCam.gameObject.SetActive(false);
            zoomVCam.gameObject.SetActive(false);
            zoomVCam_Lock.gameObject.SetActive(false);
            //mainModule.objRotation = mainCam.transform.rotation;
        }

        private Vector3 CamPos(Quaternion _quaternion)
        {
            Vector3 _pos = _quaternion * Vector3.forward;

            return _pos;
        }

        public override void LateUpdate()
        {
            if (FollawVCam.gameObject.activeSelf)
            {
                mainModule.ObjRotation = FollawVCam.transform.rotation;
                CurrentCamera = FollawVCam.gameObject;
                //return;
            }
            else if (zoomVCam.gameObject.activeSelf)
            {
                mainModule.ObjRotation = zoomVCam.transform.rotation;
                CurrentCamera = zoomVCam.gameObject;
                mainModule.ObjForword = CamPos(zoomVCam.transform.rotation);
            }
            else if (GroupVCam.gameObject.activeSelf)
            {
	            CurrentCamera = GroupVCam.gameObject;
                mainModule.ObjRotation = GroupVCam.transform.rotation;
            }else if (zoomVCam_Lock.gameObject.activeSelf)
            {
	            mainModule.ObjRotation = zoomVCam_Lock.transform.rotation;
	            CurrentCamera = zoomVCam_Lock.gameObject;
	            mainModule.ObjForword = CamPos(zoomVCam_Lock.transform.rotation);
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

        public override void OnDisable()
        {
            follawVCam = null;
            groupVCam = null;
            mainModule = null;
            zoomVCam = null;
            base.OnDisable();
            Pool.ClassPoolManager.Instance.RegisterObject<CameraModule>("CameraModule", this);
        }
        public override void OnDestroy()
        {
            follawVCam = null;
            groupVCam = null;
            zoomVCam = null;
            mainModule = null;
            base.OnDestroy();
            Pool.ClassPoolManager.Instance.RegisterObject<CameraModule>("CameraModule", this);
        }
    }
}
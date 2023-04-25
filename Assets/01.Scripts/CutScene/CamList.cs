using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace CutScene
{
	public enum CamType
	{ 
		PlayerCam,
		GroupCam,
		ZoomCam,
		CutSceneZoomCam,
		TargetCam1,
		TargetCam2,
		TrackCam,
		LookAtCam,
	}
	public class CamList : MonoBehaviour
	{
		[SerializeField]
		private Transform cameraForwardTarget;
		[SerializeField]
		private CinemachineDollyCart fowardDollyCart;

		[SerializeField]
		private List<CinemachineVirtualCamera> camList = new List<CinemachineVirtualCamera>();

		public CinemachineVirtualCamera GetCam(CamType _camType)
		{
			return camList[(int)_camType];
		}

		public Transform GetCameraForwardTarget()
		{
			return cameraForwardTarget;
		}
		public CinemachineDollyCart GetCameraDollyCart()
		{
			return fowardDollyCart;
		}
	}
}

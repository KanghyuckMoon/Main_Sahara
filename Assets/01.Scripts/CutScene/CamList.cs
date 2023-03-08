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
		CutScneeZoomCam,
		TargetCam1,
		TargetCam2,
		TrackCam,
	}
	public class CamList : MonoBehaviour
	{
		[SerializeField]
		private List<CinemachineVirtualCamera> camList = new List<CinemachineVirtualCamera>();

		public CinemachineVirtualCamera GetCam(CamType _camType)
		{
			return camList[(int)_camType];
		}
	}
}

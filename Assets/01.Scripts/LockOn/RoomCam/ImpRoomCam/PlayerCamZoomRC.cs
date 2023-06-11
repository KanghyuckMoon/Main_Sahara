using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Utill.Pattern;

namespace LockOn
{
    public class PlayerCamZoomRC : BaseRoomCam
    {
        [SerializeField] private CinemachineVirtualCamera playerCam;
        [SerializeField] private float zoomDistance = 2f;
        private float originDistance = 0f;

        public override void Start()
        {
            base.Start();
            var _transpoerser = playerCam.GetCinemachineComponent<CinemachineFramingTransposer>();
            originDistance = _transpoerser.m_CameraDistance;
        }

        public override void SetRoomCam(object _value)
        {
            
        }

        protected override void SetInRoomMethod()
        {
            var _transpoerser = playerCam.GetCinemachineComponent<CinemachineFramingTransposer>();
            _transpoerser.m_CameraDistance = zoomDistance;
        }

        protected override void SetOutRoomMethod()
        {
            var _transpoerser = playerCam.GetCinemachineComponent<CinemachineFramingTransposer>();
            _transpoerser.m_CameraDistance = originDistance;
        }
    }

}
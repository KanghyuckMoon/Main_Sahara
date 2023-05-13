using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Utill.Pattern;

namespace LockOn
{
    public class RoomCam : MonoBehaviour
    {
        private bool isInRoom; 
        
        [SerializeField] private CinemachineVirtualCamera playerCam;
        [SerializeField] private float zoomDistance = 2f;
        private float originDistance = 0f;

        public void Start()
        {
            var _transpoerser = playerCam.GetCinemachineComponent<CinemachineFramingTransposer>();
            originDistance = _transpoerser.m_CameraDistance;
        }

        public void SetInRoom()
        {
            if (!isInRoom)
            {
                isInRoom = true;
                var _transpoerser = playerCam.GetCinemachineComponent<CinemachineFramingTransposer>();
                _transpoerser.m_CameraDistance = zoomDistance;
            }
        }

        public void SetOutRoom()
        {
            if (isInRoom)
            {
                isInRoom = false;
                var _transpoerser = playerCam.GetCinemachineComponent<CinemachineFramingTransposer>();
                _transpoerser.m_CameraDistance = originDistance;
            }
        }
    }   
}

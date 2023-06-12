using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LockOn
{
    public class RoomCamEvent : MonoBehaviour
    {
        private RoomCamGroup RoomCamGroup
        {
            get
            {
                roomCamGroup ??= FindObjectOfType<RoomCamGroup>();
                return roomCamGroup;
            }
        }

        [SerializeField] 
        private RoomCamType roomCamType;

        [SerializeField, Header("TopViewCamOnly")]
        private Quaternion objRotation;

        [SerializeField, Header("TopViewCamOnly")]
        private Vector3 camRotate;
        
        private RoomCamGroup roomCamGroup;

        [SerializeField] 
        private UnityEvent inEvent; 
        [SerializeField] 
        private UnityEvent outEvent; 

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var _cam = RoomCamGroup.GetRoomCam(roomCamType);
                switch (roomCamType)
                {
                    case RoomCamType.PlayerCamZoom:
                        _cam.SetRoomCam(null);
                        break;
                    case RoomCamType.TopView:
                        _cam.SetRoomCam(new Tuple<Quaternion, Vector3>(objRotation, camRotate) );
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                inEvent?.Invoke();
                _cam.SetInRoom();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var _cam = RoomCamGroup.GetRoomCam(roomCamType);
                outEvent?.Invoke();
                _cam.SetOutRoom();
            }
        }
    } 
}

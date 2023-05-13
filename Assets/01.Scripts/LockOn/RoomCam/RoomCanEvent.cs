using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LockOn
{
    public class RoomCanEvent : MonoBehaviour
    {
        private RoomCam RoomCam
        {
            get
            {
                roomCam ??= FindObjectOfType<RoomCam>();
                return roomCam;
            }
        }

        private RoomCam roomCam;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                RoomCam.SetInRoom();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                RoomCam.SetOutRoom();
            }
        }
    }
}

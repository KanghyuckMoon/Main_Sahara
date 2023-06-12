using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LockOn
{
    public enum RoomCamType
    {
        PlayerCamZoom = 0,
        TopView
    }
    
    public class RoomCamGroup : MonoBehaviour
    {
        [SerializeField]
        private BaseRoomCam[] roomCams= new BaseRoomCam[2];
        
        public BaseRoomCam GetRoomCam(RoomCamType roomCamType)
        {
            return roomCams[(int)roomCamType];
        }
        
    }   
}

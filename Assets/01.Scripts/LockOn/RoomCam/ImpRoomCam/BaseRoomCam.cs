using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Utill.Pattern;

namespace LockOn
{
    public abstract class BaseRoomCam : MonoBehaviour
    {
        protected bool isInRoom; 
        

        public virtual void Start()
        {
        }


        public void SetInRoom()
        {
            if (!isInRoom)
            {
                isInRoom = true;
                SetInRoomMethod();
            }
        }

        public void SetOutRoom()
        {
            if (isInRoom)
            {
                isInRoom = false;
                SetOutRoomMethod();
            }
        }

        public abstract void SetRoomCam(object _value);
        protected abstract void SetInRoomMethod();
        protected abstract void SetOutRoomMethod();
    }   
}

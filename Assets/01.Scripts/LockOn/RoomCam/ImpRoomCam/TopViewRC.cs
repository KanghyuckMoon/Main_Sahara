using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Module;
using Utill.Pattern;

namespace LockOn
{
    public class TopViewRC : BaseRoomCam
    {
        [SerializeField] private CinemachineVirtualCamera playerCam;
        [SerializeField] private CinemachineVirtualCamera topViewCam;

        
        public override void SetRoomCam(object _value)
        {
            Quaternion _objRotation = ((Tuple<Quaternion, Vector3>)_value).Item1;
            Vector3 _rotate = ((Tuple<Quaternion, Vector3>)_value).Item2;
            PlayerObj.Player.GetComponent<AbMainModule>().ObjRotation = _objRotation;
            playerCam.transform.eulerAngles = _rotate;
            Camera.main.transform.eulerAngles = _rotate;
            topViewCam.transform.eulerAngles = _rotate;
        }

        protected override void SetInRoomMethod()
        {
            playerCam.gameObject.SetActive(false);
            topViewCam.gameObject.SetActive(true);
        }

        protected override void SetOutRoomMethod()
        {
            playerCam.gameObject.SetActive(true);
            topViewCam.gameObject.SetActive(false);
        }
    }   
}

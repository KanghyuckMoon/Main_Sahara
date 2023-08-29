using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class RememberPosition : MonoBehaviour
    {
        [SerializeField] private Vector3 pos;
        [SerializeField] private Quaternion rot;
        [SerializeField] private bool isGlobal;

        public Vector3 Pos => pos;
        public Quaternion Rot => rot;

        [ContextMenu("포지션 저장")]
        public void SavePos()
        {
            pos = isGlobal ? transform.position : transform.localPosition;
            rot = isGlobal ? transform.rotation : transform.localRotation;
        }

        public void SetPos()
        {
            transform.localPosition = Pos;
            transform.localRotation = Rot;
        }
    }
}
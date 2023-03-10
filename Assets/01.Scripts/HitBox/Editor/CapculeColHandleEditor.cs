#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HitBox
{
    [CustomEditor(typeof(CapsuleColEditor))]
    public class CapculeColHandleEditor : Editor
    {
        
        protected virtual void OnSceneGUI()
        {
            CapsuleColEditor _capsuleColEditor = (CapsuleColEditor)target;
            CapsuleCollider _capsuleCollider = _capsuleColEditor.GetComponent<CapsuleCollider>();
            Transform transform = _capsuleColEditor.transform;
            HitBoxData _hitBoxData = _capsuleColEditor.hitBoxData;
            Vector3 _pos = transform.position + (transform.forward * _capsuleCollider.center.z) + (transform.up * _capsuleCollider.center.y) + (transform.right * _capsuleCollider.center.x);

            Handles.color = Color.yellow;
            Handles.ArrowHandleCap(
                0,
                _pos,
                transform.rotation * Quaternion.Euler(_capsuleColEditor.hitBoxData.knockbackDir),
                _capsuleColEditor.hitBoxData.defaultPower,
                EventType.Repaint
            );
        }
    }

}
#endif
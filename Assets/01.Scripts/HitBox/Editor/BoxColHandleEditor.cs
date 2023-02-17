#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HitBox
{
    [CustomEditor(typeof(BoxColEditor))]
    public class BoxColHandleEditor : Editor
    {
        
        protected virtual void OnSceneGUI()
        {
            if (Event.current.type == EventType.Repaint)
            {
                BoxColEditor _boxColEditor = (BoxColEditor)target;
                Transform transform = _boxColEditor.transform;
                Handles.color = Color.yellow;
                Handles.ArrowHandleCap(
                    0,
                    transform.position,
                    transform.rotation * Quaternion.Euler(_boxColEditor.hitBoxData.knockbackDir),
                    _boxColEditor.hitBoxData.defaultPower,
                    EventType.Repaint
                );
            }
        }
    }

}
#endif
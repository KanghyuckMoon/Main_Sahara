#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HitBox
{
    [CustomEditor(typeof(InGameHitBox))]
    public class HitBoxHandleEditor : Editor
    {
        
        protected virtual void OnSceneGUI()
        {
            if (Event.current.type == EventType.Repaint)
            {
                InGameHitBox _inGameHitBox = (InGameHitBox)target;
                Transform transform = _inGameHitBox.transform;
                Handles.color = Color.yellow;
                Handles.ArrowHandleCap(
                    0,
                    _inGameHitBox.HitBoxPos,
                    _inGameHitBox.KnockbackDir(),
                    _inGameHitBox.KnockbackPower(),
                    EventType.Repaint
                );
            }
        }
    }

}
#endif
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = System.Object;
using static wAnimationWindowHelper;

public static class AnimationHitBoxEditor
{

    public static int GetAnimationWindowCurrentFrame()
    {
        UnityEngine.Object w = wAnimationWindowHelper.GetOpenAnimationWindow();
        if (w != null)
        {
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            FieldInfo animEditor = GetAnimationWindowType().GetField("m_AnimEditor", flags);

            Type animEditorType = animEditor.FieldType;
            System.Object animEditorObject = animEditor.GetValue(w);
            FieldInfo animWindowState = animEditorType.GetField("m_State", flags);
            Type windowStateType = animWindowState.FieldType;

            return (int)windowStateType.GetProperty("currentFrame").GetValue(animWindowState.GetValue(animEditorObject));
        }

        return 0;
    }

    public static float GetAnimationWindowTime()
    {
        UnityEngine.Object w = wAnimationWindowHelper.GetOpenAnimationWindow();
        if (w != null)
        {
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            FieldInfo animEditor = GetAnimationWindowType().GetField("m_AnimEditor", flags);

            Type animEditorType = animEditor.FieldType;
            System.Object animEditorObject = animEditor.GetValue(w);
            FieldInfo animWindowState = animEditorType.GetField("m_State", flags);
            Type windowStateType = animWindowState.FieldType;

            return (float)windowStateType.GetProperty("currentTime").GetValue(animWindowState.GetValue(animEditorObject));
        }

        return 0;
    }
    public static AnimationClip GetAnimationWindowAnimationClip()
    {
        UnityEngine.Object w = wAnimationWindowHelper.GetOpenAnimationWindow();
        if (w != null)
        {
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            FieldInfo animEditor = GetAnimationWindowType().GetField("m_AnimEditor", flags);

            Type animEditorType = animEditor.FieldType;
            System.Object animEditorObject = animEditor.GetValue(w);
            FieldInfo animWindowState = animEditorType.GetField("m_State", flags);
            Type windowStateType = animWindowState.FieldType;

            return (AnimationClip)windowStateType.GetProperty("activeAnimationClip").GetValue(animWindowState.GetValue(animEditorObject));
        }

        return null;
    }
}

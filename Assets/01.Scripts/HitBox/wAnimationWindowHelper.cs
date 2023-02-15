#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

public static class wAnimationWindowHelper
{
    [MenuItem("AnimationTool/PrintActiveClip")]
    public static void GetActiveClip()
    {
        GetAnimationWindowCurrentClip();
    }


    public static System.Type animationWindowType = null;

    public static System.Type GetAnimationWindowType()
    {
        if (animationWindowType == null)
        {
            animationWindowType = System.Type.GetType("UnityEditor.AnimationWindow,UnityEditor");
        }

        return animationWindowType;
    }

    public static UnityEngine.Object GetOpenAnimationWindow()
    {
        UnityEngine.Object[] openAnimationWindows = Resources.FindObjectsOfTypeAll(GetAnimationWindowType());
        if (openAnimationWindows.Length > 0)
        {
            return openAnimationWindows[0];
        }

        return null;
    }


    public static AnimationClip GetAnimationWindowCurrentClip()
    {
        UnityEngine.Object w = GetOpenAnimationWindow();
        if (w != null)
        {
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            FieldInfo animEditor = GetAnimationWindowType().GetField("m_AnimEditor", flags);

            Type animEditorType = animEditor.FieldType;
            System.Object animEditorObject = animEditor.GetValue(w);
            FieldInfo animWindowState = animEditorType.GetField("m_State", flags);
            Type windowStateType = animWindowState.FieldType;

            System.Object clip = windowStateType.InvokeMember("get_activeAnimationClip",
                BindingFlags.InvokeMethod | BindingFlags.Public, null, animWindowState.GetValue(animEditorObject),
                null);

            Debug.Log(clip);


            return (AnimationClip)clip;
        }

        return null;
    }
}
#endif
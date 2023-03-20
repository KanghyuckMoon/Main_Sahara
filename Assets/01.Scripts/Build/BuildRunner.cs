#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class BuildRunner : MonoBehaviour
{
    private const string APP_NAME = "MAIN_SAHARA";
    private const string BUILD_BASIC_PATH = "../../build/";
    private const string BUILD_WINDOW_PATH = BUILD_BASIC_PATH + "Window/";
    
    [MenuItem("Build/BuildWindowIL2CPP")]
    public static void BuildWindowIL2CPP()
    {
        string fileName = SetPlayerSettingsForAndroid();
        AddressableAssetSettings.BuildPlayerContent();

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = GetBuildSceneList();
        buildPlayerOptions.locationPathName =  BUILD_WINDOW_PATH + fileName;
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.None;
        
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.IL2CPP);
        
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
    
    
    
    protected static string SetPlayerSettingsForAndroid()
    {
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;

        string fileName = string.Format("{0}_{1}.apk", APP_NAME, PlayerSettings.bundleVersion);
        return fileName;
    }
    protected static string[] GetBuildSceneList()
    {
        EditorBuildSettingsScene[] scenes = UnityEditor.EditorBuildSettings.scenes;

        List<string> listScenePath = new List<string>();

        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i].enabled)
                listScenePath.Add(scenes[i].path);
        }

        return listScenePath.ToArray();
    }

}
#endif

#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CopyModelToInvenModel : EditorWindow
{
    static CopyModelToInvenModel exampleWindow;

    private List<GameObject> objectsToCopy = new List<GameObject>();
    private string targetFolderPath = "Assets/TargetFolder/";
    
    [MenuItem("PillTool/CopyModelToInvenModel")]
    static void Open()
    {
        if (exampleWindow == null)
        {
            exampleWindow = CreateInstance<CopyModelToInvenModel>();
        }

        exampleWindow.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Object Copier", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("Select objects to copy:", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;

        for (int i = 0; i < objectsToCopy.Count; i++)
        {
            objectsToCopy[i] = EditorGUILayout.ObjectField("Object " + i, objectsToCopy[i], typeof(GameObject), true) as GameObject;
        }

        if (GUILayout.Button("Add Selected Objects"))
        {
            GameObject[] selectedObjects = Selection.gameObjects;
            objectsToCopy.AddRange(selectedObjects);
        }

        EditorGUI.indentLevel--;
        targetFolderPath = EditorGUILayout.TextField("Target Folder Path", targetFolderPath);

        if (GUILayout.Button("Copy Objects"))
        {
            if (objectsToCopy.Count > 0)
            {
                CopyObjectsToFolder();
            }
            else
            {
                Debug.LogError("Please select objects to copy.");
            }
        }
    }
    
    private void CopyObjectsToFolder()
    {
        foreach (GameObject obj in objectsToCopy)
        {
            if (obj != null)
            {
                string objectPath = AssetDatabase.GetAssetPath(obj);
                string prefabName = obj.name.Replace("_Model", "_InvenModel");

                string targetPath = targetFolderPath + prefabName  + ".prefab";

                AssetDatabase.CopyAsset(objectPath, targetPath);
                AssetDatabase.SaveAssets();
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("Objects copied to: " + targetFolderPath);
    }
}
#endif
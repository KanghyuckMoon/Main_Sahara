#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

public class TestEditor : EditorWindow
{
    private GameObject prefabToCopy;
    private GameObject parent;
    static TestEditor window; 
    [MenuItem("PillTool/TestEditor")]
    static void ShowWindow()
    {
        window ??= CreateInstance<TestEditor>();
        if (window != null)
        {
            window.Show();
        }
    }

    private void OnGUI()
    {
        prefabToCopy = EditorGUILayout.ObjectField("Prefab to Copy:", prefabToCopy, typeof(GameObject), false) as GameObject;
        parent = EditorGUILayout.ObjectField("Parent:", parent, typeof(GameObject), false) as GameObject;

        if (GUILayout.Button("Copy and Save Prefab"))
        {
            CopyAndSavePrefab();
        }
    }

    private void CopyAndSavePrefab()
    {
        if (prefabToCopy == null)
        {
            Debug.LogError("Prefab to copy is not selected.");
            return;
        }

        GameObject _newParent = new GameObject();
        string _parentPath = AssetDatabase.GetAssetPath(prefabToCopy).Replace(prefabToCopy.name + ".prefab", "newParent" + "_copy.prefab");
        GameObject _copy2 = GameObject.Instantiate(prefabToCopy); 
        _copy2.transform.SetParent(_newParent.transform);

        GameObject _newParentPrefab = PrefabUtility.SaveAsPrefabAsset(_newParent, _parentPath);
        DestroyImmediate(_newParent);
        ///AssetDatabase.Refresh();

        //GameObject _copy2 = GameObject.Instantiate(prefabToCopy); 
       // GameObject _copy  =PrefabUtility.InstantiatePrefab(prefabToCopy/*,_newParentPrefab.transform*/) as GameObject;
       //_copy2.transform.SetParent(_newParentPrefab.transform);
        
        string _folderPath = AssetDatabase.GetAssetPath(prefabToCopy).Replace(prefabToCopy.name + ".prefab", prefabToCopy.name + "_copy.prefab");
        Debug.Log(_folderPath); 
        
        PrefabUtility.SaveAsPrefabAsset(_newParentPrefab, _folderPath);
        
        //DestroyImmediate(_copy);

        AssetDatabase.Refresh();

    }
}
#endif
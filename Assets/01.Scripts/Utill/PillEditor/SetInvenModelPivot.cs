#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 
public class SetInvenModelPivot : EditorWindow
{
    [MenuItem("PillTool/SetInvenModelPivot")]
    static void Open()
    {
        SetInvenModelPivot window = GetWindow<SetInvenModelPivot>();
        window.titleContent = new GUIContent("Process Prefabs");
        window.Show();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Process Selected Prefabs"))
        {
            ProcessSelectedPrefabs();
        }
    }

    private void ProcessSelectedPrefabs()
    {
        GameObject[] selectedGameObjects = Selection.gameObjects;

        if (selectedGameObjects == null || selectedGameObjects.Length == 0)
        {
            Debug.LogWarning("No prefabs selected.");
            return;
        }

        foreach (GameObject prefab in selectedGameObjects)
        {
            ProcessPrefab(prefab);
        }

        Debug.Log("Processing completed for selected prefabs.");
    }

    private void ProcessPrefab(GameObject prefab)
    {
        if (prefab == null)
            return;

        GameObject prefabInstance = GameObject.Instantiate(prefab); 
        prefabInstance.name = "Model";

        float middleY = Mathf.Abs(prefabInstance.transform.GetComponent<MeshRenderer>().bounds.center.y);
        Debug.Log(prefabInstance.transform.GetComponent<MeshRenderer>().bounds.size);
        // Update the y position of the prefab
        Vector3 newPosition = prefabInstance .transform.position;
        newPosition.y += middleY;
        prefabInstance .transform.position = newPosition;

        // Create a parent object
        GameObject parentObject = new GameObject(prefab.name);
        parentObject.name = prefab.name; 
        
        // Set the parent object's y position to middleY
        Vector3 parentPosition = parentObject.transform.position;
        parentPosition.y = -middleY;
        parentObject.transform.position = parentPosition;

        prefabInstance.transform.SetParent(parentObject.transform);
    

        string prefabPath = AssetDatabase.GetAssetPath(prefab).Replace(prefab.name+".prefab", "ModifyPivot/");
        string parentFolderPath = prefabPath + parentObject.name + ".prefab";
        string modelFolderPath = prefabPath + prefab.name + ".prefab";

        //GameObject _prefab = PrefabUtility.SaveAsPrefabAsset(parentObject, folderPath);
        // Make the current object a child of the parent object
        //prefabInstance .transform.SetParent(_prefab.transform);


        GameObject _newPrefab = PrefabUtility.SaveAsPrefabAsset(parentObject , modelFolderPath);
        //string _folderPath = AssetDatabase.GetAssetPath(prefab).Replace(prefab.name + ".prefab", prefab.name + "_copy.prefab");
        DestroyImmediate(parentObject);
        //Debug.Log(_folderPath);
        //PrefabUtility.SaveAsPrefabAsset(_newPrefab, _folderPath);
        AssetDatabase.SaveAssets();
        // Save changes to the prefab
        AssetDatabase.Refresh();

    }
}
#endif

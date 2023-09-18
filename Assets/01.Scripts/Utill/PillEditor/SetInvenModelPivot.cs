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
        prefabInstance.name = prefab.name;

        #region  Mesh체크 
        // Calculate the middleY
        MeshFilter meshFilter = prefabInstance.GetComponent<MeshFilter>();

        if (meshFilter == null)
        {
            Debug.LogWarning("Prefab doesn't have a MeshFilter component: " + prefab.name);
            return;
        }

        Mesh mesh = meshFilter.sharedMesh;

        if (mesh == null)
        {
            Debug.LogWarning("Mesh not found for prefab: " + prefab.name);
            return;
        }

        Vector3[] vertices = mesh.vertices;
        float highestY = float.MinValue;
        float lowestY = float.MaxValue; 
        
        foreach (Vector3 vertex in vertices)
        {
            highestY = Mathf.Max(highestY, vertex.y);
            lowestY = Mathf.Min(lowestY, vertex.y); 
        }

        //float middleY = -highestY;
       // float middleY = (highestY - lowestY) /2;

        float middleY = Mathf.Abs(prefabInstance.transform.GetComponent<MeshRenderer>().bounds.center.y); 
        Debug.Log(middleY);
        Debug.Log((highestY - lowestY) /2);
        #endregion
     

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
    

        string prefabPath = AssetDatabase.GetAssetPath(prefab).Replace(prefab.name+".prefab", "");
        string parentFolderPath = prefabPath + parentObject.name + ".prefab";
        string modelFolderPath = prefabPath + prefabInstance.name +"_copy" + ".prefab";

        //GameObject _prefab = PrefabUtility.SaveAsPrefabAsset(parentObject, folderPath);
        // Make the current object a child of the parent object
        //prefabInstance .transform.SetParent(_prefab.transform);


        GameObject _newPrefab = PrefabUtility.SaveAsPrefabAsset(parentObject , modelFolderPath);
        string _folderPath = AssetDatabase.GetAssetPath(prefab).Replace(prefab.name + ".prefab", prefab.name + "_copy.prefab");
        DestroyImmediate(parentObject);
        Debug.Log(_folderPath);
        PrefabUtility.SaveAsPrefabAsset(_newPrefab, _folderPath);
        AssetDatabase.SaveAssets();
        // Save changes to the prefab
        AssetDatabase.Refresh();

    }
}
#endif

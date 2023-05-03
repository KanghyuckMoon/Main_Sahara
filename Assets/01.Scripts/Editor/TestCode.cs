using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestCode : MonoBehaviour
{
    [MenuItem("TempTool/MeshMove")]
    public static void MeshMove()
    {
        foreach (var obj in Selection.gameObjects)
        {
            MeshFilter _meshFilter;
            MeshRenderer _meshRenderer;
            if (obj.TryGetComponent<MeshFilter>(out _meshFilter) && obj.TryGetComponent<MeshRenderer>(out _meshRenderer))
            {
                GameObject newObj = new GameObject();
                newObj.transform.SetParent(obj.transform);
                newObj.transform.localPosition = Vector3.zero;
                newObj.transform.localEulerAngles = Vector3.zero;
                newObj.transform.localScale = Vector3.one;
                newObj.name = obj.name.Replace("_Drop", "");
                var _newMeshFilter = newObj.AddComponent<MeshFilter>();
                _newMeshFilter.sharedMesh = _meshFilter.sharedMesh;
                
                var _newMeshRenderer = newObj.AddComponent<MeshRenderer>();
                _newMeshRenderer.sharedMaterials = _meshRenderer.sharedMaterials;
                
                DestroyImmediate(_meshFilter);
                DestroyImmediate(_meshRenderer);
            }
            else
            {
                Debug.Log("미포함", obj);
                continue;
            }
        }
    }
    
    
    [MenuItem("TempTool/NameRoot")]
    public static void NameRoot()
    {
        foreach (var obj in Selection.gameObjects)
        {
            obj.name = obj.transform.root.name.Replace("_DROP", "");
            PrefabUtility.UnpackPrefabInstance(obj, PrefabUnpackMode.Completely, InteractionMode.UserAction);
        }
    }
    
    
    [MenuItem("TempTool/Name_Model")]
    public static void Name_Model()
    {
        foreach (var obj in Selection.objects)
        {
            var path = AssetDatabase.GetAssetPath(obj.GetInstanceID());
            AssetDatabase.RenameAsset(path, $"{obj.name}_Model");
        }
    }
    
    [MenuItem("TempTool/Name_Icon")]
    public static void Name_Icon()
    {
        foreach (var obj in Selection.objects)
        {
            var path = AssetDatabase.GetAssetPath(obj.GetInstanceID());
            AssetDatabase.RenameAsset(path, $"{obj.name.Replace("_Sprite", "_Icon")}");
        }
    }
    
    [MenuItem("TempTool/Name_DROPto_Drop")]
    public static void Name_DROPto_Drop()
    {
        foreach (var obj in Selection.objects)
        {
            var path = AssetDatabase.GetAssetPath(obj.GetInstanceID());
            AssetDatabase.RenameAsset(path, $"{obj.name.Replace("_DROP", "_Drop")}");
        }
    }
}
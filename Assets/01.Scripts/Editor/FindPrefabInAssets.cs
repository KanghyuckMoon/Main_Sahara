using UnityEditor;
using UnityEngine;

public class FindPrefabInAssets : EditorWindow
{
    private GameObject selectedObject;
    private string searchFolder = "Assets/09.Prefabs/InvenModel"; // 원하는 폴더 경로로 설정

    [MenuItem("Custom Tools/Find Prefab In Assets")]
    public static void ShowWindow()
    {
        GetWindow<FindPrefabInAssets>("Find Prefab");
    }

    private void OnGUI()    
    {
        GUILayout.Label("Selected Object:");    
        selectedObject = (GameObject)EditorGUILayout.ObjectField(selectedObject, typeof(GameObject), true);

        if (GUILayout.Button("Find Prefab"))
        {
            if (selectedObject != null)
            {
                string objectName = selectedObject.name;
                string[] prefabPaths = AssetDatabase.FindAssets("t:Prefab",new []{searchFolder});

                foreach (string prefabPath in prefabPaths)
                {
                    string path = AssetDatabase.GUIDToAssetPath(prefabPath);
                    GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

                    if (prefab != null && prefab.name == objectName)
                    {
                        Selection.activeObject = prefab;
                        EditorGUIUtility.PingObject(prefab);
                        Debug.Log("Prefab found: " + prefab.name);
                        break;
                    }
                }
            }
            else
            {
                Debug.LogError("Please select an object in the Inspector.");
            }
        }
    }
}

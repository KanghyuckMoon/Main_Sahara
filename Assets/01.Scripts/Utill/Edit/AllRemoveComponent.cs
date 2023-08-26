#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AllRemoveComponent : EditorWindow
{
	static AllRemoveComponent exampleWindow;

	[MenuItem("MoonTool/AllRemoveComponent")]
	static void Open()
	{
		if (exampleWindow == null)
		{
			exampleWindow = CreateInstance<AllRemoveComponent>();
		}

		exampleWindow.Show();
	}

	void OnGUI ()
    {
        GameObject gameObject = (GameObject)Selection.activeObject ;

        if (GUILayout.Button("Delete All Components"))
        {
            // Get all components attached to the GameObject
            Component[] components = gameObject.GetComponents<Component>();

            // Iterate through all components and destroy them
            foreach (Component component in components)
            {
                // Make sure not to delete Transform component
                if (!(component is Transform))
                {
                    DestroyImmediate(component);
                }
            }

            // Remove any null entries in the components array
            gameObject.GetComponents<Component>();

            // Notify the user that components have been deleted
            Debug.Log("Deleted all components on GameObject: " + gameObject.name);
        }
	}
}
#endif

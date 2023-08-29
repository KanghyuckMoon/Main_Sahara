using UnityEngine;
using UnityEditor;

public class ScreenshotEditorWindow : EditorWindow
{
    [MenuItem("Custom/Screenshot Window")]
    public static void ShowWindow()
    {
        GetWindow<ScreenshotEditorWindow>("Screenshot Window");
    }

    private void OnGUI()
    {
        GUILayout.Label("Capture and Save Screenshot", EditorStyles.boldLabel);

        if (GUILayout.Button("Capture Screenshot"))
        {
            CaptureAndSaveScreenshot();
        }
    }

    private void CaptureAndSaveScreenshot()
    {
        string fileName = "screenshot_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        string path = Application.dataPath + "/ScreenShots/"  + fileName;

        ScreenCapture.CaptureScreenshot(path);
        Debug.Log("Screenshot captured and saved at: " + path);
    }
}

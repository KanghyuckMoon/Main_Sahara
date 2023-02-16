using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class TestUIElements : EditorWindow
{
    private Texture2D m_Texture = null;

    [MenuItem("UIElement/Test")]
    public static void ShowWindow()
    {
        TestUIElements window = GetWindow<TestUIElements>();
        window.autoRepaintOnSceneChange = true;
        window.Show();
    }

    void OnEnable()
    {
        m_Texture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/unitylogo_small.png");
        Debug.Log(m_Texture);

        var box = new Box()
        {
            style =
            {
                flexGrow = 1,
                marginTop = 5,
                marginBottom = 5,
                marginLeft = 5,
                marginRight = 5,
            }
        };
        rootVisualElement.Add(box);

        var meshContainer = new VisualElement() { style = { flexGrow = 1, } };
        box.Add(meshContainer);
        meshContainer.generateVisualContent += MyGenVisualContent;
    }

    void MyGenVisualContent(MeshGenerationContext mgc)
    {
        Debug.Log(m_Texture);
        var w = mgc.visualElement.worldBound.width;
        var h = mgc.visualElement.worldBound.height;

        float pad = 30;
        var mwd = mgc.Allocate(3, 3, m_Texture);
        var uvRegion = mwd.uvRegion;
        mwd.SetNextVertex(new Vertex()
        {
            position = new Vector3(w, -pad, Vertex.nearZ),
            tint = Color.red,
            uv = new Vector2(1, 0) * uvRegion.size + uvRegion.min
        });
        mwd.SetNextVertex(new Vertex()
        {
            position = new Vector3(-pad, -pad, Vertex.nearZ),
            tint = Color.green,
            uv = new Vector2(0, 0) * uvRegion.size + uvRegion.min
        });
        mwd.SetNextVertex(new Vertex()
        {
            position = new Vector3(w * 0.5f, h + pad, Vertex.nearZ),
            tint = Color.blue,
            uv = new Vector2(0.5f, 1) * uvRegion.size + uvRegion.min
        });
        mwd.SetNextIndex(0);
        mwd.SetNextIndex(1);
        mwd.SetNextIndex(2);
    }
}
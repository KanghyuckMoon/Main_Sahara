using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using AI;

public class BehaviourTreeEditor : EditorWindow
{
    BehaviourTreeView treeView;
    InspectorView inspectorView;
    Button sortBtn;
    NodeView selectionNodeView;

    [MenuItem("MoonTool/BehaviourTreeEditor/Editor ...")]
    public static void OpenWindow()
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviourTreeEditor");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/01.Scripts/AI/Editor/BehaviourTreeEditor.uxml");
        visualTree.CloneTree(root);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/01.Scripts/AI/Editor/BehaviourTreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        treeView = root.Q<BehaviourTreeView>();
        inspectorView = root.Q<InspectorView>();
        sortBtn = root.Q<Button>();
        sortBtn.clickable.clicked += SortNode;
		treeView.OnNodeSelected = OnNodeSelectionChanged;

		OnSelectionChange();
	}

	private void OnSelectionChange()
	{
        NodeMakeSO nodeMakeSO = Selection.activeObject as NodeMakeSO;
        if(nodeMakeSO)
        {
            treeView.PopulateView(nodeMakeSO);

		}
	}

    void SortNode()
    {
        if(selectionNodeView != null)
        {
            selectionNodeView.node.Sort();
		}
    }

    void OnNodeSelectionChanged(NodeView _nodeView)
    {
        selectionNodeView = _nodeView;
		inspectorView.UpdateSelection(_nodeView);
	}
}
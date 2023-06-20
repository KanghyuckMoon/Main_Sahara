using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using AI;

public class InspectorView : VisualElement
{
	public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

	Editor editor;

	InspectorNodeModel inspectorNodeModel;

	public InspectorView()
	{
		inspectorNodeModel = new InspectorNodeModel();
		inspectorNodeModel.nodeModel = null;
	}

	internal void UpdateSelection(NodeView _nodeView)
	{
		Clear();
		UnityEngine.Object.DestroyImmediate(editor);
		inspectorNodeModel.nodeModel = _nodeView.node;
		editor = Editor.CreateEditor(inspectorNodeModel);
		IMGUIContainer container = new IMGUIContainer(() => { editor.OnInspectorGUI(); });
		Add(container);

	}
}

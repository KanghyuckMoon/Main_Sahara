using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using AI;
using System.Diagnostics;
using System.Linq;

public class BehaviourTreeView : GraphView
{
	public System.Action<NodeView> OnNodeSelected;
	public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits> { }
	NodeMakeSO nodeMakeSO;

	public BehaviourTreeView()
	{
		var _grid = new GridBackground();
		_grid.StretchToParentSize();
		Insert(0, _grid);

		this.AddManipulator(new ContentZoomer());
		this.AddManipulator(new ContentDragger());
		this.AddManipulator(new SelectionDragger());
		this.AddManipulator(new RectangleSelector());

		//Add Style
		var styleSheet = (StyleSheet)EditorGUIUtility.Load("Assets/01.Scripts/AI/Editor/BehaviourTreeEditor.uss");
		styleSheets.Add(styleSheet);
		
	}

	NodeView FindNodeView(NodeModel _node)
	{
		return GetNodeByGuid(_node.guid) as NodeView;
	}

	internal void PopulateView(NodeMakeSO _nodeMakeSO)
	{
		this.nodeMakeSO = _nodeMakeSO;

		graphViewChanged -= OnGraphViewChanged;
		DeleteElements(graphElements);
		graphViewChanged += OnGraphViewChanged;

		//Create NodeViews
		_nodeMakeSO.nodes.ForEach(n => CreateNodeView(n));

		//Create Edgess
		_nodeMakeSO.nodes.ForEach(n =>
		{
			var _child = _nodeMakeSO.GetChild(n);
			_child.ForEach(c =>
			{
				NodeView _parentView = FindNodeView(n);
				NodeView _childView = FindNodeView(c);

				Edge edge = _parentView.output.ConnectTo(_childView.input);
				AddElement(edge);
			});
		});
	}

	public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
	{
		return ports.ToList().Where(endPort =>
		endPort.direction != startPort.direction &&
		endPort.node != startPort.node).ToList();
	}

	public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
	{
		//base.BuildContextualMenu(evt);
		{
			//evt.menu.AppendAction($"ASD", (x) => TestAction());
			//var _types = TypeCache.GetTypesDerivedFrom<NodeType>();
			int _count = (int)NodeType.Count;
			for (int i = 0; i < _count; ++i)
			{
				var _type = (NodeType)i;
				evt.menu.AppendAction($"{_type.ToString()}", (a) => CreateNode(_type));
			}
		}
	}

	void CreateNode(NodeType _type)
	{
		NodeModel _node = nodeMakeSO.CreateNodeModel(_type);
		CreateNodeView(_node);
	}

	void CreateNodeView(NodeModel _node)
	{
		NodeView _nodeView = new NodeView(_node);
		_nodeView.OnNodeSelected = OnNodeSelected;
		AddElement(_nodeView);
	}

	private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
	{
		if(graphViewChange.elementsToRemove != null)
		{
			graphViewChange.elementsToRemove.ForEach(elem =>
			{
				NodeView _nodeView = elem as NodeView;
				if (_nodeView != null)
				{
					nodeMakeSO.DeleteNode(_nodeView.node);
				}

				Edge edge = elem as Edge;
				if (edge != null)
				{
					NodeView parentView = edge.output.node as NodeView;
					NodeView childView = edge.input.node as NodeView;

					nodeMakeSO.RemoveChild(parentView.node, childView.node);
				}
			});
		}

		if(graphViewChange.edgesToCreate != null)
		{
			graphViewChange.edgesToCreate.ForEach(edge =>
			{
				NodeView parentView = edge.output.node as NodeView;
				NodeView childView = edge.input.node as NodeView;

				nodeMakeSO.AddChild(parentView.node, childView.node);
			});
		}

		return graphViewChange;
	}

}

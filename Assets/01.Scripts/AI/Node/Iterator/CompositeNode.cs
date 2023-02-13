using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : INode
{
    public List<INode> childNodeList { get; protected set; }

    // 생성자
    public CompositeNode(params INode[] nodes) => childNodeList = new List<INode>(nodes);

    // 자식 노드 추가
    public CompositeNode Add(INode node)
    {
        childNodeList.Add(node);
        return this;
    }

    public abstract bool Run();
}

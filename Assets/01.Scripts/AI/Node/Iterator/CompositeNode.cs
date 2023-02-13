using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : INode
{
    public List<INode> childNodeList { get; protected set; }

    // ������
    public CompositeNode(params INode[] nodes) => childNodeList = new List<INode>(nodes);

    // �ڽ� ��� �߰�
    public CompositeNode Add(INode node)
    {
        childNodeList.Add(node);
        return this;
    }

    public abstract bool Run();
}

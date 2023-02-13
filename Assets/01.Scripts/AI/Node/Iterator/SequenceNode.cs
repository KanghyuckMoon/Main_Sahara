using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : CompositeNode
{
    public SequenceNode(params INode[] nodes) : base(nodes)
    {

    }

    //False �ϳ��� ���� �� ���� ����
    public override bool Run()
    {
        foreach (var node in childNodeList)
        {
            bool result = node.Run();
            if (result == false)
                return false;
        }
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : CompositeNode
{
    public SelectorNode(params INode[] nodes) : base(nodes)
	{

	}

    //True �ϳ��� ���� �� ���� ����
	public override bool Run()
    {
        foreach (var node in childNodeList)
        {
            bool result = node.Run();
            if (result == true)
                return true;
        }
        return false;
    }
}

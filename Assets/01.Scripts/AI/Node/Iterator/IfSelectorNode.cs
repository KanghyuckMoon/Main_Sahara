using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfSelectorNode : CompositeNode
{
    public Func<bool> Condition { get; private set; }
    public IfSelectorNode(Func<bool> condition, params INode[] nodes) : base(nodes)
    {
        Condition = condition;
    }

    //True �ϳ��� ���� �� ���� ����
    public override bool Run()
    {
        bool firstResult = Condition();
        if (firstResult is false)
		{
            return false;
		}
        foreach (var node in childNodeList)
        {
            bool result = node.Run();
            if (result is true)
                return true;
        }
        return false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConditionCheckNode : INode
{
    private INode node;
    public Func<bool> Condition { get; private set; }

    public ConditionCheckNode(Func<bool> condition, INode node)
    {
        Condition = condition;
        this.node = node;
    }

    public bool Run()
    {
        bool result = Condition();
        if (result)
		{
            return node.Run();
		}
        return result;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfActionNode : ActionNode
{
    public Func<bool> Condition { get; private set; }

    public IfActionNode(Func<bool> condition, Action action)
        : base(action)
    {
        Condition = condition;
    }

    public override bool Run()
    {
        bool result = Condition();
        if (result) Action();
        return result;
    }
}

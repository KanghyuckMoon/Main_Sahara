using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfElseActionNode : INode
{
    public Func<bool> Condition { get; private set; }
    public Action IfAction { get; private set; }
    public Action ElseAction { get; private set; }

    public IfElseActionNode(Func<bool> condition, Action ifAction, Action elseAction)
    {
        Condition = condition;
        IfAction = ifAction;
        ElseAction = elseAction;
    }

    public bool Run()
    {
        bool result = Condition();

        if (result) IfAction();
        else ElseAction();

        return result;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : INode
{
    public Action Action { get; protected set; }
    public ActionNode(Action action)
    {
        Action = action;
    }

    public virtual bool Run()
    {
        Action();
        return true;
    }
}

public class StringActionNode : INode
{
    public string str = null;
    public Action<string> Action { get; protected set; }
    public StringActionNode(Action<string> action)
    {
        Action = action;
    }

    public virtual bool Run()
    {
        Action(str);
        return true;
    }
}

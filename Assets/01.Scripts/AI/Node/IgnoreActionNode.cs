using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class IgnoreActionNode : INode
{
    public Action Action { get; protected set; }
    public IgnoreActionNode(Action action)
    {
        Action = action;
    }

    public virtual bool Run()
    {
        Action();
        return false;
    }
}

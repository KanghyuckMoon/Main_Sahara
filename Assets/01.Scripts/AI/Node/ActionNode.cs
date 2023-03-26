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
        try
        {
            Action();
        }
        catch (Exception e)
        {
            Debug.LogError($"{e}");
        }
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

public class FloatActionNode : INode
{
    public float value = 0f;
    public Action<float> Action { get; protected set; }
    public FloatActionNode(Action<float> action)
    {
        Action = action;
    }

    public virtual bool Run()
    {
        Action(value);
        return true;
    }
}
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

public class IfInvertActionNode : ActionNode
{
    public Func<bool> Condition { get; private set; }

    public IfInvertActionNode(Func<bool> condition, Action action)
        : base(action)
    {
        Condition = condition;
    }

    public override bool Run()
    {
        bool result = !Condition();
        if (result) Action();
        return result;
    }
}
public class IfTimerActionNode : ActionNode
{
    private float originDelay = 0f;
    private float currentDelay = 0f;
    public IfTimerActionNode(float delay, Action action)
        : base(action)
    {
        originDelay = delay;
        currentDelay = 0f;
    }

    public override bool Run()
    {
        if(currentDelay < originDelay)
        {
            currentDelay += Time.deltaTime;
            return false;
        }
        else
        {
            currentDelay -= originDelay;
            Action();
            return true;
        }
    }
}
public class IfStringActionNode : INode
{
    public string str = null;
    public Func<bool> Condition { get; private set; }
    public Action<string> Action { get; protected set; }
    public IfStringActionNode(Func<bool> condition,Action<string> action)
    {
        Condition = condition;
        Action = action;
    }

    public virtual bool Run()
    {
        bool result = Condition();
        if (result) Action(str);
        return result;
    }
}
public class IfIgnoreActionNode : INode
{
    public Func<bool> Condition { get; private set; }
    public Action Action { get; protected set; }
    public IfIgnoreActionNode(Func<bool> condition,Action action)
    {
        Condition = condition;
        Action = action;
    }

    public virtual bool Run()
    {
        bool result = Condition();
        if (result) Action();
        return false;
    }
}

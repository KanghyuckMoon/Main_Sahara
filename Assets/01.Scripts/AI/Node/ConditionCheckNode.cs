using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConditionCheckNode : INode
{
    private INode node;
    public Func<bool> Condition { get; private set; }
    private bool isIgnore = false;
    private bool isInvert = false;
    private bool isInvertTime = false;
    private bool isUseTimer = false;
    private float originDelay = 0f;
    private float currentDelay = 0f;

    public ConditionCheckNode(Func<bool> condition, INode node, bool _isIgnore, bool _isInvert, bool _isUseTimer, float _delay, bool isInvertTime)
    {
        Condition = condition;
        this.node = node;
        isIgnore = _isIgnore;
        isInvert = _isInvert;
        this.isInvertTime = isInvertTime;
        isUseTimer = _isUseTimer;
        originDelay = _delay;
        currentDelay = 0f;
    }

    public bool Run()
    {
        bool result = Condition();

        if(isInvert)
        {
            result = !result;
        }

        if (result)
        {
            if(!isUseTimer)
            {
                node.Run();
                if (isIgnore)
                {
                    return false;
                }
                return true;
            }

            if (currentDelay < originDelay)
            {
                currentDelay += Time.deltaTime;
                if(isInvertTime)
                {
                    return true;
                }
                return false;
            }
            else
            {
                currentDelay -= originDelay;
                node.Run();
                if (isIgnore)
                {
                    return false;
                }
                if (isInvertTime)
                {
                    return false;
                }
                return true;
            }
		}

        return false;
    }
}

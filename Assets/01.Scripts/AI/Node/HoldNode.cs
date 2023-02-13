using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HoldNode : INode
{
    private float originTime = 0f;
    private float currentTime = 0f;
    private Action<KeyCode> holdAction;
    private Action<KeyCode> upAction;
    private KeyCode[] keyCodes;

    public HoldNode(float time, Action<KeyCode> holdAction, Action<KeyCode> upAction, KeyCode[] keyCodes)
    {
        this.originTime = time;
        currentTime = this.originTime;
        this.holdAction = holdAction;
        this.upAction = upAction;
        this.keyCodes = keyCodes;
    }

    public bool Run()
    {
        if (currentTime > 0f)
        {
            foreach (var keyCode in keyCodes)
			{
                holdAction.Invoke(keyCode);
			}
            currentTime -= Time.deltaTime;
            return false;
        }
        else
        {
            foreach (var keyCode in keyCodes)
            {
                upAction.Invoke(keyCode);
            }
            currentTime = originTime;
            return true;
        }
    }
}

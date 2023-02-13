using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNode : INode
{
    private float originTime = 0f;
    private float currentTime = 0f;

    public WaitNode(float time)
    {
        originTime = time;
        currentTime = originTime;
    }

    public bool Run()
	{
        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            return false;
        }
        else
        {
            currentTime = originTime;
            return true;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChoiceNode : CompositeNode
{
    public RandomChoiceNode(params INode[] nodes) : base(nodes) { }

    private float _changeDelay = 0.5f;

    public override bool Run()
    {
        if (_changeDelay < 0f)
		{
            _changeDelay = 0.5f;
		}
        else
		{
            _changeDelay -= Time.deltaTime;
            return false;
        }
        int random = Random.Range(0, childNodeList.Count);
        childNodeList[random].Run();

        return true;
    }
}

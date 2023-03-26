using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PercentRandomChoiceNode : INode
{
	public PercentRandomChoiceNode(float _changeDelay, params Tuple<float, INode>[] _nodes)
    {
        originDelay = _changeDelay;
        changeDelay = _changeDelay;
        tupleNodeList = new List<Tuple<float, INode>>(_nodes);
    }

    private List<Tuple<float, INode>> tupleNodeList;
    private float changeDelay = 0.5f;
    private float originDelay = 0.5f;
    private int random = 0;

    public void Add(Tuple<float, INode> _percentNode)
	{
        tupleNodeList.Add(_percentNode);
    }

    public bool Run()
    {
        if (changeDelay >= originDelay)
        {
            changeDelay -= originDelay;
        }
        else
        {
            tupleNodeList[random].Item2.Run();
            changeDelay += Time.deltaTime;
            return false;
        }
        random = Choose();
        tupleNodeList[random].Item2.Run();

        return true;
    }

    private int Choose()
    {
        float randomPoint = UnityEngine.Random.value * 100f;

        for (int i = 0; i < tupleNodeList.Count; i++)
        {
            if (randomPoint < tupleNodeList[i].Item1)
            {
                return i;
            }
            else
            {
                randomPoint -= tupleNodeList[i].Item1;
            }
        }
        return tupleNodeList.Count - 1;
    }
}

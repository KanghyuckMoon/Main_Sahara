using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelNode : CompositeNode
{
    public ParallelNode(params INode[] nodes) : base(nodes) { }

    public override bool Run()
    {
        foreach (var node in childNodeList)
        {
            node.Run();
        }
        return true;
    }
}

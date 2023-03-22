using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class NodeUtill
{
    public static SelectorNode Selector(params INode[] nodes) => new SelectorNode(nodes);
    public static IfSelectorNode IfSelector(Func<bool> condition, params INode[] nodes) => new IfSelectorNode(condition, nodes);
    public static SequenceNode Sequence(params INode[] nodes) => new SequenceNode(nodes);
    public static ParallelNode Parallel(params INode[] nodes) => new ParallelNode(nodes);
    public static RandomChoiceNode RandomChoice(params INode[] nodes) => new RandomChoiceNode(nodes);
    public static PercentRandomChoiceNode PercentRandomChoiceNode(float changeDelay, params Tuple<float, INode>[] nodes) => new PercentRandomChoiceNode(changeDelay, nodes);
    public static ActionNode Action(Action action) => new ActionNode(action);
    public static StringActionNode StringAction(Action<string> action) => new StringActionNode(action);
    public static IfStringActionNode IfStringAction(Func<bool> condition, Action<string> action) => new IfStringActionNode(condition,action);
    public static Tuple<float, INode> PercentAction(float _percent, INode _action) => new Tuple<float, INode>(_percent, _action);
    public static IgnoreActionNode IgnoreAction(Action action) => new IgnoreActionNode(action);

    public static IfActionNode IfAction(Func<bool> condition, Action action)
        => new IfActionNode(condition, action);
    public static IfInvertActionNode IfInvertActionNode(Func<bool> condition, Action action)
        => new IfInvertActionNode(condition, action);
    public static IfElseActionNode IfElseAction(Func<bool> condition, Action ifAction, Action ifElseAction)
        => new IfElseActionNode(condition, ifAction, ifElseAction);
}
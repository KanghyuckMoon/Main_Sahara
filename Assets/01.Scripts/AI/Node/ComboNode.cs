using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ComboNode : INode
{
    public List<INode> childNodeList { get; protected set; }

    public ComboNode(ComboSO comboSO, Action<KeyCode> holdAction, Action<KeyCode> keyUpAction, Action<KeyCode> tapAction)
    {
        childNodeList = new List<INode>();
        for (int i = 0; i < comboSO.comboInputDatas.Length; ++i)
		{
            INode actionNode = null;

            if (comboSO.comboInputDatas[i].isHold)
            {
                actionNode = new HoldNode(comboSO.comboInputDatas[i].holdTime, holdAction, keyUpAction, comboSO.comboInputDatas[i].keyCode);
            }
            else
            {
                actionNode = new TapNode(tapAction, comboSO.comboInputDatas[i].keyCode);
            }

            childNodeList.Add(actionNode);

            INode waitNode = new WaitNode(comboSO.comboInputDatas[i].delay);
            childNodeList.Add(waitNode);
		}
    }

    int index = 0;

    //모든 노드가 True가 한번 씩 될 때까지 돌림
    public bool Run()
    {
        bool result = childNodeList[index].Run();
        if (result)
        {
            index++;
        }

        if (index == childNodeList.Count)
		{
            index = 0;
            return false;
		}
		else
        {
            return true;
        }
    }
}

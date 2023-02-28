using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class ClassPoolChecker : MonoBehaviour
    {
        [Multiline(10)]
        public string str;

        [ContextMenu("Check")]
        public void Check()
		{
            str = "";
            foreach (var queue in ClassPoolManager.Instance.ClassQueueDic)
			{
                str += $"{queue.Key} {queue.Value.Count}";
                str += '\n';
            }

        }
    }

}
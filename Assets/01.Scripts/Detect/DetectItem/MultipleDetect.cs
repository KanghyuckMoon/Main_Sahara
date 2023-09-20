using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Detect
{
	public class MultipleDetect : MonoBehaviour
	{
		[SerializeField] private List<BaseDetectItem> baseDetectItems = new List<BaseDetectItem>();

		private void Start()
		{
			foreach (var item in baseDetectItems)
			{
				item.getoutEventBefore.AddListener(AllGetOut);
			}
		}

		public void AllGetOut()
		{
			foreach(var item in baseDetectItems)
			{
				if(item.IsGetOut)
				{
					continue;
				}
				item.IsGetOut = true;
				item.GetOut();
			}
		}
	}
}

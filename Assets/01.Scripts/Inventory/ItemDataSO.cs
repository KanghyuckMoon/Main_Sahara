using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	[CreateAssetMenu(fileName = "ItemDataSO", menuName = "SO/ItemDataSO")]
	public class ItemDataSO : ScriptableObject
	{
		public string key;
		public int count;
		public string nameKey; //�̸� Ű
		public string explanationKey; //���� Ű
		public string spriteKey; //��������Ʈ Ű
		public bool stackble;
		public int stackMax = 64;
		public ItemType itemType;
		public string prefebkey;
		public string animationLayer;


	}
}

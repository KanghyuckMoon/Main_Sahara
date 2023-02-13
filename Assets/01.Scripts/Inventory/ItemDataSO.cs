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
		public string nameKey; //이름 키
		public string explanationKey; //설명 키
		public string spriteKey; //스프라이트 키
		public bool stackble;
		public int stackMax = 64;
		public ItemType itemType;
		public string prefebkey;
		public string animationLayer;


	}
}

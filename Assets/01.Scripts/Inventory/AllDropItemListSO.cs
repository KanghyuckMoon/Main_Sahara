using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inventory
{
	[CreateAssetMenu(fileName = "AllDropItemListSO", menuName = "SO/AllDropItemListSO")]
	public class AllDropItemListSO : ScriptableObject
	{
		public List<DropItemListSO> dropItemListSOList = new List<DropItemListSO>();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
	[CreateAssetMenu(fileName = "ShopAllSO", menuName = "SO/ShopAllSO")]
	public class ShopAllSO : ScriptableObject
	{
		public List<ShopSO> shopSOList = new List<ShopSO>();
	}
}

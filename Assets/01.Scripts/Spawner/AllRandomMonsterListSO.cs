using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawner
{
	[CreateAssetMenu(fileName = "AllRandomMonsterListSO", menuName = "SO/AllRandomMonsterListSO")]
	public class AllRandomMonsterListSO : ScriptableObject
	{
		public List<RandomMonsterListSO> randomMonsterSpawnerSOList = new List<RandomMonsterListSO>();
	}
}

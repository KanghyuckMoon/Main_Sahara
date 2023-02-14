using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using Pool;

namespace Spawner
{
	public class MonsterSpawner : MonoBehaviour
	{
		private static Dictionary<string, bool> isSpawnDic = new Dictionary<string, bool>();

		[SerializeField]
		private string spawnerName;
		[SerializeField]
		private string enemyAddress;
		

		public void OnEnable()
		{
			if(isSpawnDic.TryGetValue(spawnerName, out bool _bool))
			{
				if(_bool)
				{
					return;
				}
				else
				{
					_bool = true;
				}
			}
			else
			{
				isSpawnDic.Add(spawnerName, true);
				GameObject obj = ObjectPoolManager.Instance.GetObject(enemyAddress);
				obj.transform.position = transform.position;
				obj.SetActive(true);
			}
		}

	}
}

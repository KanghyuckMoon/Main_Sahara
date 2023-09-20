using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using Pool;
using Streaming;
using UpdateManager;
using Effect;

namespace Spawner
{
	public enum MonsterType
	{
		xa_Nm,
		xa_Nm_Elec,
		xa_Nm_Stone,
		xb_Hp,
		xb_Hp_Air,
		xb_Sr,
		xb_Sr_Life,
		xc_Kr,
		xc_Kr_BigFire,
		xc_Kr_Fire,
		Mummy,
		mc_Hd,
		mc_Km,
		mc_Or,
	}

	public class MonsterSpawner : MonoBehaviour, IRadiusCheck
	{
		private static Dictionary<int, bool> isSpawnDic = new Dictionary<int, bool>();

		private static int index;

		[SerializeField]
		private MonsterType enemyAddress;
		[SerializeField]
		private ObjectDataSO objectDataSO;

		[SerializeField]
		private int curIndex = 0;

		[ContextMenu("SetIndex")]
		public void SetIndex()
		{
			curIndex = index++;

#if UNITY_EDITOR
			UnityEditor.EditorUtility.SetDirty(this);
#endif

		}

		//public void OnEnable()
		//{
		//}

		private void Spawn()
		{
			if (isSpawnDic.TryGetValue(curIndex, out bool _bool))
			{
				if (_bool)
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
				isSpawnDic.Add(curIndex, true);
				GameObject obj = ObjectPoolManager.Instance.GetObject(enemyAddress.ToString());
				ObjectClassCycle objectClassCycle = obj.GetComponentInChildren<ObjectClassCycle>();
				if (objectClassCycle is not null)
				{
					objectClassCycle.TargetObject = obj;
					ObjectSceneChecker _objectSceneChecker = ClassPoolManager.Instance.GetClass<ObjectSceneChecker>();
					if (_objectSceneChecker is null)
					{
						_objectSceneChecker = new ObjectSceneChecker();
					}
					_objectSceneChecker.ObjectDataSO = objectDataSO;
					_objectSceneChecker.ObjectClassCycle = objectClassCycle;
					objectClassCycle.AddObjectClass(_objectSceneChecker);
				}
				obj.transform.position = transform.position + Vector3.up * 1;
				obj.transform.rotation = transform.rotation;
				obj.SetActive(true);
				EffectManager.Instance.SetEffectDefault("BoomSandVFX", transform.position, Quaternion.identity);
			}
			gameObject.SetActive(false);
		}

		public void Add()
		{
			Spawn();
		}

		public void Remove()
		{

		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;


namespace Pool
{
    public class PoolParentManager : Singleton<PoolParentManager>
    {
		private Dictionary<string, Transform> poolParentDic = new Dictionary<string, Transform>();
		private Dictionary<string, Transform> useParentDic = new Dictionary<string, Transform>();

		public void SetPoolParent(string key, GameObject obj)
		{
			obj.transform.SetParent(GetPoolParent(key));
		}
		public void SetUseParent(string key, GameObject obj)
		{
			obj.transform.SetParent(GetUseParent(key));
		}

		public Transform GetPoolParent(string key)
		{
			Transform parent;

			if (!poolParentDic.TryGetValue(key, out parent))
			{
				parent = new GameObject().transform;
				parent.transform.position = Vector3.zero;
				poolParentDic.Add(key, parent);
			}
			parent.name = $"{key} Pool Parent {parent.childCount}";

			return parent;
		}

		public Transform GetUseParent(string key)
		{
			Transform parent;

			if (!useParentDic.TryGetValue(key, out parent))
			{
				parent = new GameObject().transform;
				parent.transform.position = Vector3.zero;
				useParentDic.Add(key, parent);
			}
			parent.name = $"{key} Use Parent {parent.childCount}";

			return parent;
		}
	}
}

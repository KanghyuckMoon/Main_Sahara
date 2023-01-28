using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpdateManager;

namespace Streaming
{
	public interface IObjectClass 
	{ 
		public void Start(); 
		public void Update(); 
	}
	public class ObjectClassCycle : MonoBehaviour, IUpdateObj
	{
		public List<IObjectClass> objectClassList = new List<IObjectClass>();

		public void AddObjectClass(IObjectClass objectClass)
		{
			objectClassList.Add(objectClass);
		}
		public void RemoveObjectClass(IObjectClass objectClass)
		{
			objectClassList.Remove(objectClass);
		}

		public void OnEnable()
		{
			UpdateManager.UpdateManager.Add(this);
		}
		public void OnDisable()
		{
			UpdateManager.UpdateManager.Remove(this);
		}

		public void Start()
		{
			foreach (var obj in objectClassList)
			{
				obj.Start();
			}
		}

		public void UpdateManager_Update()
		{
			for (int i = 0; i < objectClassList.Count; ++i)
			{
				objectClassList[i]?.Update();
			}
		}
	}

}
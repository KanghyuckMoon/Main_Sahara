using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace Streaming
{
	[CreateAssetMenu(fileName = "ObjectDataSO", menuName = "SO/ObjectDataSO")]
	public class ObjectDataSO : ScriptableObject
	{
		public bool isUse;
		public long key;
		public string address = "";
		public Vector3 position;
		public Vector3 rotation;
		public Vector3 scale;

		//LOD
		public string lodAddress = "";
		public LODType lodType;

		//Monster
		public bool isMonster = false;
		public string dataSOPath = null;
	}
}

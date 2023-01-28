using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Streaming
{
	public enum LODType
	{
		None,
		On,
	}
	public class ObjectData
	{
		public static long totalKey = 0;

		public bool isUse;
		public long key;
		public string address = "";
		public Vector3 position;
		public Quaternion rotation;
		public Vector3 scale;
		
		//LOD
		public string lodAddress = "";
		public LODType lodType;

		/// <summary>
		/// 오브젝트 데이터 SO를 복사함
		/// </summary>
		/// <param name="_objectDataSO"></param>
		public void CopyObjectDataSO(ObjectDataSO _objectDataSO)
		{
			this.isUse = _objectDataSO.isUse;
			this.key = _objectDataSO.key;
			this.address = _objectDataSO.address;
			this.position = _objectDataSO.position;
			this.rotation = Quaternion.Euler(_objectDataSO.rotation);
			this.scale = _objectDataSO.scale;
			this.lodAddress = _objectDataSO.lodAddress;
			this.lodType = _objectDataSO.lodType;
		}
	}
}

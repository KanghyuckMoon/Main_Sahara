using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UpdateManager
{
	public class RadiusUpdateAdd : MonoBehaviour
	{
		[SerializeField] private LayerMask layerMask;

		private void OnTriggerEnter(Collider other)
		{
			if (layerMask == (layerMask | (1 << other.gameObject.layer)))
			{
				var obj = other.GetComponent<IRadiusCheck>();
				if(obj != null)
				{
					obj.Add();
					Debug.Log("Add Obj", other.gameObject);
				}
			}
		}
		private void OnTriggerExit(Collider other)
		{
			if (layerMask == (layerMask | (1 << other.gameObject.layer)))
			{
				var obj = other.GetComponent<IRadiusCheck>();
				if (obj != null)
				{
					obj.Remove();
					Debug.Log("Remove Obj", other.gameObject);
				}
			}
		}
	}

}
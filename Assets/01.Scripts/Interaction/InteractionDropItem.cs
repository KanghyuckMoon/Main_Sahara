using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Utill.Pattern;
using Inventory;

namespace Interaction
{
	public class InteractionDropItem : MonoBehaviour, IInteractionItem
	{
		[SerializeField]
		private ItemDataSO itemDataSO = null;

		[SerializeField]
		private string itemAddress = null;

		[SerializeField]
		private GameObject targetObj = null;

		public bool Enabled
		{
			get
			{
				return isEnabled;
			}
			set
			{
				isEnabled = value;
			}
		}

		private bool isEnabled = false;
		private DropItem dropItem = new DropItem();

		public void OnEnable()
		{
			isEnabled = true;
		}

		public void Interaction()
		{
			isEnabled = false;
			dropItem.GetItem(itemDataSO);
			ObjectPoolManager.Instance.RegisterObject(itemAddress, targetObj);
			targetObj.SetActive(false);
		}
	}

}
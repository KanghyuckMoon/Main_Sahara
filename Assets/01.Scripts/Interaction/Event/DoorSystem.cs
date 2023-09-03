using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using DG.Tweening;

namespace Interaction
{
	public class DoorSystem : MonoBehaviour, IInteractionItem
	{


		public bool Enabled
		{
			get
			{
				return !isOpen;
			}
			set
			{

			}
		}

		public string Name
		{
			get
			{
				return nameKey;
			}
		}

		public Vector3 PopUpPos
		{
			get
			{
				return transform.position + new Vector3(0, 1, 0);
			}
		}

		public string ActionName
		{
			get
			{
				if (string.IsNullOrEmpty(needItem))
				{
					return "O00000052";
				}
				else if (InventoryManager.Instance.ItemCheck(needItem, 1))
				{
					return "O00000052";
				}
				return "O00000051";
			}
		}

		[SerializeField] private string nameKey = "M00000010";
		[SerializeField] private string needItem;
		[SerializeField] private Transform movingPosition;
		[SerializeField] private float movingDelay = 1f;
		private bool isOpen;

		public void Interaction()
		{
			if (string.IsNullOrEmpty(needItem))
			{
				Open();
				return;
			}
			else if (InventoryManager.Instance.ItemCheck(needItem, 1))
			{
				Open();
			}
		}

		private void Open()
		{
			isOpen = true;
			transform.DOMove(movingPosition.position, movingDelay);
		}
	}
}

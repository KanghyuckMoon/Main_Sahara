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

		public string Name
		{
			get
			{
				return $"{itemDataSO.nameKey}";
			}
		}

		public string ActionName
		{
			get
			{
				return "O00000033";
			}
		}
		
		public Vector3 PopUpPos
		{
			get
			{
				return transform.position + new Vector3(0, 2, 0);
			}
		}

		[SerializeField]
		private ItemDataSO itemDataSO = null;

		[SerializeField]
		private Rigidbody rigid = null;

		[SerializeField]
		private string itemAddress = null;

		[SerializeField]
		private GameObject targetObj = null;

		[SerializeField]
		private bool isJumping = false;

		private bool isEnabled = false;
		private DropItem dropItem = new DropItem();

        [ContextMenu("GetRigid")]
        public void GetRigid()
        {
            rigid = gameObject.GetComponent<Rigidbody>();
        }
        
        [ContextMenu("GetGameObj")]
        public void GetGameObj()
        {
	        targetObj = gameObject;
        }

		public void Start()
		{
			if(isEnabled is false)
			{
				if (isJumping)
                {
					rigid?.AddForce(Vector3.up * 0.5f, ForceMode.Impulse);
                }
				isEnabled = true;
			}
		}

		public void OnEnable()
		{
			if (isJumping)
			{
				rigid?.AddForce(Vector3.up * 0.5f, ForceMode.Impulse);
			}
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
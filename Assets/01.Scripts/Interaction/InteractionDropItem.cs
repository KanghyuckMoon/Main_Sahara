using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

		[SerializeField] private int count;

		[SerializeField]
		private Rigidbody rigid = null;

		[SerializeField]
		private string itemAddress = null;

		[SerializeField]
		private GameObject targetObj = null;

		[SerializeField]
		private bool isJumping = false;

		private bool isEnabled = false;

		[SerializeField] protected UnityEvent getEvent;

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
			InventoryManager.Instance.AddItem(itemDataSO.key, count);
			ObjectPoolManager.Instance.RegisterObject(itemAddress, targetObj);
			targetObj.SetActive(false);
			getEvent?.Invoke();
		}
		
		#if UNITY_EDITOR

		[ContextMenu("Setting")]
		public void Setting()
		{
			rigid = gameObject.GetComponentInParent<Rigidbody>();
			targetObj = transform.root.gameObject;
		}

#endif
	}

}
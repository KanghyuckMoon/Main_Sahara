using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Popup;
using UI.Base;
using System.Linq;
using UI.ActiveManager;
using Utill.Pattern;
using Module;

namespace Interaction
{
	public class InteractionManager : MonoSingleton<InteractionManager>, IUIManaged
	{
		private Transform Player
		{
			get
			{
				return PlayerObj.Player?.transform;
			}
		}
		private Camera MainCamera
		{
			get
			{
				try
				{
					_mainCamera ??= Camera.main;
					return _mainCamera;
				}
				catch (Exception e)
				{
					_mainCamera ??= Camera.main;
					return _mainCamera;
				}
			}
			set
			{
				_mainCamera = value;
			}
		}

		//[SerializeField]
		private Transform _player = null;
		//[SerializeField]
		private GameObject _targetObject = null;
		[SerializeField]
		private LayerMask _targetLayerMask;
		[SerializeField]
		private float _radius = 1.0f;

		private IInteractionItem _interactionObj; 
		private Camera _mainCamera; 

		private void Update()
		{
			if (Player is null)
			{
				return;
			}
			GetNearObject();
			//UpdateUI();
			InputInteraction();
		}

		private void LateUpdate()
		{
			SetInteractionData(); 
		}

		/// <summary>
		/// GetNearObject
		/// </summary>
		/// <returns></returns>
		private void GetNearObject()
		{
			float minimumDistance = float.MaxValue;
			Collider[] targets = Physics.OverlapSphere(Player.position, _radius, _targetLayerMask);
			foreach (Collider col in targets)
			{
				Vector3 dir = col.transform.position - Player.position;
				if (dir.sqrMagnitude < minimumDistance)
				{
					var component = col.gameObject.GetComponent<IInteractionItem>();
					if (!component.Enabled)
					{
						_interactionObj = null;
						continue;
					}
					_interactionObj = component;
					_targetObject = col.gameObject;
					minimumDistance = dir.sqrMagnitude;
				}
			}

			if (targets.Length == 0)
			{
				_targetObject = null;
				_interactionObj = null;
			}
			
		}
		
		private IPopup activePopup;

		private InteractionUIData uiData; 
		/// <summary>
		/// InputInteraction
		/// </summary>
		private void InputInteraction()
		{

			if (_interactionObj != null && activePopup == null)
			{
				uiData = new InteractionUIData { targetVec = _interactionObj.PopUpPos, textKey = _interactionObj.Name,textTypeKey =  _interactionObj.ActionName};
				activePopup = PopupUIManager.Instance.CreatePopup<InteractionPresenter>(PopupType.Interaction,
					uiData,-1f);
				(UIActiveManager.Instance as IUIManager).Add(this);
			}
			if (_interactionObj == null && activePopup != null)
			{
				activePopup.Undo();
				activePopup = null;
			}
			if (Input.GetKeyDown(KeyCode.F))
			{
				if (_interactionObj != null)
				{
				    Player.gameObject.GetComponent<AbMainModule>().ObjDir = Vector2.zero;
					_interactionObj.Interaction();
				}
			}
		}

		private void SetInteractionData()
		{
			if (_interactionObj != null && activePopup != null)
			{
				
				uiData.targetVec = _interactionObj.PopUpPos;
				activePopup.SetData(uiData);
			}
		}

		//private void OnDrawGizmos()
		//{
		//	if (Application.isEditor)
		//	{
		//		if (!Application.isPlaying)
		//		{
		//			return;
		//		}
		//	}
		//
		//
		//	if (Player == null)
		//	{
		//		return;
		//	}
		//	Gizmos.DrawWireSphere(_player.position, _radius);
		//}

		public void Execute()
		{
			if (activePopup is not null)
			{
				activePopup.InActiveTween();
			}
		}

		public void Undo()
		{
			if (activePopup is not null)
			{
				activePopup.ActiveTween();
			}
		}
	}
}

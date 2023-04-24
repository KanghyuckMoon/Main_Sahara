using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Popup;
using UI.Base;
using System.Linq;
using UI.ActiveManager;
using Utill.Pattern;

namespace Interaction
{
	public class InteractionManager : MonoSingleton<InteractionManager>, IUIManaged
	{
		private Transform Player
		{
			get
			{
				if (_player is null)
				{
					_player = PlayerObj.Player?.transform;

				}
				return _player;
			}
		}
		private Camera MainCamera
		{
			get
			{
				_mainCamera ??= Camera.main;
				return _mainCamera;
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

		/// <summary>
		/// ��ȣ�ۿ� �������� ������ UI�� ������Ʈ�Ѵ�.
		/// </summary>
		//private void UpdateUI()
		//{
		//	if (_interactionObj == null)
		//	{
		//		//UI�� ����
		//		_nameFrame.gameObject.SetActive(false);
		//		_actionFrame.gameObject.SetActive(false);
		//		return;
		//	}

		//	//�̸� UI ����
		//	Vector3 point = MainCamera.WorldToScreenPoint(_targetObject.transform.position);
		//	point.y += 150;
		//	_nameFrame.position = point;
		//	_nameText.text = _interactionObj.Name;

		//	//�׼� UI ����
		//	_actionText.text = _interactionObj.ActionName;

		//	//UI�� Ų��
		//	_nameFrame.gameObject.SetActive(true);
		//	_actionFrame.gameObject.SetActive(true);

		//}

		private IPopup activePopup;

		private InteractionUIData uiData; 
		/// <summary>
		/// InputInteraction
		/// </summary>
		private void InputInteraction()
		{

			if (_interactionObj != null && activePopup == null)
			{
				uiData = new InteractionUIData { targetVec = _interactionObj.PopUpPos, textKey = _interactionObj.Name };
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

		private void OnDrawGizmos()
		{
			if (Application.isEditor)
			{
				if (!Application.isPlaying)
				{
					return;
				}
			}


			if (Player == null)
			{
				return;
			}
			Gizmos.DrawWireSphere(_player.position, _radius);
		}

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

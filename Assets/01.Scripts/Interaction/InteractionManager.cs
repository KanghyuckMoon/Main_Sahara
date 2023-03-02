using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
	public class InteractionManager : MonoBehaviour
	{
		private Transform Player
		{
			get
			{
				_player ??= GameObject.FindGameObjectWithTag("Player").transform;
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
			GetNearObject();
			//UpdateUI();
			InputInteraction();
		}

		/// <summary>
		/// GetNearObject
		/// </summary>
		/// <returns></returns>
		private void GetNearObject()
		{
			GameObject obj = null;
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

		/// <summary>
		/// InputInteraction
		/// </summary>
		private void InputInteraction()
		{
			if (Input.GetKeyDown(KeyCode.F))
			{
				if (_interactionObj != null)
				{
					_interactionObj.Interaction();
				}
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
	}
}

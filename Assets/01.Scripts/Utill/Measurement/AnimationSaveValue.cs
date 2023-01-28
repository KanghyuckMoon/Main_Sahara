using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

namespace Utill.Measurement
{
	/// <summary>
	/// �������� �ִϸ��̼��� �� ������ ���� �����ϴ� Ŭ����. �ִϸ��̼ǿ��� ��� �̾Ƴ��� ������ ���
	/// </summary>
	public class AnimationSaveValue : MonoBehaviour
	{
		private class AnimationValue
		{
			public Vector3 pos;
			public Vector3 rot;
			public Vector3 scale;

			public AnimationValue(Vector3 _pos, Vector3 _rot, Vector3 _scale)
			{
				this.pos = _pos;
				this.rot = _rot;
				this.scale = _scale;
			}

		}

		[SerializeField]
		private Transform rootTransform;
		private Dictionary<int, AnimationValue> keyValuePairs = new Dictionary<int, AnimationValue>();
		private int index = 0;
		
		/// <summary>
		/// ���� �ִϸ��̼� �����͸� ������
		/// </summary>
		[ContextMenu("CopyValue")]
		public void CopyValue()
		{
			index = 100;
			keyValuePairs = new Dictionary<int, AnimationValue>();
			CopyChild(rootTransform);
		}

		/// <summary>
		/// ������ �ִϸ��̼� �����͸� �ٽ� ������
		/// </summary>
		[ContextMenu("PasteValue")]
		public void PasteValue()
		{
			index = 100;
			PasteChild(rootTransform);
		}

		private void CopyChild(Transform _transform)
		{
			for (int i = 0; i < _transform.childCount; ++i)
			{
				Transform _transformChild = _transform.GetChild(i);
				keyValuePairs.Add(index++, new AnimationValue(_transformChild.position, _transformChild.eulerAngles, _transformChild.localScale));
				CopyChild(_transformChild);
			}
		}
		private void PasteChild(Transform _transform)
		{
			for (int i = 0; i < _transform.childCount; ++i)
			{
				Transform _transformChild = _transform.GetChild(i);
				_transformChild.position = keyValuePairs[index].pos;
				_transformChild.eulerAngles = keyValuePairs[index].rot;
				_transformChild.localScale = keyValuePairs[index].scale;
				index += 1;
				PasteChild(_transformChild);
			}
		}
	}
}

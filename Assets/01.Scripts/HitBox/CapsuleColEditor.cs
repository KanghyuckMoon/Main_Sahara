using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitBox
{
	[RequireComponent(typeof(CapsuleCollider)), ExecuteInEditMode]
	public class CapsuleColEditor : MonoBehaviour
	{
		public CapsuleCollider Col
		{
			get
			{
				col ??= GetComponent<CapsuleCollider>();
				return col;
			}
		}

		public HitBoxDatasSO HitBoxDatasSO
		{
			get
			{
				return hitBoxDataSO;
			}
			set
			{
				hitBoxDataSO = value;
			}
		}
		private CapsuleCollider col;
		public HitBoxDatasSO hitBoxDataSO;
		public HitBoxData hitBoxData = new HitBoxData();

		public void SetHitBox(HitBoxData _hitBoxData)
		{
			hitBoxData = _hitBoxData;
		}

		private void Start()
		{
			col = GetComponent<CapsuleCollider>();
		}

		private void Update()
		{
			if (hitBoxData == null)
			{
				return;
			}
			hitBoxData.offset = col.center;
			hitBoxData.radius = col.radius;
			hitBoxData.height = col.height;
			hitBoxData.rotation = transform.eulerAngles;
		}

		[ContextMenu("GetHitBox")]
		public void GetHitBox()
		{
			var _hitboxList = hitBoxDataSO.GetHitboxList(hitBoxData.hitBoxName);
			HitBoxData _hitBoxData = _hitboxList.hitBoxDataList.Find(x => x.ClassificationName == hitBoxData.ClassificationName);
			hitBoxData.Copy(_hitBoxData);
			col.center = hitBoxData.offset;
			col.radius = hitBoxData.radius;
			col.height = hitBoxData.height;
			transform.eulerAngles = hitBoxData.rotation;
		}

		[ContextMenu("Upload")]
		public void Upload()
		{
			if (hitBoxDataSO is null)
			{
				Debug.LogError("SO ¾øÀ½");
				return;
			}
			hitBoxDataSO.UploadHitBoxNoneCopy(hitBoxData);
		}

		[ContextMenu("PositionToOffset")]
		public void PositionToOffset()
		{
			Vector3 pos = transform.position;
			Vector3 rot = transform.eulerAngles;

			transform.position = Vector3.zero;
			transform.eulerAngles = rot;

			GameObject _childObj = new GameObject();
			_childObj.transform.SetParent(transform);
			_childObj.transform.position = pos;

			Vector3 _result = _childObj.transform.localPosition;
			DestroyImmediate(_childObj);
			col.center = _result;
		}
	}
}

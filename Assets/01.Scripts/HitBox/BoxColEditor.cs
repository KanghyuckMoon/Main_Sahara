using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitBox
{
	public class BoxColEditor : MonoBehaviour
	{
		public BoxCollider Col
		{
			get
			{
				col ??= GetComponent<BoxCollider>();
				return col;
			}
		}

		[SerializeField]
		private BoxCollider col;
		public HitBoxDataSO hitBoxDataSO;
		public HitBoxData hitBoxData;

		public void OnValidate()
		{
			//Col.center = hitBoxData.offset;
			//Col.size = hitBoxData.size;
		}

		[ContextMenu("GetHitBox")]
		public void GetHitBox()
		{
			var _hitboxList = hitBoxDataSO.GetHitboxList(hitBoxData.hitBoxName);
			HitBoxData _hitBoxData = _hitboxList.hitBoxDataList.Find(x => x.ClassificationName == hitBoxData.ClassificationName);
			hitBoxData = _hitBoxData;
		}

		[ContextMenu("ResetHitBox")]
		public void ResetHitBox()
		{
			hitBoxData = new HitBoxData();
		}

		[ContextMenu("Refresh")]
		public void Refresh()
		{
			hitBoxData.offset = Col.center;
			hitBoxData.size = Col.size;
		}

		[ContextMenu("Upload")]
		public void Upload()
		{
			if (hitBoxDataSO is null)
			{
				Debug.LogError("SO ¾øÀ½");
				return;
			}
			hitBoxDataSO.UploadHitBox(hitBoxData);
		}
	}
}

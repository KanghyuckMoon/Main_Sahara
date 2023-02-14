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

		private BoxCollider col;
		public HItBoxDataSO hitBoxDataSO;
		public HitBoxData hitBoxData;

		public void OnValidate()
		{
			col.center = hitBoxData.offset;
			col.size = hitBoxData.size;
		}

		[ContextMenu("Refresh")]
		public void Refresh()
		{
			hitBoxData.offset = col.center;
			hitBoxData.size = col.size;
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

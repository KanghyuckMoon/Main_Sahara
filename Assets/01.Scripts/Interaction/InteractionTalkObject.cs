using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module.Talk;
using Module;

namespace Interaction
{
	public class InteractionTalkObject : MonoBehaviour, IInteractionItem
	{
		public bool Enabled
		{
			get
			{
				return true;
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
				return transform.position + new Vector3(0, 0.5f, 0);
			}
		}

		public string ActionName
		{
			get
			{
				return actionKey;
			}
		}

		private AbMainModule mainModule;
		private TalkModule talkModule;

		[SerializeField] private TalkObject talkObject;
		[SerializeField] private string nameKey = "M00000010";
		[SerializeField] private string actionKey = "O00000032";

		public void Interaction()
		{
			if (talkObject.IsCanTalk)
			{
				talkObject.Talk();
			}
		}
	}
}

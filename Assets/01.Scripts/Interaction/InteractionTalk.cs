using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module.Talk;
using Module;

namespace Interaction
{
	public class InteractionTalk : MonoBehaviour, IInteractionItem
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
				return transform.position + new Vector3(0, 2, 0);
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

		[SerializeField] private string nameKey = "M00000010";
		[SerializeField] private string actionKey = "O00000032";

		public void Interaction()
		{
			mainModule ??= gameObject.GetComponentInParent<AbMainModule>();
			talkModule ??= mainModule.GetModuleComponent<TalkModule>(ModuleType.Talk);

			if (talkModule.IsCanTalk)
			{
				talkModule.Talk();
			}
		}
	}
}

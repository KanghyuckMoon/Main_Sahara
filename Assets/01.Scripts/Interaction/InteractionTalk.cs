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
				return "M00000010";
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
				return "M00000010";
			}
		}

		private AbMainModule mainModule;
		private TalkModule talkModule;

		public void Interaction()
		{
			mainModule ??= gameObject.GetComponentInParent<AbMainModule>();
			talkModule ??= mainModule.GetModuleComponent<TalkModule>(ModuleType.Talk);
			talkModule.Talk();
		}
	}
}

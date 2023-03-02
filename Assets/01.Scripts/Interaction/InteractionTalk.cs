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

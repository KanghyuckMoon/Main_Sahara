using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSpreadSheet;
using Utill.Measurement;
using Quest;
using Module;
using Utill.Addressable;
using UI.Dialogue;
using Module.Shop;
using Shop;

namespace Module.Talk
{
	public class TalkModule : AbBaseModule
	{
		public AIModule AIModule
		{
			get
			{
				aiModule ??= mainModule.GetModuleComponent<AIModule>(ModuleType.Input);
				return aiModule;
			}
		}

		private string talkCode;
		[SerializeField]
		private string authorCode;

		private int index = 0;
		private AIModule aiModule;
		private Transform Player
		{
			get
			{
				if (player is null)
				{
					player ??= GameObject.FindGameObjectWithTag("Player").transform;
				}

				return player;
			}
		}
		private ShopModule ShopModule
		{
			get
			{
				return mainModule.GetModuleComponent<ShopModule>(ModuleType.Shop);
			}
		}

		private Transform player = null;
		private TalkDataSO talkDataSO = null;
		private bool isFirst = false;

		public TalkModule(AbMainModule _mainModule, string _talkSOAddress) : base(_mainModule)
		{
			talkDataSO = AddressablesManager.Instance.GetResource<TalkDataSO>(_talkSOAddress);
		}

		public override void Update()
		{
			if(CanTalk())
			{
				Logging.Log("대화 가능");
				if (Input.GetKeyDown(KeyCode.E))
				{
					if (ShopModule is not null)
					{
						ShopManager.Instance.SetShopModule(ShopModule);
					}

					//이벤트로 사용된 대화가 있는가?
					if (!GetText())
					{
						//없을시 기본 대화
						RandomDefaultText();
					}
				}
			}
		}

		private bool CanTalk()
		{
			//적대 상태인지 아닌지
			if(!AIModule.IsHostilities)
			{
				Vector3 _distancePos = Player.position - mainModule.transform.position;
				if(_distancePos.sqrMagnitude < talkDataSO.talkRange * talkDataSO.talkRange)
				{
					return true;
				}
			}
			return false;
		}

		private bool GetText()
		{
			for (int i = 0; i < talkDataSO.talkDataList.Count; ++i)
			{
				TalkData _talkData = talkDataSO.talkDataList[i];
				if (ConditionCheck(_talkData))
				{
					DialoguePresenter.SetTexts(_talkData.authorText, _talkData.talkText);
					return true;
				}
			}
			return false;
		}

		private bool ConditionCheck(TalkData _talkData)
		{
			switch (_talkData.talkCondition)
			{
				case TalkCondition.Quest:
					QuestData questData = QuestManager.Instance.GetQuestData(_talkData.questKey);
					if(_talkData.questState == questData.QuestState)
					{
						return true;
					}
					break;
				case TalkCondition.Position:
					break;
				case TalkCondition.HandWork:
					break;
			}
			return false;
		}

		private void RandomDefaultText()
		{
			int _index = 0;

			if (!isFirst)
			{
				isFirst = true;
			}
			else
			{
				_index = Random.Range(1, talkDataSO.defaultTalkCodeList.Count);
			}
			DialoguePresenter.SetTexts(talkDataSO.defaultAutherCodeList[_index], talkDataSO.defaultTalkCodeList[_index]);
		}

		public void LogText()
		{
			string text = TextManager.Instance.GetText($"{talkCode}_{index}");
			string authortext = TextManager.Instance.GetText($"{authorCode}_{index}");
			Logging.Log($"{text}{authortext}");

			if (authortext[0] is '!')
			{
				switch (authortext)
				{
					case "!END\r":
						return;
					case "!TACTIVE\r":
						QuestManager.Instance.ChangeQuestActive(text);
						return;
					case "!TCLEAR\r":
						QuestManager.Instance.ChangeQuestClear(text);
						return;
				}
			}

			index++;
			LogText();
		}

	}
}

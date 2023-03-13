using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Measurement;
using Quest;
using Module;
using Utill.Addressable;
using UI.Dialogue;
using Module.Shop;
using Shop;
using UI.Manager;
using UI.PublicManager; 

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

		public bool IsEndTalk
		{
			get
			{
				return isEndTalk;
			}
			set
			{
				isEndTalk = value;
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
		private ITalkWithCutScene TalkWithCutScene
		{
			get
			{
				talkWithCutScene ??= mainModule.GetComponent<ITalkWithCutScene>();
				return talkWithCutScene;
			}
		}

		private Transform player = null;
		private TalkDataSO talkDataSO = null;
		private bool isFirst = false;
		private bool isEndTalk = false;
		private bool isTalking = false;
		private bool isCutScene = false;

		private ITalkWithCutScene talkWithCutScene;

		public TalkModule(AbMainModule _mainModule, string _talkSOAddress) : base(_mainModule)
		{
			talkDataSO = AddressablesManager.Instance.GetResource<TalkDataSO>(_talkSOAddress);
		}
		public TalkModule() : base()
		{
		}

		public override void Init(AbMainModule _mainModule, params string[] _parameters)
		{
			base.Init(_mainModule, _parameters);
			talkDataSO = AddressablesManager.Instance.GetResource<TalkDataSO>(_parameters[0]);
		}

		public void Talk()
		{
			if(CanTalk())
			{
				Logging.Log("대화 가능");
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

		public void SetCutScene(bool _bool)
		{
			isCutScene = _bool;
		}

		public void CutSceneTalk()
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

		private bool CanTalk()
		{
			//적대 상태인지 아닌지
			if(!AIModule.IsHostilities && !isTalking && !isCutScene)
			{
				return true;
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
					PublicUIManager.Instance.ScreenUIController.GetScreen<DialoguePresenter>(UI.Base.ScreenType.Dialogue)
						.SetTexts(_talkData.authorText, _talkData.talkText);
					isTalking = true;

					if (_talkData.isUseCutScene)
					{
						TalkWithCutScene.PlayCutScene(_talkData.cutSceneKey);
					}

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
				_index = Random.Range(0, talkDataSO.defaultTalkCodeList.Count);
			}
			isEndTalk = false;
			PublicUIManager.Instance.ScreenUIController.GetScreen<DialoguePresenter>(UI.Base.ScreenType.Dialogue)
				.SetTexts(talkDataSO.defaultAutherCodeList[_index], talkDataSO.defaultTalkCodeList[_index], EndTalk);
			//DialoguePresenter.SetTexts(talkDataSO.defaultAutherCodeList[_index], talkDataSO.defaultTalkCodeList[_index]);
		}

		private void EndTalk()
		{
			isEndTalk = true;
			isTalking = false;
		}

	}
}

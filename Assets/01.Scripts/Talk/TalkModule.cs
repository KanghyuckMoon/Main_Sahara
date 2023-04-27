using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Measurement;
using Quest;
using Module;
using Utill.Addressable;
using Module.Shop;
using Shop;
using UI.PublicManager;
using Pool;

namespace Module.Talk
{
	public class TalkModule : AbBaseModule
	{
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
		private System.Action<int> pathAction;
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

		public bool IsCanTalk
		{
			get
			{
				return isCanTalk;
			}
			set
			{
				isCanTalk = value;
			}
		}

		private Transform player = null;
		private TalkDataSO talkDataSO = null;
		private bool isFirst = false;
		private bool isEndTalk = false;
		private bool isTalking = false;
		private bool isCutScene = false;

		private TalkData priorTalkData = null;
		
		private ITalkWithCutScene talkWithCutScene;

		private bool isCanTalk = true; 

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
			if (!isCanTalk)
			{
				return;
			}
			if(!isTalking)
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
				isTalking = true;
			}
		}

		public void SetCutScene(bool _bool)
		{
			isCutScene = _bool;
		}

		public void CutSceneTalk(string _talkKey)
		{
			if (ShopModule is not null)
			{
				ShopManager.Instance.SetShopModule(ShopModule);
			}

			//이벤트로 사용된 대화가 있는가?
			//if (!GetText())
			//{
			//	//없을시 기본 대화
			//	RandomDefaultText();
			//}

			var _talkData = talkDataSO.talkDataList.Find(x => x.talkCondition == TalkCondition.CutScene && x.talkKey == _talkKey);
			if (_talkData is null)
			{
				Debug.LogError("TalkData is null");
			}
			priorTalkData = _talkData;
			PublicUIManager.Instance.SetTexts(_talkData.authorText, _talkData.talkText, EndTalk);
			isEndTalk = false;
			isTalking = true;
		}

		private bool GetText()
		{
			for (int i = 0; i < talkDataSO.talkDataList.Count; ++i)
			{
				TalkData _talkData = talkDataSO.talkDataList[i];
				if (ConditionCheck(_talkData))
				{
					priorTalkData = _talkData;
					
					PublicUIManager.Instance.SetTexts(_talkData.authorText, _talkData.talkText, EndTalk);
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
					foreach (var questCondition in _talkData.questConditionList)
					{
						QuestData questData = QuestManager.Instance.GetQuestData(questCondition.questKey);
						if(questCondition.questState != questData.QuestState)
						{
							return false;
						}
					}
					break;
				case TalkCondition.Position:
					break;
				case TalkCondition.HandWork:
					return false;
				case TalkCondition.CutScene:
					return false;
			}
			return true;
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
			PublicUIManager.Instance.SetTexts(talkDataSO.defaultAutherCodeList[_index], talkDataSO.defaultTalkCodeList[_index], EndTalk);
			//DialoguePresenter.SetTexts(talkDataSO.defaultAutherCodeList[_index], talkDataSO.defaultTalkCodeList[_index]);
		}

		private void EndTalk()
		{
			isEndTalk = true;
			isTalking = false;
			if (priorTalkData is not null && priorTalkData.isUseSmoothPath)
			{
				pathAction?.Invoke(priorTalkData.smoothPathIndex);
			}
		}

		public void AddSmoothPathAction(System.Action<int> action)
		{
			pathAction += action;
		}

		public override void OnDisable()
		{
			player = null;
			talkDataSO = null;
			priorTalkData = null;
			talkWithCutScene = null;
			pathAction = null;
			base.OnDisable();
			ClassPoolManager.Instance.RegisterObject<TalkModule>("TalkModule", this);
		}

		public override void OnDestroy()
		{
			player = null;
			talkDataSO = null;
			priorTalkData = null;
			talkWithCutScene = null;
			pathAction = null;
			base.OnDestroy();
			ClassPoolManager.Instance.RegisterObject<TalkModule>("TalkModule", this);
		}
	}
}

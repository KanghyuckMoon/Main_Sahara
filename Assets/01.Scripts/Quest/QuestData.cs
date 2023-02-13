using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Streaming;

namespace Quest
{
	public interface IQuestCondition 
	{
		public bool IsClear();
	}
	public partial class QuestData
	{
		public class QuestCondition<T> : IQuestCondition
		{
			public Predicate<T> ClearCondition
			{
				get
				{
					return _clearCondition;
				}
				set
				{
					_clearCondition = value;
				}
			}
			public T Parameter
			{
				get
				{
					return _parameter;
				}
				set
				{
					_parameter = value;
				}
			}

			private Predicate<T> _clearCondition;
			private T _parameter;

			public bool IsClear()
			{
				return _clearCondition.Invoke(_parameter);
			}
		}

		public QuestData(string questKey, string nameKey, string explanationKey, QuestState earlyQuestState, QuestConditionType questConditionType, List<QuestCreateObjectSO> questCreateObjectSOList, List<string> linkQuestKeyList, bool isTalkQuest)
		{
			this.questKey = questKey;
			this.nameKey = nameKey;
			this.explanationKey = explanationKey;
			this.questState = earlyQuestState;
			this.questConditionType = questConditionType;
			this.questCreateObjectSOList = questCreateObjectSOList;
			this.linkQuestKeyList = linkQuestKeyList;
			this.isTalkQuest = isTalkQuest;
		}

		public void SetCondition<T>(Predicate<T> clearCondition, T paremeter)
		{
			QuestCondition<T> _questCondition = new QuestCondition<T>();
			_questCondition.ClearCondition = clearCondition;
			_questCondition.Parameter = paremeter;
			condition = _questCondition;
		}

		public string QuestKey
		{
			get
			{
				return questKey;
			}
			set
			{
				questKey = value;
			}
		}
		public string NameKey
		{
			get
			{
				return nameKey;
			}
			set
			{
				nameKey = value;
			}
		}
		public string ExplanationKey
		{
			get
			{
				return explanationKey;
			}
			set
			{
				explanationKey = value;
			}
		}

		public QuestState QuestState
		{
			get
			{
				return questState;
			}
			set
			{
				questState = value;
			}
		}

		public QuestConditionType QuestConditionType
		{
			get
			{
				return questConditionType;
			}
			set
			{
				questConditionType = value;
			}
		}
		public QuestCategory QuestCategory
		{
			get
			{
				return questCategory;
			}
			set
			{
				questCategory = value;
			}
		}

		public List<string> LinkQuestKeyList
		{
			get
			{
				return linkQuestKeyList;
			}
			set
			{
				linkQuestKeyList = value;
			}
		}
		public List<QuestCreateObjectSO> QuestCreateObjectSOList
		{
			get
			{
				return questCreateObjectSOList;
			}
			set
			{
				questCreateObjectSOList = value;
			}
		}
		public bool IsTalkQuest
		{
			get
			{
				return isTalkQuest;
			}
		}

		public bool IsClear()
		{
			if (condition is null)
			{
				return false;
			}

			return condition.IsClear();
		}

		private string questKey;
		private string nameKey;
		private string explanationKey;
		private QuestState questState;
		private QuestConditionType questConditionType;
		private QuestCategory questCategory;
		private List<string> linkQuestKeyList; 
		private List<QuestCreateObjectSO> questCreateObjectSOList;
		private IQuestCondition condition;
		private bool isTalkQuest = false;
	}
}
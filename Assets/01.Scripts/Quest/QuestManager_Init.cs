using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Streaming;
using Utill.Pattern;

namespace Quest
{
    public partial class QuestManager : MonoSingleton<QuestManager>
    {
        partial void InitQuestData()
		{
            foreach (var _questSO in questDataAllSO.questDataSOList)
			{
                questDataDic.Add(_questSO.questKey, new QuestData(_questSO.questKey, _questSO.nameKey, _questSO.explanationKey, _questSO.earlyQuestState, _questSO.questConditionType, _questSO.questCreateObjectSOList, _questSO.linkQuestKeyList, _questSO.isTalkQuest));

                if (_questSO.earlyQuestState == QuestState.Discoverable || _questSO.earlyQuestState == QuestState.Active)
				{
                    CreateAllObject(_questSO.questCreateObjectSOList);
				}

				switch (_questSO.questConditionType)
				{
					case QuestConditionType.Position:
						Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
						questDataDic[_questSO.questKey].SetCondition<Transform>(_ => Vector3.Distance(player.position, _questSO.goalPosition) <= _questSO.distance, player);
						break;
					case QuestConditionType.TargetMonster:
						break;
					case QuestConditionType.MonsterType:
						break;
					case QuestConditionType.DebugData:
						break;
					case QuestConditionType.TargetObject:
						break;
					case QuestConditionType.MiniGame:
						break;
					case QuestConditionType.Stat:
						break;
					case QuestConditionType.Inventory:
						break;
					case QuestConditionType.Mission:
						break;
					case QuestConditionType.TargetNPC:
						break;
					case QuestConditionType.TargetInteractionItem:
						break;
					case QuestConditionType.Time:
						questDataDic[_questSO.questKey].SetCondition<Time>(_ => Time.time > _questSO.afterTime, null);
						break;
					default:
					case QuestConditionType.Handwork:
						break;
				}
			}
		}
	}
}
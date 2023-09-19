using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Streaming;
using Utill.Pattern;
using Utill.Addressable;
using Inventory;

namespace Quest
{
    public partial class QuestManager : MonoSingleton<QuestManager>
    {
        partial void InitQuestData()
		{

			questDataAllSO = AddressablesManager.Instance.GetResource<QuestDataAllSO>("QuestAllDataSO");
			questSaveDataSO = AddressablesManager.Instance.GetResource<QuestSaveDataSO>("QuestSaveDataSO");

			questDataDic.Clear();
			foreach (var _questSO in questDataAllSO.questDataSOList)
			{
				try
				{
					questDataDic.Add(_questSO.questKey, new QuestData(_questSO.questKey, _questSO.nameKey, _questSO.explanationKey, _questSO.earlyQuestState, _questSO.questConditionType, _questSO.questCreateObjectSOList, _questSO.linkQuestKeyList, _questSO.isTalkQuest));
				}
				catch
				{
					Debug.LogError($"Error {_questSO.name}", _questSO);
				}
                //            if (_questSO.earlyQuestState == QuestState.Discoverable || _questSO.earlyQuestState == QuestState.Active)
				//{
    //                CreateAllObject(_questSO.questCreateObjectSOList);
				//}

				switch (_questSO.questConditionType)
				{
					case QuestConditionType.Position:
						try
						{
							Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
							questDataDic[_questSO.questKey].SetCondition<Transform>(_ => player is null ? false : Vector3.Distance(player.position, _questSO.goalPosition) <= _questSO.distance, player);
						}
						catch
						{

						}
						break;
					case QuestConditionType.TargetMonster:
						break;
					case QuestConditionType.TargetObject:
						break;
					case QuestConditionType.Inventory:
						questDataDic[_questSO.questKey].SetCondition<InventoryManager>(_ => InventoryManager.Instance.ItemCheck(_questSO.itemKey, _questSO.needCount), null);
						
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
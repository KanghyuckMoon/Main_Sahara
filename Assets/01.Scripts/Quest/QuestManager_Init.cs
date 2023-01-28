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
                questDataDic.Add(_questSO.questKey, new QuestData(_questSO.questKey, _questSO.nameKey, _questSO.explanationKey, _questSO.earlyQuestState, _questSO.questConditionType, _questSO.questCreateObjectSOList, _questSO.linkQuestKeyList));

                if (_questSO.earlyQuestState == QuestState.Discoverable || _questSO.earlyQuestState == QuestState.Active)
				{
                    CreateAllObject(_questSO.questCreateObjectSOList);
                }
            }

            questDataDic["Test0"].SetCondition<Time>(_ => Time.time > 10, null);
            questDataDic["Test1"].SetCondition<Time>(_ => Time.time > 20, null);
        }
    }
}
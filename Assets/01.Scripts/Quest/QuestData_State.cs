using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
	public enum QuestState
	{
		Disable, //비활성화
		Discoverable, //발견 가능
		Active, //활성화
		Achievable, //달성가능
		Clear, //클리어
		NotClear, //Disable Discoverable Active Achievable
	}

	public enum QuestConditionType
	{
		Position, //위치
		TargetMonster, //타겟 몬스터
		TargetObject, //타겟 오브젝트
		Inventory, //인벤토리
		Time, //시간
		/// <summary>
		/// 프로그래머가 수작업하는 조건
		/// </summary>
		Handwork, //수작업
	}

	public enum QuestCategory
	{
		Main,
		Sub
	}

	public partial class QuestData
	{
	}
}



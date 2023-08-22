using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

public class GameEventManager : MonoSingleton<GameEventManager>
{
	public AllGameEvent allGameEvent;
	public Dictionary<string, GameEvent> gameEventDic = new Dictionary<string, GameEvent>();
	private bool isInit = false;

	private void Init()
	{
		foreach(var gameEvent in allGameEvent.gameEventList)
		{
			gameEventDic.Add(gameEvent.name, gameEvent);
		}
		isInit = true;
	}

	public GameEvent GetGameEvent(string key)
	{
		if(!isInit)
		{
			Init();
		}
		return gameEventDic[key];
	}
}

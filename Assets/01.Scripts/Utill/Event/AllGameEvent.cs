using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AllGameEvent : ScriptableObject
{
	public List<GameEvent> gameEventList = new List<GameEvent>();
}

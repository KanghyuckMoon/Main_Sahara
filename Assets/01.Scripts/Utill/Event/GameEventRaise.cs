using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

public class GameEventRaise : MonoBehaviour
{
    public GameEvent Event;
    public string str;
    public bool isStr;

    // Update is called once per frame
    public void EventRaise()
    {
        if(isStr)
        {
			GameEventManager.Instance.GetGameEvent(str).Raise();
        }
        else
        {
            Event.Raise();
        }
    }
}

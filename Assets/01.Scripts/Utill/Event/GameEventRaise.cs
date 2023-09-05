using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventRaise : MonoBehaviour
{
    public GameEvent Event;
    
    // Update is called once per frame
    void EventRaise()
    {
        Event.Raise();
    }
}

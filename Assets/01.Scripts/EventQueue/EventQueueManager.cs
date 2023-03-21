using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Utill.Pattern;

namespace EventQueue
{
    public class EventQueueManager : MonoSingleton<EventQueueManager>
    {
        private Queue<Action> actionQueue = new Queue<Action>();
    
        public void AddAction(Action _action)
        {
            actionQueue.Enqueue(_action);
        }
    
        public void Start()
        {
            StartCoroutine(UpdateQueue());
        }

        private IEnumerator UpdateQueue()
        {
            while (true)
            {
                if(actionQueue.Count > 0)
                {
                    Action _action = actionQueue.Dequeue();
                    _action.Invoke();
                }   
                yield return null;
            }
        }
    }
}


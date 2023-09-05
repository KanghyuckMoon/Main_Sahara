using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEvent : MonoBehaviour
{
        public UnityEvent events;

        private bool isOnlyOne = true;
        private bool isUse;
        
        private void OnTriggerEnter(Collider other)
        {
                if (isUse)
                {
                        return;
                }

                if( other.CompareTag("Player"))
                {
                        events?.Invoke();
                        if (isOnlyOne)
                        {
                                isUse = true;
                        }
                }
        }

        
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pool;
using Utill.Pattern;

namespace Interaction
{
    public class InteractionEvent : MonoBehaviour, IInteractionItem
    {
        public bool Enabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
            }
        }

        public string Name
        {
            get
            {
                return nameKey;
            }
        }

        public Vector3 PopUpPos { get; }

        public string ActionName
        {
            get
            {
                return actionKey;
            }
        }

        [SerializeField] private string nameKey;
        [SerializeField] private string actionKey;
        [SerializeField] private UnityEvent unityEvent;
        [SerializeField] private bool isEnabled = true;
        [SerializeField] private bool isOnlyOne;
        
        public void Interaction()
        {
            if (isEnabled)
            {
                unityEvent?.Invoke();
                if (isOnlyOne)
                {
                    isEnabled = false;
                }
            }
        }

    }
}

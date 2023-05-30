using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Module
{
    public class EndSkillAnimation : MonoBehaviour
    {
        [SerializeField] 
        private UnityEvent endEvent;

        public void EndEvent()
        {
            endEvent?.Invoke();
        }
        
    }   
}

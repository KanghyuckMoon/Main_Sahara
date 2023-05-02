using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeManager;

namespace Module
{
    public class SettingTime : MonoBehaviour
    {
        private AbMainModule mainModule;

        private bool isRunning = false;
        private float durationTime;

        private float originSpeed;

        private void Start()
        {
            mainModule = GetComponent<AbMainModule>();
        }

        private void Update()
        {
            if (durationTime >= 0)
            {
                durationTime -= Time.deltaTime;
            }

            if(durationTime < 0 && isRunning)
            {
                ResetTime();
            }
        }

        public void SetTime(float _duration, float _slowvalue)
        {
            durationTime = _duration;

            originSpeed = mainModule.PersonalTime;
            isRunning = true;
            mainModule.PersonalTime = _slowvalue;
        }

        private void ResetTime()
        {
            isRunning = false;
            mainModule.PersonalTime = 1;
        }
        
        public void SettingCanMove(int _on)
        {
            bool _isOn = _on > 0;
            mainModule.CanMove = _isOn;
            mainModule.PersonalTime = originSpeed;
        }
    }
}
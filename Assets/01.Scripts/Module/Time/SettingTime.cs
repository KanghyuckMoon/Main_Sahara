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
            isRunning = false;
        }

        private void Update()
        {
            if (durationTime > 0)
            {
                durationTime -= Time.deltaTime;
               // Debug.LogError(durationTime);
            }

            if(durationTime < 0 && isRunning)
            {//Debug.LogError("skadjf");
                ResetTime();
            }
        }

        public void SetTime(float _duration, float _slowvalue)
        {
            Debug.Log(transform.name + ": 타임세팅 : " + _duration + _slowvalue);
            if (_duration <= 0) return;
            durationTime = _duration;
            
            isRunning = true;
            
            //originSpeed = mainModule.EntireTime;
            mainModule.EntireTime = _slowvalue;
            
            //Debug.LogError(Time.timeScale);
        }

        private void ResetTime()
        {
            isRunning = false;
            mainModule.EntireTime = 1;
        }
        
        public void SettingCanMove(int _on)
        {
            bool _isOn = _on > 0;
            mainModule.CanMove = _isOn;
            mainModule.EntireTime = originSpeed;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.GameUI.Explorer;
using UnityEngine;

namespace Arena
{
    public interface IArenaPlatform 
    {
        public List<Action> ActionList { get;  }
        public float startDelay { get;  } // 액션 처음 시작시 딜레이
        public float actionDelay { get;  } // 액션 간의 딜레이 
        public float tweenDuration { get; } // 액션시 진행되는속도 
        public void StartAction(); 
    }    
}


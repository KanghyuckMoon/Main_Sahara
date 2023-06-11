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
        public float startDelay { get;  } // �׼� ó�� ���۽� ������
        public float actionDelay { get;  } // �׼� ���� ������ 
        public float tweenDuration { get; } // �׼ǽ� ����Ǵ¼ӵ� 
        public void StartAction(); 
    }    
}


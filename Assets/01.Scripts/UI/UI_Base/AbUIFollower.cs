using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Base
{
    public abstract class AbUIFollower<T> : IUIFollower,Observer where T : class
    {
        protected T data; 
        public UIDocument RootUIDocument { get; set; }

        public abstract void Awake();

        public void Start(object _data)
        {
            this.data = _data as T; 
        }

        public abstract void UpdateUI();

        public void Receive()
        {
            UpdateUI();
        }
    }

}


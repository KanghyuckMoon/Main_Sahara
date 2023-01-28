using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public interface IUpdateUI
    {
        public UIDocument Root { get; set;  }
        public void Awake(UIDocument root);
        public void Start();

        public void UpdateUI();
    }
}



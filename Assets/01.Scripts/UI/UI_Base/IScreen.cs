using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Base
{
    public interface IScreen
    {
        //public ScreenUIController ScreenController { get; set; }; 
        //public void Init(ScreenUIController _screenController)
        //{
        //    this.ScreenController = _screenController; 
        //}
        public bool ActiveView();
        public void ActiveView(bool _isActive);
    }

}

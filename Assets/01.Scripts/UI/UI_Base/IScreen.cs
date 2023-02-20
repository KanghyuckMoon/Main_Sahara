using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Base
{
    public interface IScreen
    {
        public IUIController UIController { get; set; }
        public void Init(IUIController _uiController)
        {
            this.UIController = _uiController;
        }
        public bool ActiveView();
        public void ActiveView(bool _isActive);
    }

}

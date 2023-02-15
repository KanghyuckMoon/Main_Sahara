using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Base
{
    public interface IScreen
    {
        public bool ActiveView();
        public void ActiveView(bool _isActive);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public interface IScreen
    {
        public bool ActiveView();
        public void ActiveView(bool _isActive);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    /// <summary>
    /// Monobehaviour를 상속 받고 Presenters를 관리하며 UIDocument를 넘겨줘라 
    /// </summary>
    public interface IUIOwner
    {
        public UIDocument RootUIDocument { get;  }
        public List<IUIFollower> PresenterList { get;  }
    }

}

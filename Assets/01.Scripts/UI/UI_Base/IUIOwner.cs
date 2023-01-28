using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    /// <summary>
    /// Monobehaviour�� ��� �ް� Presenters�� �����ϸ� UIDocument�� �Ѱ���� 
    /// </summary>
    public interface IUIOwner
    {
        public UIDocument RootUIDocument { get;  }
        public List<IUIFollower> PresenterList { get;  }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

namespace  UI.Base
{
    public interface IUIManager
    {
        public List<IUIManaged> UIManagedList { get;  }
        public List<IUIManaged> UIIgnoredList { get;  }

        public void Add(IUIManaged _uiManaged)
        {
            UIManagedList.Add(_uiManaged);
        }

        public void Remove(IUIManaged _uiManaged)
        {
            UIManagedList.Remove(_uiManaged);
        }

        public void ExecuteAll()
        {
            foreach (var _ui in UIManagedList)
            {
                if (UIIgnoredList.Contains(_ui) == false)
                {
                    _ui.Execute();
                }
            }
        }
        
        public void UndoAll()
        {
            foreach (var _ui in UIManagedList)
            {
                if (UIIgnoredList.Contains(_ui) == false)
                {
                    _ui.Undo();
                }
            }
        }

        public void ClearIgnore()
        {
            UIIgnoredList.Clear();
        }
    }    
}


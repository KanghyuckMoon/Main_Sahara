using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

namespace  UI.Base
{
    public interface IUIManager
    {
        public List<IUIManaged> UIManagedList { get;  }
        public List<IUIManaged> UIIgnoredList { get;  }

        public void Add(IUIManaged _uiManaged)
        {
            if (UIManagedList.Contains(_uiManaged) is true) return; 
            UIManagedList.Add(_uiManaged);
        }

        public void Remove(IUIManaged _uiManaged)
        {
            UIManagedList.Remove(_uiManaged);
        }

        public void Execute(bool _isExecute)
        {
            if (_isExecute == true)
            {
                ExecuteAll();
                return; 
            }
            UndoAll();
        }
        public void ExecuteAll()
        {
            for (int i = 0; i < UIManagedList.Count; i++)
            {
                var _ui = UIManagedList[i]; 
                if (UIIgnoredList.Contains(_ui) == false)
                {
                    _ui.Execute();
                }
            }
            /*foreach (var _ui in UIManagedList)
            {
                if (UIIgnoredList.Contains(_ui) == false)
                {
                    _ui.Execute();
                }
            }*/
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


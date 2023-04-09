using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Base
{
    public interface IUIManaged
    {
        public void Execute();
        public void Undo();  
    }    
}


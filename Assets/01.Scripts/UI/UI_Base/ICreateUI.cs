using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Base
{
    public interface ICreateUI
    {
        public (VisualElement,AbUI_Base) CreateUI(); 
    }

}

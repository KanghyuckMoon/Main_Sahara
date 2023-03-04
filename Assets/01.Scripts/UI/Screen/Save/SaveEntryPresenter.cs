using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Production;
using UI.ConstructorManager; 

namespace UI.Save
{
    public class SaveEntryPresenter : MonoBehaviour
    {
        private SaveEntryView saveEntryView;
        private VisualElement parent; 
        public SaveEntryPresenter()
        {
           var _pr = UIConstructorManager.Instance.GetProductionUI(typeof(SaveEntryView));
            saveEntryView = _pr.Item2 as SaveEntryView;
            parent = _pr.Item1; 
        }

        public void SetParent(VisualElement _parent)
        {
            _parent.Add(this.saveEntryView.ParentElement);
        }
    }

}

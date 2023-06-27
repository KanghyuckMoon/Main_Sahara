using System.Collections;
using System.Collections.Generic;
using UI.UtilManager;
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

    public abstract class AbBaseScreen : IScreen
    {
        public abstract IUIController UIController { get; set; }

        public virtual bool ActiveView()
        {
            // 사운드 재생
            UIUtilManager.Instance.PlayUISound(UISoundType.ShowScreen);
            return true; 
        }

        public virtual void ActiveView(bool _isActive)
        {
            // 사운드 재생
            UIUtilManager.Instance.PlayUISound(UISoundType.ShowScreen);

        }
    }
}

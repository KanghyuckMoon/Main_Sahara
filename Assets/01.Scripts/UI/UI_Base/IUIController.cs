using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Base
{
    public interface IUIController
    {
        // 다른 UIController가 생긴다면 ScreenType을 Type으로 바꾸고 wher 조건 없애라 
        public T GetScreen<T>(ScreenType _screenType) where T : IScreen;
        public void ActiveScreen(ScreenType _screenType, bool _isActive); // 이것도 지우고 

    }

}
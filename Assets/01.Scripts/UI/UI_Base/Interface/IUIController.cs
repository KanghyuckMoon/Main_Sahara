using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Base
{
    public interface IUIController
    {
        // �ٸ� UIController�� ����ٸ� ScreenType�� Type���� �ٲٰ� wher ���� ���ֶ� 
        public T GetScreen<T>(ScreenType _screenType) where T : IScreen;
        public void ActiveScreen(ScreenType _screenType, bool _isActive); // �̰͵� ����� 

    }

}
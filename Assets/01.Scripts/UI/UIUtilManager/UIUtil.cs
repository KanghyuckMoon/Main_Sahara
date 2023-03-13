using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.UtilManager
{
    public class UIUtil : MonoBehaviour
    {
        public static void SendEvent(RadioButton _btn)
        {
            using (var e = new NavigationSubmitEvent() { target = _btn })
                _btn.SendEvent(e);
        }
        public static void SendEvent(Button _btn)
        {
            using (var e = new NavigationSubmitEvent() { target = _btn })
                _btn.SendEvent(e);
        }
    }

}

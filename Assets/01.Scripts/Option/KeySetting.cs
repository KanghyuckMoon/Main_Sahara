using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

namespace Option
{
    public class KeySetting : MonoBehaviour
    {
        public void ChangeKey(string _str, KeyCode _keyCode)
        {
            InputManager.Instance.ChangeKey(_str, _keyCode);
        }
    }   
}

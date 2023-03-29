
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  UI
{
    public class CursorModule
    {
        private Texture2D cursorImage;
        private bool isCursorVisible = false;

        public CursorModule(Texture2D _image = null,bool _isCursorVisible = false)
        {
            this.cursorImage = _image;
            this.isCursorVisible = _isCursorVisible; 
        }
        public void ActiveCursor(bool _isActive)
        {
            isCursorVisible = _isActive;
            Cursor.visible = isCursorVisible; 
            if (isCursorVisible)
            {
                Cursor.lockState = CursorLockMode.None;
                return;
            }
            Cursor.lockState = CursorLockMode.Locked;

        }
    }    
}


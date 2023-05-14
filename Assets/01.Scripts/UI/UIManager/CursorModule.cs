
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utill.Addressable;
using Utill.SeralizableDictionary;

namespace  UI
{
    public class CursorModule
    {
        private CursorImageSO cuursorImageSO; 
        private Texture2D cursorImage;
        private bool isCursorVisible = false;

        public CursorModule(Texture2D _image = null,bool _isCursorVisible = false)
        {
            this.cursorImage = _image;
            this.isCursorVisible = _isCursorVisible;
            cuursorImageSO = AddressablesManager.Instance.GetResource<CursorImageSO>("CursorImageSO");
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

        public void SetCursor(CursorImageType _sprite)
        {
            Texture2D _image = cuursorImageSO.GetCursorImage(_sprite); 
            Cursor.SetCursor(_image,Vector2.zero,CursorMode.Auto);
        }
    }    
}


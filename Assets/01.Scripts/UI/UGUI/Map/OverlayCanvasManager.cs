using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using UI.Base;
using Utill.Addressable;

namespace  UI.Canvas
{
    public class OverlayCanvasManager : MonoSingleton<OverlayCanvasManager>
    {
        // »ý¼º 
        // particle, 
        private CanvasUIComp canvasUIComp;
        private CanvasScreenDataSO canvasScreenDataSO;
        public override void Awake()
        {
            base.Awake();

            canvasScreenDataSO = AddressablesManager.Instance.GetResource<CanvasScreenDataSO>("OverlayCanvasDataSO");
            canvasUIComp = new CanvasUIComp("OverlayCanvas",canvasScreenDataSO);
        }

        public void ActvieScreen(ScreenType _screenType,bool _isActive)
        {¤·
            canvasUIComp.ActvieScreen(_screenType, _isActive);
        }
        public void UpdateLinesScale(ScreenType _screenType,Vector2 _scale)
        {
            canvasUIComp.UpdateScale(_screenType,_scale);
        }
    }
    
}

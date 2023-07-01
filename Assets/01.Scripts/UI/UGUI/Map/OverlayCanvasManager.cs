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
        // 생성 
        // particle, 
        private CanvasUIComp canvasUIComp;
        private CanvasScreenDataSO canvasScreenDataSO;
        private LinerComp linerComp; 
        
        // 프로퍼티 
        public Transform Canvas => canvasUIComp.Canvas; 
        public override void Awake()
        {
            base.Awake();

            canvasScreenDataSO = AddressablesManager.Instance.GetResource<CanvasScreenDataSO>("OverlayCanvasDataSO");
            canvasUIComp = new CanvasUIComp("OverlayCanvas",canvasScreenDataSO);
            linerComp = gameObject.AddComponent<LinerComp>(); 
            linerComp.Init(canvasUIComp.Canvas);
            
        }

        public void ActvieScreen(ScreenType _screenType,bool _isActive)
        {
            canvasUIComp.ActvieScreen(_screenType, _isActive);
        }
        public void UpdateLinesScale(ScreenType _screenType,Vector2 _scale)
        {
            canvasUIComp.UpdateScale(_screenType,_scale);
        }

        public Transform GetScreenTrm(ScreenType _type)
        {
            return canvasUIComp.GetCanvasContent(_type); 
        }
        
        public MapLiner CreateLine(ScreenType _screenType)
        {
            Transform _parent = canvasUIComp.GetCanvasContent(_screenType);
            var _line = linerComp.CreateLine(_screenType, _parent);
            return _line; 
        }

        public void DestroyLine(ScreenType _screenType)
        {
            linerComp.DestroyLine(_screenType);
        }

        public void UpdateLinesPos(ScreenType _screenType,Vector2 _pos)
        {
            linerComp.UpdateLinesPos(_screenType, _pos);
        }

 

    }
    
}

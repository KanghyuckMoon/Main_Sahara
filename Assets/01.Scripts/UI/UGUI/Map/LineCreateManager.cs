using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Utill.Pattern;
using Utill.Addressable;
using UI.Base;
using Pool; 

namespace UI.Canvas
{
    public record CanvasScreen
    {
        public RectTransform parent; // 최상위 부모 ( 처음 스크린 사이즈 초기화 ) 
        public RectTransform ctrlPanel; // 조작할 패널 
        public RectTransform sizeAnchor; // 조작할 패널 밑의 오브젝트( 확대 축소 사이즈 조정시 이 앵커를 기준으로 )
        public RectTransform moveAnchor; // 조작할 패널 밑의 오브젝트( 위치 조정시 이 앵커를 기준으로 )

        public CanvasScreen(RectTransform _parent)
        {
            this.parent = _parent;
            this.ctrlPanel = parent.Find("CtrlPanel").GetComponent<RectTransform>(); 
            this.sizeAnchor = ctrlPanel.Find("SizeAnchor").GetComponent<RectTransform>();
            this.moveAnchor = sizeAnchor.Find("MoveAnchor").GetComponent<RectTransform>();
        } 
    }
    public class LineCreateManager : MonoSingleton<LineCreateManager>
    {
        private CanvasUIComp canvasUIComp;
        private LinerComp linerComp; 
        
        public override void Awake()
        {
            base.Awake();

            canvasUIComp = new CanvasUIComp("LinerCanvas");
            linerComp = gameObject.AddComponent<LinerComp>(); 
            linerComp.Init(canvasUIComp.Canvas);
            //   GameObject _poolObj = AddressablesManager.Instance.GetResource<GameObject>("MapLiner");
            //   ObjectPoolManager.Instance.RegisterObject(lineAddress, _poolObj);
        }

        private void Update()
        {
            if (canvasUIComp.Canvas is not null)
            {
            }

            ;
        }

        public void ActvieScreen(ScreenType _screenType,bool _isActive)
        {
            canvasUIComp.ActvieScreen(_screenType, _isActive);
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

        public void UpdateLinesScale(ScreenType _screenType,Vector2 _scale)
        {
            canvasUIComp.UpdateScale(_screenType,_scale);
        }


    }
}


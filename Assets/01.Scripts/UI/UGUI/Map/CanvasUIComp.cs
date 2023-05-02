using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Base;

namespace UI.Canvas
{
    public class CanvasUIComp
    {
        private Transform canvas;

        private Dictionary<ScreenType, CanvasScreen> linerParentDic = new Dictionary<ScreenType, CanvasScreen>();

        private GameObject canvasScreenObj;
        private CanvasScreen canvasScreen;
        private CanvasScreenDataSO canvasScreenDataSO;

        // 캔버스 초기화시 호출할 함수 
        private Action initCanvasCallback = null;

        private readonly string canvasName;
        private bool isInit;

        public bool IsInit => isInit;

        public Transform Canvas
        {
            get
            {
                if (canvas == null)
                {
                    GameObject _obj = GameObject.FindWithTag(canvasName);
                    if (_obj == null) return null;
                    isInit = true;
                    canvas = _obj.transform;
                    initCanvasCallback?.Invoke();
                    InitLinerParent();
                    InitLinerParentSize();
                    InActiveAll();
                }

                return canvas;
            }
        }

        public CanvasUIComp(string _canvasName, CanvasScreenDataSO _canvasScreenDataSO = null)
        {
            canvasName = _canvasName;
            canvasScreenDataSO = _canvasScreenDataSO;
        }

        public Transform GetCanvasContent(ScreenType _screenType)
        {
            if (linerParentDic.Count is 0) return null;
            return linerParentDic[_screenType].moveAnchor;
        }

        public void ActvieScreen(ScreenType _screenType, bool _isActive)
        {
            if (linerParentDic.TryGetValue(_screenType, out CanvasScreen _lineParent))
            {
                _lineParent.parent.gameObject.SetActive(_isActive);
            }
        }

        public void UpdateScale(ScreenType _screenType, Vector2 _scale)
        {
            /*if (linerDic.ContainsKey(_screenType) == false) return;
            foreach (var _line in linerDic[_screenType])
            {
                _line.UpdateScale(_scale);
            }*/

            if (linerParentDic.ContainsKey(_screenType) == false) return;
            linerParentDic[_screenType].sizeAnchor.localScale = new Vector2(_scale.x, -_scale.y);
        }

        private void InitLinerParent()
        {
            if (Canvas is null || canvasScreenDataSO is null) return;

            linerParentDic.Clear();

            foreach (var screenType in canvasScreenDataSO.canvasScreenList)
            {
                string _name = Enum.GetName(typeof(ScreenType), screenType);
                linerParentDic.Add(screenType, new CanvasScreen(Canvas.Find(_name).GetComponent<RectTransform>()));
            }


            /*
            linerParentDic.Add(ScreenType.Upgrade,new CanvasScreen(Canvas.Find("Upgrade").GetComponent<RectTransform>()));
            linerParentDic.Add(ScreenType.Map,new CanvasScreen(Canvas.Find("Map").GetComponent<RectTransform>()));
            linerParentDic.Add(ScreenType.Inventory,new CanvasScreen(Canvas.Find("Inventory").GetComponent<RectTransform>()));
            linerParentDic.Add(ScreenType.Quest,new CanvasScreen(Canvas.Find("Quest").GetComponent<RectTransform>()));*/
        }

        private void InitLinerParentSize()
        {
            if (linerParentDic.ContainsKey(ScreenType.Upgrade) == true)
            {
                float _curH = linerParentDic[ScreenType.Upgrade].parent.sizeDelta.y;
                linerParentDic[ScreenType.Upgrade].parent.sizeDelta = new Vector2(Screen.width * 1f, _curH);
            }
        }

        private void InActiveAll()
        {
            foreach (var _line in linerParentDic.Values)
            {
                _line.parent.gameObject.SetActive(false);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Utill.Pattern;
using Utill.Addressable;
using UI.Base;
using Pool; 

namespace UI.MapLiner
{
    public record LineParent
    {
        public LineParent(RectTransform _parent)
        {
            this.parent = _parent;
            this.ctrlPanel = parent.Find("CtrlPanel").GetComponent<RectTransform>(); 
            this.sizeAnchor = ctrlPanel.Find("SizeAnchor").GetComponent<RectTransform>();
            this.moveAnchor = sizeAnchor.Find("MoveAnchor").GetComponent<RectTransform>();
        }
        public RectTransform parent; // 최상위 부모 ( 처음 스크린 사이즈 초기화 ) 
        public RectTransform ctrlPanel; // 조작할 패널 
        public RectTransform sizeAnchor; // 조작할 패널 밑의 오브젝트( 확대 축소 사이즈 조정시 이 앵커를 기준으로 )
        public RectTransform moveAnchor; // 조작할 패널 밑의 오브젝트( 위치 조정시 이 앵커를 기준으로 )
    }
    public class LineCreateManager : MonoSingleton<LineCreateManager>
    {
        private Transform canvas; 
        private MapLiner pLiner; // 라인 프리팹 

        private List<MapLiner> linerList = new List<MapLiner>();
        private Dictionary<ScreenType, List<MapLiner>> linerDic = new Dictionary<ScreenType, List<MapLiner>>();
        private Dictionary<ScreenType, LineParent> linerParentDic = new Dictionary<ScreenType, LineParent>();

        private const string lineAddress = "MapLiner"; 
        public Transform Canvas
        {
            get
            {
                if(canvas == null)
                {
                    GameObject _obj = GameObject.FindWithTag("LinerCanvas");
                    if (_obj == null) return null; 
                    canvas = _obj.transform;
                    InitLinerParent();
                    InitLinerParentSize();
                    InActiveAll(); 
                }
                return canvas; 
            }
        }

        public MapLiner Liner
        {
            get
            {
                if(pLiner ==null)
                {
                    pLiner = AddressablesManager.Instance.GetResource<GameObject>("MapLiner").GetComponent<MapLiner>();
                }
                return pLiner; 
            }
        }
        public override void Awake()
        {
            base.Awake();
            pLiner = AddressablesManager.Instance.GetResource<GameObject>("MapLiner").GetComponent<MapLiner>(); 
         //   GameObject _poolObj = AddressablesManager.Instance.GetResource<GameObject>("MapLiner");
         //   ObjectPoolManager.Instance.RegisterObject(lineAddress, _poolObj);
        }

        public void ActvieParent(ScreenType _screenType,bool _isActive)
        {
            
            if (linerParentDic.TryGetValue(_screenType, out LineParent _lineParent))
            {
                _lineParent.parent.gameObject.SetActive(_isActive);
            }
        }
        private void AddLineToDic(ScreenType _screenType, MapLiner _liner)
        {
            if(linerDic.ContainsKey(_screenType) == false)
            {
                this.linerDic[_screenType] = new List<MapLiner>(); 
            }
            this.linerDic[_screenType].Add(_liner);

        }
        public MapLiner CreateLine(ScreenType _screenType)
        {
              var _line = Instantiate(Liner, Canvas);
        //    var _line = ObjectPoolManager.Instance.GetObject(lineAddress).GetComponent<MapLiner>();
         //   _line.transform.SetParent(Canvas);
            AddLineToDic(_screenType, _line);
            // 부모 설정 
            _line.transform.SetParent(this.linerParentDic[_screenType].moveAnchor);
            return _line; 
        }

        public void DestroyLine(ScreenType _screenType)
        {
            if (linerDic.ContainsKey(_screenType) == false) return;
            foreach (var _line in linerDic[_screenType])
            {
                GameObject.Destroy(_line.gameObject);
              //  _line.gameObject.SetActive(false); 
              // ObjectPoolManager.Instance.RegisterObject(lineAddress, _line.gameObject);
            }
            linerDic[_screenType].Clear(); 
        }

        public void UpdateLinesPos(ScreenType _screenType,Vector2 _pos)
        {
            if (linerDic.ContainsKey(_screenType) == false) return;
            foreach(var _line in linerDic[_screenType])
            {
                _line.UpdatePos(_pos);
            }

//            if (linerParentDic.ContainsKey(_screenType) == false) return;
//            linerParentDic[_screenType].moveAnchor.anchoredPosition =  new Vector2(_pos.x  ,_pos.y);
            
        }

       

        public void UpdateLinesScale(ScreenType _screenType,Vector2 _scale)
        {
            
            /*if (linerDic.ContainsKey(_screenType) == false) return;
            foreach (var _line in linerDic[_screenType])
            {
                _line.UpdateScale(_scale);
            }*/
            
            if (linerParentDic.ContainsKey(_screenType) == false) return;
            linerParentDic[_screenType].sizeAnchor.localScale=  new Vector2(_scale.x  ,-_scale.y);
        }

        private void InitLinerParent()
        {
            if (Canvas is null) return;

            linerParentDic.Clear();
            linerParentDic.Add(ScreenType.Upgrade,new LineParent(Canvas.Find("Upgrade").GetComponent<RectTransform>()));
            linerParentDic.Add(ScreenType.Map,new LineParent(Canvas.Find("Map").GetComponent<RectTransform>()));
            linerParentDic.Add(ScreenType.Inventory,new LineParent(Canvas.Find("Inventory").GetComponent<RectTransform>()));
        }

        private void InitLinerParentSize()
        {
            if(linerParentDic.ContainsKey(ScreenType.Upgrade) == true)
            {
                float _curH = linerParentDic[ScreenType.Upgrade].parent.sizeDelta.y; 
                linerParentDic[ScreenType.Upgrade].parent.sizeDelta = new Vector2(Screen.width * 1f,_curH);
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


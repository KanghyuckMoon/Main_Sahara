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
    public class LineCreateManager : MonoSingleton<LineCreateManager>
    {
        private Transform canvas; 
        private MapLiner pLiner; // ∂Û¿Œ «¡∏Æ∆’ 

        private List<MapLiner> linerList = new List<MapLiner>();
        private Dictionary<ScreenType, List<MapLiner>> linerDic = new Dictionary<ScreenType, List<MapLiner>>();

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
            return _line; 
        }

        public void DestroyLine(ScreenType _screenType)
        {
            if (linerDic.ContainsKey(_screenType) == false) return;
            foreach (var _line in linerDic[_screenType])
            {
                Destroy(_line); 
              //  _line.gameObject.SetActive(false); 
               // ObjectPoolManager.Instance.RegisterObject(lineAddress, _line.gameObject);
            }
        }

        public void UpdateLinesPos(ScreenType _screenType,Vector2 _pos)
        {
            if (linerDic.ContainsKey(_screenType) == false) return;
            foreach(var _line in linerDic[_screenType])
            {
                _line.UpdatePos(_pos);
            }
        }

       

        public void UpdateLinesScale(ScreenType _screenType,Vector2 _scale)
        {
            
            if (linerDic.ContainsKey(_screenType) == false) return;
            foreach (var _line in linerDic[_screenType])
            {
                _line.UpdateScale(_scale);
            }
        }

    }
}


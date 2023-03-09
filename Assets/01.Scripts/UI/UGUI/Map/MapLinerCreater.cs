using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Utill.Pattern;
using Utill.Addressable;
using UI.Base; 

namespace UI
{
    public class MapLinerCreater : MonoSingleton<MapLinerCreater>
    {
        private Transform canvas; 
        private MapLiner pLiner; // 라인 프리팹 

        private List<MapLiner> linerList = new List<MapLiner>();
        private Dictionary<ScreenType, MapLiner> linerDic = new Dictionary<ScreenType, MapLiner>(); 
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
                    pLiner = AddressablesManager.Instance.GetResource<MapLiner>("MapLiner");
                }
                return pLiner; 
            }
        }
        public override void Awake()
        {
            base.Awake(); 
            pLiner = AddressablesManager.Instance.GetResource<MapLiner>("MapLiner");
        }
        public MapLiner CreateLine()
        {
            var _line = Instantiate(Liner, Canvas); 
            this.linerList.Add(_line);
            return _line; 
        }

        public void DestroyLine(MapLiner _liner)
        {
            var _line =linerList.Where((x) => x == _liner).First(); 
            // 삭제 
        }
    }
}


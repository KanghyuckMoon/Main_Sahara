using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using UI.Base;

namespace UI.Canvas
{
    public class LinerComp : MonoBehaviour
    {
        private MapLiner pLiner; // ∂Û¿Œ «¡∏Æ∆’ 

        private Dictionary<ScreenType, List<MapLiner>> linerDic = new Dictionary<ScreenType, List<MapLiner>>();

        private const string lineAddress = "MapLiner";

        
        public MapLiner Liner
        {
            get
            {
                if (pLiner == null)
                {
                    pLiner = AddressablesManager.Instance.GetResource<GameObject>(lineAddress).GetComponent<MapLiner>();
                }

                return pLiner;
            }
        }

        private void AddLineToDic(ScreenType _screenType, MapLiner _liner)
        {
            if (linerDic.ContainsKey(_screenType) == false)
            {
                this.linerDic[_screenType] = new List<MapLiner>();
            }

            this.linerDic[_screenType].Add(_liner);
        }

        public MapLiner CreateLine(ScreenType _screenType,Transform _parent)
        {
            MapLiner _line = Instantiate(Liner);
            if (_parent is not null)
            {
                _line.transform.SetParent(_parent);
            }
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
                GameObject.Destroy(_line.gameObject);
                //  _line.gameObject.SetActive(false); 
                // ObjectPoolManager.Instance.RegisterObject(lineAddress, _line.gameObject);
            }

            linerDic[_screenType].Clear();
        }

        public void UpdateLinesPos(ScreenType _screenType, Vector2 _pos)
        {
            if (linerDic.ContainsKey(_screenType) == false) return;
            foreach (var _line in linerDic[_screenType])
            {
                _line.UpdatePos(_pos);
            }

//            if (linerParentDic.ContainsKey(_screenType) == false) return;
//            linerParentDic[_screenType].moveAnchor.anchoredPosition =  new Vector2(_pos.x  ,_pos.y);
        }


    }
}
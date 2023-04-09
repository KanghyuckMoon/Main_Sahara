using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UI.Base;

namespace UI
{
    public class MapMarkerView : AbUI_Base
    {
        private VisualElement marker;
        public VisualElement Marker => marker; 
        enum Elements
        {
            marker
        }

        public MapMarkerView(VisualElement _v,Sprite _sprite)
        {
            InitUIParent(_v); 
            Init(); 
            marker.style.backgroundImage = new StyleBackground(_sprite); // 스프라이트 입히고 

        }
        public override void Init()
        {
            base.Init();
            BindVisualElements(typeof(Elements));
            marker = GetVisualElement((int)Elements.marker); 
        }

        public void SetPosAndRot(Vector2 _pos,Quaternion _rot)
        {
            float _rotV = _rot.eulerAngles.y; 
            marker.transform.position = _pos;
            marker.style.rotate = new StyleRotate(new Rotate(_rotV));  
        //    marker.style.left = new StyleLength(-pos.x);
        //    marker.style.top = new StyleLength(-pos.y);
        }
        public void SetPosAndRot(Vector2 _pos, Vector3 _rot)
        {
            marker.transform.position = _pos;
            marker.style.rotate = new StyleRotate(new Rotate(_rot.y));
        }
        public void SetPosAndRot(Vector2 _pos, float _rot)
        {
            marker.transform.position = _pos;
            marker.style.rotate = new StyleRotate(new Rotate(_rot));
        }


    }
}


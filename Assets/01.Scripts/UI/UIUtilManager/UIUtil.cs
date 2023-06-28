using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;

namespace UI.UtilManager
{
    public class UIUtil : MonoBehaviour
    {
        private static float width => Screen.width;
        private static float height => Screen.height; 
        public static void SendEvent(RadioButtonGroup _btn)
        {
            using (var e = new NavigationSubmitEvent() { target = _btn })
                _btn.SendEvent(e);
        }
        public static void SendEvent(RadioButton _btn)
        {
            using (var e = new NavigationSubmitEvent() { target = _btn })
                _btn.SendEvent(e);
        }
        public static void SendEvent(Button _btn)
        {
            using (var e = new NavigationSubmitEvent() { target = _btn })
                _btn.SendEvent(e);
        }
        
        public static string GetEnumStr(Type _eType, int _idx)
        {
            string[] _arr = Enum.GetNames(_eType);
            return _arr[_idx];
        }

        /// <summary>
        /// uitoolkit  ui의 중심 좌표를 가져온다 
        /// </summary>
        /// <param name="_v"></param>
        /// <returns></returns>
        public static Vector2 GetUICenterPos(VisualElement _v)
        {
            var _rect = _v.worldBound;

            return new Vector2(_rect.x + _rect.width / 2, _rect.y + _rect.height / 2);
        }

        /// <summary>
        /// UGUIPos to uitoolkit Pos
        /// </summary>
        /// <param name="_uguiPos"></param>
        /// <returns></returns>
        public static Vector2 GetUIToolkitPos(Vector2 _uguiPos)
        {
            return new Vector2(_uguiPos.x, height - _uguiPos.y);
        }

        /// <summary>
        /// 첫 번째 인자가 두 번째 인자 안에 있는가 
        /// </summary>
        /// <returns></returns>
        public static bool IsVectorInTarget(Vector2 _v1, VisualElement _target)
        {
            if (_v1.x > _target.worldBound.x && _v1.x < _target.worldBound.x + _target.worldBound.width
                                             && _v1.y > _target.worldBound.y &&
                                             _v1.y < _target.worldBound.y + _target.worldBound.height)
            {
                return true; 
            }

            return false; 
        }
        
        /// <summary>
        /// 리스트뷰 스크롤 스피드 설정 
        /// </summary>
        /// <param name="listView"></param>
        public static void FixListViewScrollingBug(ListView listView) {
#if UNITY_EDITOR
            var scroller = listView.Q<Scroller>();
            listView.RegisterCallback<WheelEvent>(@event => {
                scroller.value +=  @event.delta.y * 100;
                @event.StopPropagation();
            });
#else
            var scroller = listView.Q<Scroller>();
            listView.RegisterCallback<WheelEvent>(@event => {
                scroller.value -=  @event.delta.y * 10000;
                @event.StopPropagation();
            });
#endif
        }
    }

}

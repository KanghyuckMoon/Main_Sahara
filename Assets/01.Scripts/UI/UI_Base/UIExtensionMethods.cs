using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System;
using System.Reflection;

namespace UI.Base
{
    public static class UIExtensionMethods 
    {
        public static void GetClosedElement(this VisualElement _element,VisualElement[] _vList)
        {

        }
        public static void GetClosedElement<T>(this AbUI_Base _element, AbUI_Base[] _vList, string _name)
        {
            var _field = typeof(T).GetProperty(_name);
            var _value = _field.GetValue(_element) as VisualElement;

            List<VisualElement> _boundList = new List<VisualElement>(); 
            foreach(var _v in _vList)
            {
                var _e = _field.GetValue(_v) as VisualElement;
                _boundList.Add(_e); 
            }

            var _overlaps = _boundList.Where((x) => x.worldBound.Overlaps(_value.worldBound));
            var _closed = _overlaps.OrderBy((x) => Vector2.Distance(_value.worldBound.position, x.worldBound.position)).First(); 
            
        }
    }

}


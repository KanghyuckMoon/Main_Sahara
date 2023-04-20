using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.UGUIBase
{
    public class UGUI_Base : MonoBehaviour
    {
        Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

        protected void Bind<T>(Type type) where T : UnityEngine.Object
        {
            string[] names = Enum.GetNames(type);

            UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
            _objects.Add(typeof(T), objects);

            for (int i = 0; i < names.Length; i++)
            {
                if (typeof(T) == typeof(GameObject))
                    objects[i] = UGUUtil.FindChild(gameObject, names[i], true);
                else
                    objects[i] = UGUUtil.FindChild<T>(gameObject, names[i], true);
            }
        }

        protected T Get<T>(int idx) where T : UnityEngine.Object
        {
            UnityEngine.Object[] objects = null;
            if (_objects.TryGetValue(typeof(T), out objects) == false)
                return null;

            return objects[idx] as T;
        }

        protected Text GetText(int idx)
        {
            return Get<Text>(idx);
        }

        protected Button GetButton(int idx)
        {
            return Get<Button>(idx);
        }

        protected Image GetImage(int idx)
        {
            return Get<Image>(idx);
        }

        public static void AddUIEvent(GameObject go, Action<PointerEventData> action,
            UGUIDefine.UIEvent type = UGUIDefine.UIEvent.Click)
        {
            UGUI_EventHandler evt = UGUUtil.GetOrAddComponent<UGUI_EventHandler>(go);

            switch (type)
            {
                case UGUIDefine.UIEvent.Click:
                    evt.OnClickHandler -= action;
                    evt.OnClickHandler += action;
                    break;
                case UGUIDefine.UIEvent.Drag:
                    evt.OnDragHandler -= action;
                    evt.OnDragHandler += action;
                    break;
            }
        }
    }
}
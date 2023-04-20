using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.UGUIBase
{
    public class UGUI_EventHandler : MonoBehaviour, IDragHandler, IPointerClickHandler
    {
        public Action<PointerEventData> OnClickHandler = null;
        public Action<PointerEventData> OnDragHandler = null;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnClickHandler != null)
                OnClickHandler.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (OnDragHandler != null)
                OnDragHandler.Invoke(eventData);
        }
    }
    
}

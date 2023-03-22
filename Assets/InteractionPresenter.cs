using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UI.ConstructorManager;

namespace UI.Popup
{   
    public class InteractionUIData
    {
        public Vector3 targetVec; // vec3 위치 
        public string textStr; // ui에 나타날 텍스트
    }
    
    public class InteractionPresenter : IPopup
    {
        private InteracftionPopupView interacftionPopupView;

        private VisualElement parent;

        private MapInfo mapInfo; 
       // 프로퍼티 
        public VisualElement Parent { get; }
        
        public InteractionPresenter()
        {
            var _prod = UIConstructorManager.Instance.GetProductionUI(typeof(InteracftionPopupView));
            parent = _prod.Item1; 
            interacftionPopupView = _prod.Item2 as InteracftionPopupView;
            ;
            mapInfo = new MapInfo(); 
        }
        public void ActiveTween()
        {
            
        }

        public void InActiveTween()
        {
        }

        public void Undo()
        {
            
        }

        public void SetData(object _data)
        {
            InteractionUIData _uiData = _data as InteractionUIData;
            ;
            Vector2 _uiPos = mapInfo.WorldToUIPos(_uiData.targetVec);
            interacftionPopupView.ParentElement.transform.position = _uiPos;
            
        }

    }   
}

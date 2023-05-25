using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UI.ConstructorManager;
using GoogleSpreadSheet;

namespace UI.Popup
{   
    public class InteractionUIData
    {
        public Vector3 targetVec; // vec3 위치 
        public string textKey; // ui에 나타날 텍스트
        public string textTypeKey; // 어떤 상호작용인지 표시해줄 텍스트 키 예) 대화하기 
    }
    
    public class InteractionPresenter : IPopup
    {
        private InteracftionPopupView interacftionPopupView;

        private VisualElement parent;

        private MapInfo mapInfo;

        private Camera cam; 
       // 프로퍼티 
       public VisualElement Parent => parent; 
        
        public InteractionPresenter()
        {
            var _prod = UIConstructorManager.Instance.GetProductionUI(typeof(InteracftionPopupView));
            parent = _prod.Item1; 
            interacftionPopupView = _prod.Item2 as InteracftionPopupView;
            
            mapInfo = new MapInfo(); 
            interacftionPopupView.InteractionParent.AddToClassList("inactive");

            cam = Camera.main; 
        }
        public void ActiveTween()
        {
            interacftionPopupView.InteractionParent.RemoveFromClassList("inactive");
            interacftionPopupView.InteractionParent.AddToClassList("active");
        }

        public void InActiveTween()
        {
            interacftionPopupView.InteractionParent.RemoveFromClassList("active");
            interacftionPopupView.InteractionParent.AddToClassList("inactive");
        }

        public void Undo()
        {
            interacftionPopupView.ParentElement.RemoveFromHierarchy();
        }

        public void SetData(object _data)
        {
            
            //Vector2 _uiPos 
            try
            {
                InteractionUIData _uiData = _data as InteractionUIData;
                
                Rect rect = RuntimePanelUtils.CameraTransformWorldToPanelRect(Parent.panel, _uiData.targetVec
                    ,new Vector2(10,10) ,cam);
                interacftionPopupView.ParentElement.transform.position = rect.position;

                string _detail = TextManager.Instance.GetText(_uiData.textKey);
                interacftionPopupView.SetDetail(_detail);
            }
            catch (Exception e)
            {
                cam = Camera.current;
            }
        }

    }   
}

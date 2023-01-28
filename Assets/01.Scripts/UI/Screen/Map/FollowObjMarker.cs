using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{

    /// <summary>
    /// 월드맵 오브젝트가 미니맵상으로 표시 
    /// </summary>
    public class FollowObjMarker : MonoBehaviour
    {
        #region 나중에 어드레서블로 스프라이트 가져올시 사용 
        enum MarkerType
        {
            player,

        }
        #endregion

        [SerializeField]
        private VisualTreeAsset markerUxml;
        [SerializeField]
        private Sprite sprite;

        private MapPresenter mapPresenter;
        private MapMarkerView markerView; 
        private VisualElement markerUI;

        private bool isUpdatePos = true; 

        // 프로퍼티 
        public VisualElement MarkerUI => markerUI; 
        private MapView MapView => mapPresenter.MapView;
        private MapInfo MapInfo => mapPresenter.MapInfo;
        public bool IsUpdatePos => isUpdatePos; 
        private void Start()
        {
            this.mapPresenter = GameObject.FindGameObjectWithTag("UIParent").GetComponentInChildren<ScreenUIController>().MapPresenter;
            CreateMarker(); 

        }

        private void Update()
        {
            if (isUpdatePos == false) return; 
            SetMarkerPosAndRot(); 
        }

        [ContextMenu("마커 생성")]
        /// <summary>
        /// 맵UI상에서 마커UI 생성 
        /// </summary>
        public void CreateMarker()
        {
            TemplateContainer _markerContainter = markerUxml.Instantiate();
            //markerUI = _markerContainter.contentContainer.children(); // 생성 

            markerView = new MapMarkerView(_markerContainter,sprite); //스프라이트, 위치 설정 
            //_mapMarkerView.Init();
            SetMarkerPosAndRot(); // 위치 설정 

            markerUI = markerView.Marker;
            MapView.MarkerParent.Add(markerUI); // 자식으로 추가 

            // 현재오브젝트 위치 -> UI
            

        }

        [ContextMenu("마커 삭제")]
        /// <summary>
        /// 맵UI상에서 마커UI 삭제 
        /// </summary>
        public void RemoveMarker()
        {
            if (markerUI == null)
            {
                Debug.LogError("현재 마커UI가 초기화 되어 있지 않습니다 ");
                return;
            }
            MapView.MarkerParent.Remove(markerUI);
        }

        public void SetMarkerPosAndRot()
        {
            Vector2 _uiPos = MapInfo.WorldToUIPos(transform.position);
            markerView.SetPosAndRot(_uiPos,transform.rotation);
        }
    }
}


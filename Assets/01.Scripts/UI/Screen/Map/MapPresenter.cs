using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    /// <summary>
    /// 전체맵 활성화시 이동, 확대축소, 마커 기능 
    /// </summary>
    public class MapPresenter : MonoBehaviour, IScreen
    {
        // 인스펙터 참조 
        [SerializeField]
        private UIDocument uiDocument;
        [SerializeField]
        private MapView mapView;

        [SerializeField]
        private FullMapComponent fullMapComponent;
        [SerializeField]
        private MIniMapComponent miniMapComponent; 

        // 프로퍼티
        public MapView MapView => mapView;
        public MapInfo MapInfo => miniMapComponent.MapInfo; 

        private void Awake()
        {
            uiDocument ??= GetComponent<UIDocument>(); 

            mapView.InitUIDocument(uiDocument);
        }
        private void OnEnable()
        {
            mapView.Cashing();
            mapView.Init();
        }
        private void Start()
        {
            fullMapComponent.Init(mapView);
            miniMapComponent.Init(mapView); 
        }

        private void LateUpdate()
        {
            if (mapView.CurMapType == MapType.FullMap)
            {
                // 전체맵 렌더링
                fullMapComponent.UpdateUI();
            }
            else if (mapView.CurMapType == MapType.MiniMap)
            {
                // 미니맵 렌더링
                miniMapComponent.UpdateUI();
            }
        }

        public void ActiveView()
        {
            mapView.ShowMap(); 
        }
        public void ActiveView(bool _isActive)
        {
            mapView.ShowMap(_isActive); 
        }

        [ContextMenu("마커 활성화")]
        public void ActiveMarkers()
        {
            fullMapComponent.MarkersComponent.ActiveMarkers(true);
        }

        [ContextMenu("마커 비활성화")]
        public void DisableMarkers()
        {
            fullMapComponent.MarkersComponent.ActiveMarkers(false);
        }


    }
}



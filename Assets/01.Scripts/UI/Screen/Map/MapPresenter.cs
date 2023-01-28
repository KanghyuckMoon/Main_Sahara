using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    /// <summary>
    /// ��ü�� Ȱ��ȭ�� �̵�, Ȯ�����, ��Ŀ ��� 
    /// </summary>
    public class MapPresenter : MonoBehaviour, IScreen
    {
        // �ν����� ���� 
        [SerializeField]
        private UIDocument uiDocument;
        [SerializeField]
        private MapView mapView;

        [SerializeField]
        private FullMapComponent fullMapComponent;
        [SerializeField]
        private MIniMapComponent miniMapComponent; 

        // ������Ƽ
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
                // ��ü�� ������
                fullMapComponent.UpdateUI();
            }
            else if (mapView.CurMapType == MapType.MiniMap)
            {
                // �̴ϸ� ������
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

        [ContextMenu("��Ŀ Ȱ��ȭ")]
        public void ActiveMarkers()
        {
            fullMapComponent.MarkersComponent.ActiveMarkers(true);
        }

        [ContextMenu("��Ŀ ��Ȱ��ȭ")]
        public void DisableMarkers()
        {
            fullMapComponent.MarkersComponent.ActiveMarkers(false);
        }


    }
}



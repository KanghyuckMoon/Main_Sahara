using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Measurement;
using UI.Base; 

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
        public IUIController UIController { get; set; }
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

            fullMapComponent.Init(mapView);
            miniMapComponent.Init(mapView);

        }

        private void OnDisable()
        {
            
        }
        private void Start()
        {
        }

        private void LateUpdate()
        {
            //Logging.Log("MarkerParent Scale : " + mapView.MarkerParent.transform.scale);
            //Logging.Log("MarkerParent Origin : " + mapView.MarkerParent.resolvedStyle.transformOrigin);
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
            //if(Input.GetKeyDown(KeyCode.Alpha2))
            //{
            //    mapView.Test(); 
            //}
            //if (Input.GetKeyDown(KeyCode.Alpha3))
            //{
            //    mapView.Test2();
            //}
        }

        public void Active(bool _isActive)
        {
            mapView.ActiveScreen(_isActive);
        }
        public bool ActiveView()
        {
            //mapView.ShowMap();
            StartCoroutine(TestCo());
            // StartCoroutine(T()); 
            // mapView.ShowMap();
            // if (mapView.CurMapType == MapType.MiniMap)
            // {
            //     StartCoroutine(Test());
            // } 
            if (MapView.CurMapType == MapType.FullMap)
            {
                this.fullMapComponent.ActivePath();
            }
            else
            {
                this.fullMapComponent.ClearLines();

            }
            return MapView.CurMapType == MapType.FullMap ? true : false;  
        }
        public void ActiveView(bool _isActive)
        {
            mapView.ShowMap(_isActive);
           // StartCoroutine(TestCo());

            if (_isActive == false) 
            { 
            }
                //StartCoroutine(Test());
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

        IEnumerator TestCo()
        {
            if (mapView.ShowMap() == true) yield break; 
            
            mapView.Test2();
            yield return null;
            mapView.Test();
            mapView.SetMask(true);
        }

    }
}



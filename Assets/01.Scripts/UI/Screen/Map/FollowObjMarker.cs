using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Addressable; 

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

        [SerializeField]
        private Transform camTrm;
        private MapPresenter mapPresenter;
        private MapMarkerView markerView;
        private VisualElement markerUI;
        private VisualElement sightUI; 

        [SerializeField, Header("동적 이동 회전 여부")]
        private bool isUpdatePosAndRot = true;
        [SerializeField,Header("플레이어 앞 방향에 부채꼴모양 이미지 ")]
        private bool isPlayerMarker;

        // 프로퍼티 
        public VisualElement MarkerUI => markerUI;
        private MapView MapView => mapPresenter.MapView;
        private MapInfo MapInfo => mapPresenter.MapInfo;
        private Transform CamTrm
        {
            get
            {
                if (camTrm == null)
                {
                    camTrm = Camera.main.transform;
                }
                return camTrm;
            }
        }

        public bool IsUpdatePos => isUpdatePosAndRot;

        private void Start()
        {
            this.mapPresenter = GameObject.FindGameObjectWithTag("UIParent").GetComponentInChildren<ScreenUIController>().MapPresenter;
            CreateMarker();
            

        }

        private void Update()
        {
            if (isUpdatePosAndRot == false) return;
            SetMarkerPosAndRot();

            if (isPlayerMarker == true)
                UpdateSightUI(); 
        }

        [ContextMenu("마커 생성")]
        /// <summary>
        /// 맵UI상에서 마커UI 생성 
        /// </summary>
        private void CreateMarker()
        {
            if (isPlayerMarker == true)
            {
                // 생성 
                sightUI = AddressablesManager.Instance.GetResource<VisualTreeAsset>("Sight").Instantiate();
                MapView.MarkerParent.Add(sightUI);
                sightUI.style.top = -75f; 
                //markerUI.Add(sightUI); 
            }

            TemplateContainer _markerContainter = markerUxml.Instantiate();
            //markerUI = _markerContainter.contentContainer.children(); // 생성 

            markerView = new MapMarkerView(_markerContainter, sprite); //스프라이트, 위치 설정 
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

        private void SetMarkerPosAndRot()
        {
            Vector2 _uiPos = MapInfo.WorldToUIPos(transform.position);
            markerView.SetPosAndRot(_uiPos, 0);
        }
        private void UpdateSightUI()
        {
            sightUI.transform.position = markerUI.transform.position; 
        }

        public void SetMarkerPosAndRot(float _rot)
        {
            Vector2 _uiPos = MapInfo.WorldToUIPos(transform.position);
            markerView.SetPosAndRot(_uiPos,/* CamTrm.eulerAngles.y -*/ transform.eulerAngles.y + _rot);
            sightUI.style.rotate = new Rotate(CamTrm.eulerAngles.y);
            //markerView.SetPosAndRot(_uiPos, transform.eulerAngles.y - CamTrm.eulerAngles.y + _rot);
            //sightUI.style.rotate = new Rotate(_rot);
        }
    }
}


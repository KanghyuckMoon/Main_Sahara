using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Utill.Addressable; 

namespace UI
{

    /// <summary>
    /// ����� ������Ʈ�� �̴ϸʻ����� ǥ�� 
    /// </summary>
    public class FollowObjMarker : MonoBehaviour
    {
        #region ���߿� ��巹����� ��������Ʈ �����ý� ��� 
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

        [SerializeField, Header("���� �̵� ȸ�� ����")]
        private bool isUpdatePosAndRot = true;
        [SerializeField,Header("�÷��̾� �� ���⿡ ��ä�ø�� �̹��� ")]
        private bool isPlayerMarker;

        // ������Ƽ 
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

        [ContextMenu("��Ŀ ����")]
        /// <summary>
        /// ��UI�󿡼� ��ĿUI ���� 
        /// </summary>
        private void CreateMarker()
        {
            if (isPlayerMarker == true)
            {
                // ���� 
                sightUI = AddressablesManager.Instance.GetResource<VisualTreeAsset>("Sight").Instantiate();
                MapView.MarkerParent.Add(sightUI);
                sightUI.style.top = -75f; 
                //markerUI.Add(sightUI); 
            }

            TemplateContainer _markerContainter = markerUxml.Instantiate();
            //markerUI = _markerContainter.contentContainer.children(); // ���� 

            markerView = new MapMarkerView(_markerContainter, sprite); //��������Ʈ, ��ġ ���� 
            //_mapMarkerView.Init();
            SetMarkerPosAndRot(); // ��ġ ���� 

            markerUI = markerView.Marker;
            MapView.MarkerParent.Add(markerUI); // �ڽ����� �߰� 

            // ���������Ʈ ��ġ -> UI


        }

        [ContextMenu("��Ŀ ����")]
        /// <summary>
        /// ��UI�󿡼� ��ĿUI ���� 
        /// </summary>
        public void RemoveMarker()
        {
            if (markerUI == null)
            {
                Debug.LogError("���� ��ĿUI�� �ʱ�ȭ �Ǿ� ���� �ʽ��ϴ� ");
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


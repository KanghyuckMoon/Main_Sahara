using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

        private MapPresenter mapPresenter;
        private MapMarkerView markerView; 
        private VisualElement markerUI;

        private bool isUpdatePos = true; 

        // ������Ƽ 
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

        [ContextMenu("��Ŀ ����")]
        /// <summary>
        /// ��UI�󿡼� ��ĿUI ���� 
        /// </summary>
        public void CreateMarker()
        {
            TemplateContainer _markerContainter = markerUxml.Instantiate();
            //markerUI = _markerContainter.contentContainer.children(); // ���� 

            markerView = new MapMarkerView(_markerContainter,sprite); //��������Ʈ, ��ġ ���� 
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

        public void SetMarkerPosAndRot()
        {
            Vector2 _uiPos = MapInfo.WorldToUIPos(transform.position);
            markerView.SetPosAndRot(_uiPos,transform.rotation);
        }
    }
}


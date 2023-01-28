using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class MapController : MonoBehaviour
    {
        [SerializeField]
        private MapMarkerSpriteSO markerSpriteSO;

        [SerializeField]
        private MapData mapData;

        [SerializeField]
        private Sprite tempMarkerSprite; // �ӽ� ��Ŀ sprite 
        [SerializeField]
        private GameObject tempUIMarkerPrefab; // �ӽ� UI��Ŀ 
        [SerializeField]
        private GameObject tempMarkerObj;  // �ӽ� ������Ʈ ��Ŀ 

        [Header("Ȱ��ȭ ����")]
        [SerializeField]
        private bool isSceneMarker; // ����󿡼� ��Ŀ Ȱ��ȭ�� ���� 
        [SerializeField]
        private bool isUIMarker; // UI�󿡼� ��Ŀ Ȱ��ȭ�� ����

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // ��Ŀ ���� 
                CreateMarker();
            }
        }

        private void Start()
        {
            InitMapData();
        }

        public void CreateMarker()
        {
            GameObject _marker = Instantiate(tempUIMarkerPrefab, mapData.mapTrm);
            _marker.transform.position = new Vector3(mapData.selectTrm.position.x, mapData.selectTrm.position.y, mapData.mapUITrm.position.z);

            // �� UI ��ġ�� ����� ��ġ�� �ٲٱ� 
            var tempPos2 = Vector3.zero;
            var tempPos1 = _marker.transform.position;
            tempPos2.x = tempPos1.x * mapData.sceneSize.x / mapData.mapSize.x;
            tempPos2.y = 0;
            tempPos2.z = (tempPos1.y - mapData.mapUITrm.position.y) * mapData.sceneSize.y / mapData.mapSize.y;

            // ���� ������Ʈ 
            GameObject markerObj = Instantiate(tempMarkerObj, mapData.sceneTrm);
            markerObj.transform.position = tempPos2 + mapData.sceneMapPoint;

            //UI ������Ʈ 

        }

        public void ActiveSceneMarkers(bool isActive)
        {

        }

        public void ActiveUIMarkers(bool isActive)
        {

        }

        private void InitMapData()
        {
            mapData.sceneSize.x = mapData.sceneMaxPoint.position.x - mapData.sceneMinPoint.position.x;
            mapData.sceneSize.y = mapData.sceneMaxPoint.position.z - mapData.sceneMinPoint.position.z;
            mapData.sceneMapPoint = (mapData.sceneMaxPoint.position + mapData.sceneMinPoint.position) / 2;

            mapData.mapSize.x = mapData.mapMaxPoint.position.x - mapData.mapMinPoint.position.x;
            mapData.mapSize.y = mapData.mapMaxPoint.position.y - mapData.mapMinPoint.position.y;

        }
    }

}


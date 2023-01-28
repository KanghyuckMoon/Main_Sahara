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
        private Sprite tempMarkerSprite; // 임시 마커 sprite 
        [SerializeField]
        private GameObject tempUIMarkerPrefab; // 임시 UI마커 
        [SerializeField]
        private GameObject tempMarkerObj;  // 임시 오브젝트 마커 

        [Header("활성화 여부")]
        [SerializeField]
        private bool isSceneMarker; // 월드상에서 마커 활성화할 건지 
        [SerializeField]
        private bool isUIMarker; // UI상에서 마커 활성화할 건지

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // 마커 생성 
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

            // 맵 UI 위치를 월드맵 위치로 바꾸기 
            var tempPos2 = Vector3.zero;
            var tempPos1 = _marker.transform.position;
            tempPos2.x = tempPos1.x * mapData.sceneSize.x / mapData.mapSize.x;
            tempPos2.y = 0;
            tempPos2.z = (tempPos1.y - mapData.mapUITrm.position.y) * mapData.sceneSize.y / mapData.mapSize.y;

            // 월드 오브젝트 
            GameObject markerObj = Instantiate(tempMarkerObj, mapData.sceneTrm);
            markerObj.transform.position = tempPos2 + mapData.sceneMapPoint;

            //UI 오브젝트 

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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

namespace UI
{
    /// <summary>
    /// 플레이어 마커 표시, 
    /// </summary>
    [Serializable]
    public class MIniMapComponent
    {
        // 인스펙터 참조 
        [SerializeField]
        private MapInfo mapInfo;

        [SerializeField]
        private FollowObjMarker playerMarker; // 일단 임시로, 나중에 플레이어 가져와서 할거야  

        // 프라이빗 변수 
        private GameObject player;
        private MapView mapView;
        private Camera cam;

        // 프로퍼티 
        public MapInfo MapInfo => mapInfo;
        private GameObject Player
        {
            get
            {
                player ??= GameObject.FindWithTag("Player");
                return player;
            }
        }
        public FollowObjMarker PlayerMarker
        {
            get
            {
                if (playerMarker == null && Player != null)
                {
                    playerMarker = Player.GetComponentInChildren<FollowObjMarker>();
                }
                return playerMarker;
            }
        }
        public Transform CamTrm
        {
            get
            {
                cam ??= Camera.main;
                
                if (cam != null) return cam.transform;
                return null; 
            }
        }

        public void Init(MapView _mapView)
        {
            this.mapView = _mapView;
            // MapInfo 초기화 

            mapInfo.UIMapSize = new Vector2(mapView.Map.style.width.value.value, mapView.Map.style.height.value.value);
            mapInfo.UIMapCenterPos = new Vector2(mapView.Map.style.width.value.value / 2, mapView.Map.style.height.value.value / 2);
            mapInfo.sceneSize.x = mapInfo.MaxScenePos.x - mapInfo.MinScenePos.x;
            mapInfo.sceneSize.y = mapInfo.MaxScenePos.y - mapInfo.MinScenePos.y;
        }

        public void UpdateUI()
        {
            // 플레이어 마커 가져와서 
            // 그에 맞게 맵 이돟 
            // Vector2 _playerMarkerPos = new Vector2(playerMarker.MarkerUI.style.left.value.value, playerMarker.MarkerUI.style.top.value.value);

            if (PlayerMarker == null) return; 
            Vector2 _playerMarkerPos = PlayerMarker.MarkerUI.transform.position; 
            Vector2 _mapPos; // 맵 포지션 
            _mapPos.x = Mathf.Clamp(mapView.Map.style.width.value.value * 0.5f - (_playerMarkerPos.x + mapInfo.UIMapCenterPos.x),
              -mapInfo.UIMapSize.x * 0.5f + mapView.MinimapMaskW * 0.5f,
              mapInfo.UIMapSize.x * 0.5f - mapView.MinimapMaskW * 0.5f);
            _mapPos.y = Mathf.Clamp(mapView.Map.style.height.value.value * 0.5f - (_playerMarkerPos.y + mapInfo.UIMapCenterPos.y),
                                                       -mapInfo.UIMapSize.y * 0.5f + mapView.MinimapMaskH * 0.5f,
                                                        mapInfo.UIMapSize.y * 0.5f - mapView.MinimapMaskH * 0.5f);
            Debug.Log("mapPos" + _mapPos);
            Debug.Log("mapTrans" + mapView.Map.transform.position);

            Debug.Log("maskWorldBound" + mapView.MinimapMask.worldBound);
            Debug.Log("maskLocalBound" + mapView.MinimapMask.localBound);
            Debug.Log("maskContentRect" + mapView.MinimapMask.contentRect);
            Debug.Log("maskLayout" + mapView.MinimapMask.layout);
            Debug.Log("maskWidth" + mapView.MinimapMask.style.width);
            Debug.Log("maskHeight" + mapView.MinimapMask.style.height);
            //mapView.Map.style.left = new Length(_mapPos.x);
            //mapView.Map.style.top = new Length(_mapPos.y);
           //mapView.MapTrm.position = Vector2.Lerp(mapView.MapTrm.position,_mapPos,Time.deltaTime * 20f); 
            mapView.MapTrm.position = _mapPos;

            // 맵 회전 
            RotateMap(); 
        }

        private float prevMapAngle = 0f;
        private float angleDiff = 0f; 
        /// <summary>
        /// 카메라 회전값에 따른 미니맵 회전 
        /// </summary>
        private void RotateMap()
        {
            if (CamTrm == null) return; 
            prevMapAngle = mapView.MinimapMask.style.rotate.value.angle.value;

            mapView.MinimapMask.style.rotate = new Rotate(new Angle(-CamTrm.eulerAngles.y));
            angleDiff = mapView.MinimapMask.style.rotate.value.angle.value - prevMapAngle;
            var _markers = mapView.MarkerParent.Children();
            foreach (var m in _markers)
            {
                if (m == PlayerMarker.MarkerUI) continue;
                //  m.style.rotate = new Rotate(new Angle(m.style.rotate.value.angle.value - angleDiff)); // 마커는 방향 유지해야하닌 
            }

            if(PlayerMarker != null)
            {
                PlayerMarker.SetMarkerPosAndRot(-mapView.MinimapMask.style.rotate.value.angle.value);
            }

        }
        // 마커UI가 오브젝트 따라가는거 (플레이어, 몬스터)
        // obj -> UI 
        // mapView 가져와서 밑으로 가야해 

        // 마커 UI 움직임에 따라 맵 이동하는거 (플레이어) 
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

namespace UI
{
    /// <summary>
    /// �÷��̾� ��Ŀ ǥ��, 
    /// </summary>
    [Serializable]
    public class MIniMapComponent
    {
        // �ν����� ���� 
        [SerializeField]
        private MapInfo mapInfo;

        [SerializeField]
        private FollowObjMarker playerMarker; // �ϴ� �ӽ÷�, ���߿� �÷��̾� �����ͼ� �Ұž�  

        // �����̺� ���� 
        private GameObject player;
        private MapView mapView;
        private Camera cam;

        // ������Ƽ 
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
            // MapInfo �ʱ�ȭ 

            mapInfo.UIMapSize = new Vector2(mapView.Map.style.width.value.value, mapView.Map.style.height.value.value);
            mapInfo.UIMapCenterPos = new Vector2(mapView.Map.style.width.value.value / 2, mapView.Map.style.height.value.value / 2);
            mapInfo.sceneSize.x = mapInfo.MaxScenePos.x - mapInfo.MinScenePos.x;
            mapInfo.sceneSize.y = mapInfo.MaxScenePos.y - mapInfo.MinScenePos.y;
        }

        public void UpdateUI()
        {
            // �÷��̾� ��Ŀ �����ͼ� 
            // �׿� �°� �� �̉� 
            // Vector2 _playerMarkerPos = new Vector2(playerMarker.MarkerUI.style.left.value.value, playerMarker.MarkerUI.style.top.value.value);

            if (PlayerMarker == null) return; 
            Vector2 _playerMarkerPos = PlayerMarker.MarkerUI.transform.position; 
            Vector2 _mapPos; // �� ������ 
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

            // �� ȸ�� 
            RotateMap(); 
        }

        private float prevMapAngle = 0f;
        private float angleDiff = 0f; 
        /// <summary>
        /// ī�޶� ȸ������ ���� �̴ϸ� ȸ�� 
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
                //  m.style.rotate = new Rotate(new Angle(m.style.rotate.value.angle.value - angleDiff)); // ��Ŀ�� ���� �����ؾ��ϴ� 
            }

            if(PlayerMarker != null)
            {
                PlayerMarker.SetMarkerPosAndRot(-mapView.MinimapMask.style.rotate.value.angle.value);
            }

        }
        // ��ĿUI�� ������Ʈ ���󰡴°� (�÷��̾�, ����)
        // obj -> UI 
        // mapView �����ͼ� ������ ������ 

        // ��Ŀ UI �����ӿ� ���� �� �̵��ϴ°� (�÷��̾�) 
    }

}

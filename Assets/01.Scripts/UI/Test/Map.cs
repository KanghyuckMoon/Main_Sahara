using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

namespace Aiden
{
    public class Map : MonoBehaviour
    {
        public bool enableMap = true;

        public EMapMode MapMod;

        public GameMapData mapInfo = new GameMapData();

        public Transform player;
        public RectTransform playerIcon;

        private void Start()
        {
            MapDataInitialization(mapInfo);

            mapInfo.GameStartMod = MapMod;

            MapSwitch(mapInfo.GameStartMod);
        }

        private void Update()
        {
            if (enableMap)
            {
                mapInfo.mapCanvasRect.gameObject.SetActive(true);
                if (player != null)
                {
                    MapUpdate(player);
                }
                else
                {
                    return;
                }
            }
            else
            {
                mapInfo.mapCanvasRect.gameObject.SetActive(false);
            }


            if (mapInfo.GameStartMod != MapMod)
            {
                mapInfo.switchMapType = true;
            }
        }


        #region init



        /// <summary>
        /// Coordinate initialization
        /// 좌표 초기화
        /// </summary>
        /// <param name="mapData"></param>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public void MapDataInitialization(GameMapData mapData)
        {
            // 최대 최소 좌표 설정 
            if (mapData.sceneMax != null && mapData.sceneMin != null)
            {
                mapData.sceneMaxV3 = mapData.sceneMax.position;
                mapData.sceneMinV3 = mapData.sceneMin.position;
            }

            // 씬 사이즈, 로컬 좌표 설정 
            if (mapData.sceneMaxV3 != null && mapData.sceneMinV3 != null)
            {
                mapData.sceneSize.y = mapData.sceneMaxV3.z - mapData.sceneMinV3.z;
                mapData.sceneSize.x = mapData.sceneMaxV3.x - mapData.sceneMinV3.x;
                mapData.sceneMapPoint = (mapData.sceneMaxV3 + mapData.sceneMinV3) / 2;
            }
            else
            {
                Debug.LogWarning("<color=#ff0800>OH SHIT NO SET MAP POSITION!!!</color>");
                return;
            }


            foreach (Transform child in this.gameObject.GetComponentsInChildren<Transform>(true))
            {
                if (mapData.mapImage != null && mapData.maskRect != null && mapData.mapCanvasRect != null)
                {
                    break;
                }

                switch (child.gameObject.name)
                {
                    // 미니맵 이미지 설정
                    case "map":
                        mapData.mapRect = child.GetComponent<RectTransform>();
                        mapData.mapImage = child.GetComponent<Image>();
                        if (mapData.mapSprite == null)
                        {
                            mapData.mapImage.sprite = mapData.mapSprite[0];
                            child.GetComponent<Image>().SetNativeSize(); // ?
                        }
                        break;
                    
                     // 마스크 설정
                    case "mask":
                        mapData.maskRect = child.GetComponent<RectTransform>();
                        break;
                    //
                    case "GameMap":
                        mapData.mapCanvasRect = child.GetComponent<RectTransform>();
                        if (child.TryGetComponent(out CanvasGroup group))
                        {
                            //mapData.mapGroupAlpha = group;
                        }

                        //mapData.mapGroupAlpha = child.GetComponent<CanvasGroup>();
                        break;
                }
            }

            // 미니맵이나 전체맵으로 설정 
            MapSwitch(mapInfo.GameStartMod);

            if (mapInfo.GameStartMod == EMapMode.EBigMap)
            {
                mapInfo.maskRect.localRotation = Quaternion.Euler(0, 0, 0);
                mapInfo.zoomValue = 1;
            }
        }

        #endregion


        #region MapFunction

        public void ZoomButtonClick(float num)
        {
            if (mapInfo != null)
            {
                mapInfo.zoomValue += num;
            }
        }




        /// <summary>
        /// 맵 업데이트(프레젠테이션 계층)
        /// </summary>
        /// <param name="PlayerPos"></param>
        public void MapUpdate(Transform PlayerPos)
        {
            if (mapInfo != null)
            {
                if(Input.GetKeyDown(KeyCode.Q))
                {
                    CreateMarker(mapInfo);
                }
                // 플레이어 위치에 따라갈거면 
                if (mapInfo.TrackPlayers)
                {
                    // 
                    MapPosTrackTargetPos(mapInfo, PlayerPos.position);
                    // 플레이어 아이콘 위치 업데이트 
                    IconPosSet(mapInfo, PlayerPos, playerIcon); 
                }

                if (mapInfo.switchMapType)
                {
                    if (mapInfo.GameStartMod == EMapMode.EMiniMap)
                    {
                        MapSwitch(mapInfo.GameStartMod = EMapMode.EBigMap);
                    }
                    else if (mapInfo.GameStartMod == EMapMode.EBigMap)
                    {
                        MapSwitch(mapInfo.GameStartMod = EMapMode.EMiniMap);
                    }

                    mapInfo.switchMapType = false;
                    MapMod = mapInfo.GameStartMod;
                }

                if (mapInfo.CanZoomMap)
                {
                    GameMapUtilities.MapZoomMultiply(mapInfo.maskRect, mapInfo.mapRect, mapInfo.zoomValue,
                        mapInfo.zoomMax, mapInfo.zoomMin);
                }


                if (mapInfo.SpinMap)
                {
                    IconSpin(mapInfo.maskRect, -PlayerPos.eulerAngles.y);
                }

                if (mapInfo.floor.Length > 1)
                {
                    MapImageSwitch(mapInfo.floor, mapInfo.mapSprite, PlayerPos, mapInfo.mapImage);
                }
            }
        }


        /// <summary>
        /// 지도 모드 전환
        /// </summary>
        /// <param name="nowMode"></param>
        public void MapSwitch(EMapMode nowMode)
        {
            switch (nowMode)
            {
                case EMapMode.EBigMap:
                    mapInfo.mapCanvasRect.anchorMax = mapInfo.BigMapAnchorPos;
                    mapInfo.mapCanvasRect.anchorMin = mapInfo.BigMapAnchorPos;
                    mapInfo.mapCanvasRect.pivot = mapInfo.BigMapAnchorPos;
                    mapInfo.mapCanvasRect.sizeDelta = mapInfo.BigMapSize;
                    mapInfo.mapCanvasRect.anchoredPosition = mapInfo.BigMapPos;
                    mapInfo.maskRect.localRotation = Quaternion.Euler(0, 0, 0);
                    mapInfo.zoomValue = mapInfo.BigMapScale;
                    break;
                case EMapMode.EMiniMap:
                    mapInfo.mapCanvasRect.anchorMax = mapInfo.MiniMapAnchorPos;
                    mapInfo.mapCanvasRect.anchorMin = mapInfo.MiniMapAnchorPos;
                    mapInfo.mapCanvasRect.pivot = mapInfo.MiniMapAnchorPos;
                    mapInfo.mapCanvasRect.sizeDelta = mapInfo.MiniMapSize;
                    mapInfo.mapCanvasRect.anchoredPosition = mapInfo.MiniMapPos;
                    mapInfo.zoomValue = mapInfo.MiniMapScale;
                    break;
            }
        }

        /// <summary>
        /// Icon Spin
        /// 아이콘 회전
        /// </summary>
        /// <param name="icon"></param>
        /// <param name="angle"></param>
        public void IconSpin(RectTransform icon, float angle = 0f)
        {
            var temp_Spin_Value = new Vector3();
            temp_Spin_Value.x = 0;
            temp_Spin_Value.y = 0;
            temp_Spin_Value.z = angle;
            icon.localRotation = Quaternion.Euler(temp_Spin_Value);
        }


        /// <summary>
        /// Icon Track Target position 
        /// 아이콘 위치
        /// </summary>
        /// <param name="scene_Long"></param>
        /// <param name="scene_High"></param>
        /// <param name="scene_Map_Point"></param>
        /// <param name="map"></param>
        /// <param name="player"></param>
        /// <param name="playerIcon"></param>
        public void IconPosSet(GameMapData mapData, Transform player, RectTransform playerIcon)
        {
            var temp_player_pos_1 = new Vector3();
            var temp_player_pos_2 = player.position - mapData.sceneMapPoint;

            // 월드 좌표를 현재 미니맵 UI 위치로 바꾸기
            temp_player_pos_1.x = Mathf.Clamp((temp_player_pos_2.x / mapData.sceneSize.x * mapData.mapRect.rect.width),
                -mapData.mapRect.rect.width / 2, mapData.mapRect.rect.width / 2);
            
            temp_player_pos_1.y = Mathf.Clamp((temp_player_pos_2.z / mapData.sceneSize.y * mapData.mapRect.rect.height),
                -mapData.mapRect.rect.height / 2, mapData.mapRect.rect.height / 2);
          
            playerIcon.localPosition = temp_player_pos_1;
        }

        [SerializeField]
        private GameObject iconPrefab;
        [SerializeField]
        private GameObject marker;
        [SerializeField]
        private GameObject canvas; 
        public void CreateMarker(GameMapData mapData)
        {
            GameObject marker = Instantiate(iconPrefab, mapData.mapRect);
            marker.GetComponent<RectTransform>().anchoredPosition = playerIcon.anchoredPosition;

            var tempPos1 = marker.transform.localPosition;
            tempPos1.x = tempPos1.x * mapData.sceneSize.x * mapData.mapRect.rect.width;
            tempPos1.y = tempPos1.y * mapData.sceneSize.y * mapData.mapRect.rect.height;
            // 맵 UI 위치를 월드맵 위치로 바꾸기 

            GameObject markerObj = Instantiate(marker, canvas.transform);
            markerObj.transform.position = tempPos1;
       

        }

        /// <summary>
        /// Map Track Target position 
        /// 지도 포지셔닝
        /// </summary>
        /// <param name="scene_Long"></param>
        /// <param name="scene_High"></param>
        /// <param name="scene_Map_Point"></param>
        /// <param name="player"></param>
        /// <param name="map"></param>
        /// <param name="map_Mask"></param>
        public void MapPosTrackTargetPos(GameMapData mapData, Vector3 player)
        {
            var temp_map_pos = new Vector3();
            var temp_player_pos_2 = player - mapData.sceneMapPoint; // 플레이어 위치 - 로컬 맵 좌표 
            
            temp_map_pos.x =
                Mathf.Clamp((-temp_player_pos_2.x / mapData.sceneSize.x * mapData.mapRect.rect.width),
                    -((mapData.mapRect.rect.width / 2) - (mapData.maskRect.rect.width / 2)),
                    (mapData.mapRect.rect.width / 2) - (mapData.maskRect.rect.width / 2));
            
            temp_map_pos.y =
                Mathf.Clamp((-temp_player_pos_2.z / mapData.sceneSize.y * mapData.mapRect.rect.height),
                    -((mapData.mapRect.rect.height / 2) - (mapData.maskRect.rect.height / 2)),
                    (mapData.mapRect.rect.height / 2) - (mapData.maskRect.rect.height / 2));
       
            mapData.mapRect.localPosition = temp_map_pos;
        }

        /// <summary>
        /// 맵 텍스처 전환
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="imagelist"></param>
        /// <param name="player"></param>
        /// <param name="map_Image"></param>
        public void MapImageSwitch(float[] floor, Sprite[] imagelist, Transform player, Image map_Image)
        {
            for (int i = floor.Length - 1; i > -1; i--)
            {
                if (player.position.y >= floor[i])
                {
                    map_Image.sprite = imagelist[i];
                    break;
                }
            }
        }

        #endregion

        /// <summary>
        /// 이미지 재설정
        /// </summary>
        /// <param name="resetRect"></param>
        public void RectReset(RectTransform resetRect)
        {
            resetRect.localRotation = new Quaternion(0f, 0f, 0f, 1f);
            resetRect.localPosition = Vector3.zero;
            resetRect.localScale = Vector3.one;
            resetRect.anchorMin = new Vector2(0.5f, 0.5f);
            resetRect.anchorMax = new Vector2(0.5f, 0.5f);
            resetRect.anchoredPosition = Vector2.zero;
            resetRect.sizeDelta = new Vector2(100f, 100f);
            resetRect.pivot = new Vector2(0.5f, 0.5f);
        }
    }


    public class GameMapUtilities
    {
        #region Map Function

        /// <summary>
        /// Map zoom(Addition)
        /// 스케일링(가산)
        /// </summary>
        /// <param name="map_Mask"></param>
        /// <param name="map"></param>
        /// <param name="_offset"></param>
        public static void MapZoomAddition(RectTransform map_Mask, RectTransform map, float _offset)
        {
            var temp_Zoom_Value = new Vector3();
            temp_Zoom_Value.x = map.rect.width + _offset;
            temp_Zoom_Value.y = map.rect.height + _offset;
            map.sizeDelta = temp_Zoom_Value;
        }

        public static void MapZoomAddition(RectTransform map_Mask, RectTransform map, float _offset, float _maxMulitple,
            float _minMulitple = 1f)
        {
            var temp_Zoom_Value = new Vector3();
            temp_Zoom_Value.x = Mathf.Clamp(map.rect.width + _offset, map_Mask.rect.width * _minMulitple,
                map_Mask.rect.width * _maxMulitple);
            temp_Zoom_Value.y = Mathf.Clamp(map.rect.height + _offset, map_Mask.rect.height * _minMulitple,
                map_Mask.rect.height * _maxMulitple);
            map.sizeDelta = temp_Zoom_Value;
        }

        /// <summary>
        /// Map zoom(Multiply)
        /// 스케일링(곱셈)
        /// </summary>
        /// <param name="map_Mask"></param>
        /// <param name="map"></param>
        /// <param name="_offset"></param>
        public static void MapZoomMultiply(RectTransform map_Mask, RectTransform map, float _offset)
        {
            var temp_Zoom_Value = new Vector3();
            temp_Zoom_Value.x = map_Mask.rect.width * _offset;
            temp_Zoom_Value.y = map_Mask.rect.height * _offset;
            map.sizeDelta = temp_Zoom_Value;
        }

        public static void MapZoomMultiply(RectTransform map_Mask, RectTransform map, float _offset, float _maxMulitple,
            float _minMulitple = 1f)
        {
            var temp_Zoom_Value = new Vector3();
            temp_Zoom_Value.x =
                Mathf.Clamp(map_Mask.rect.width * _offset, map_Mask.rect.width * _minMulitple,
                    map_Mask.rect.width * _maxMulitple);
            temp_Zoom_Value.y =
                Mathf.Clamp(map_Mask.rect.height * _offset, map_Mask.rect.height * _minMulitple,
                    map_Mask.rect.height * _maxMulitple);
            map.sizeDelta = temp_Zoom_Value;
        }

        /// <summary>
        /// 모바일 지도
        /// </summary>
        /// <param name="mapData"></param>
        /// <param name="moveTemp"></param>
        public static void MapMove(GameMapData mapData, Vector3 moveTemp)
        {
            mapData.mapRect.localPosition += moveTemp;
        }

        #endregion
    }


    [Serializable]
    public class GameMapData
    {
        public Transform sceneMax;
        public Vector3 sceneMaxV3;
        public Transform sceneMin;
        public Vector3 sceneMinV3;

        [Range(1, 10)] public float zoomMax = 5f;
        [Range(1, 10)] public float zoomMin = 1f;
        private float _zoomValue = 2.5f; //스케일 값

        public float zoomValue
        {
            get { return _zoomValue; }
            set { _zoomValue = Mathf.Clamp(value, zoomMin, zoomMax); }
        }

        public bool CanZoomMap = true;
        public bool SpinMap;
        public bool TrackPlayers = true;

        [HideInInspector] public Vector2 sceneSize; //장면 맵의 실제 크기(초기 값을 할당할 필요 없음, sceneMax 및 sceneMin에 따라 계산됨)
        [HideInInspector] public Vector3 sceneMapPoint; //로컬 장면 중심의 좌표(초기 값을 할당할 필요 없음, sceneMax 및 sceneMin에 따라 계산됨)
        [HideInInspector] public RectTransform maskRect; //맵 마스크 좌표
        [HideInInspector] public RectTransform mapRect; //지도 배경 좌표
        [HideInInspector] public RectTransform mapCanvasRect; //map obj(프리팹 아님)
        [HideInInspector] public Image mapImage; //지도 객체

        [HideInInspector] public EMapMode GameStartMod = 0;
        [HideInInspector] public bool switchMapType;

        //플레이어가 지도 바닥을 구성하도록 합니다.
        public float[] floor;

        //플레이어가 맵 이미지를 구성하도록 합니다.
        public Sprite[] mapSprite;

        //플레이어가 총 아이콘을 구성하도록 합니다.
        //public List<Sprite> Icon;
        //public List<RectTransform> IconPos;

        // 작은 지도 화면 앵커 위치
        public Vector2 MiniMapAnchorPos = new Vector2(1f, 1f);

        //큰 지도 화면의 앵커 위치
        public Vector2 BigMapAnchorPos = new Vector2(0.5f, 0.5f);

        //미니맵 위치
        public Vector2 MiniMapPos = new Vector2(0, 0);

        //큰 지도 위치
        public Vector2 BigMapPos = new Vector2(0, 0);

        //작은 맵 크기
        public Vector2 MiniMapSize;

        //큰 지도 크기
        public Vector2 BigMapSize;

        //작은 지도의 기본 줌 비율
        public float MiniMapScale = 2f;

        //빅맵 기본 줌 비율
        public float BigMapScale = 1f;

        //public float alpha = 1;

        //[HideInInspector] public CanvasGroup mapGroupAlpha;
    }

    [Serializable]
    public enum EMapMode
    {
        EMiniMap = 0,
        EBigMap = 1
    }
}   
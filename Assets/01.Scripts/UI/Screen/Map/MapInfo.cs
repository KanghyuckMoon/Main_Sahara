using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UI
{
    /// <summary>
    /// 맵에 대한 정보( 월드맵 사이즈, UI맵 사이즈 등 )
    /// </summary>
    [Serializable]
    public class MapInfo
    {
        [Header("UI")]
        // 초기화 받을 것
        //[HideInInspector]
        public Vector2 minUIMapPos;
        //[HideInInspector]
        public Vector2 maxUIMapPos;
        //[HideInInspector]
        public Vector2 UIMapSize;
        //[HideInInspector]
        public Vector2 UIMapCenterPos; // UI 중심 좌표 
        [Header("실제 맵")]

        public Transform minScenePos;
        public Transform maxScenePos;
        //[HideInInspector]
        public Vector2 sceneSize;

        // 월드맵 상에서 오브젝트로 표시될경우 
        public Transform markerParent;

        /// <summary>
        /// 월드 포지션으로 UI 포지션으로( absolute 기준) 
        /// </summary>
        /// <param name="_worldPos"></param>
        /// <returns></returns>
        public Vector2 WorldToUIPos(Vector3 _worldPos)
        {
            // uxml의 width /2 , height / 2를 더해줘야해 
            Vector2 _uiPos;
            _uiPos.x = Mathf.Clamp((_worldPos.x /*+ sceneSize.x * 0.5f*/) / sceneSize.x * UIMapSize.x,
                                                    -UIMapSize.x * 0.5f, UIMapSize.x * 0.5f);
            _uiPos.y = Mathf.Clamp(-(_worldPos.z/* + sceneSize.y * 0.5f*/) / sceneSize.y * UIMapSize.y,
                                                    -UIMapSize.y * 0.5f, UIMapSize.y * 0.5f);

            return _uiPos;
        }
    }
}


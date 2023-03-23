using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UI
{
    /// <summary>
    /// 맵에 대한 정보( 월드맵 사이즈, UI맵 사이즈 등 )
    /// </summary>
  //  [CreateAssetMenu(menuName ="SO/UI/MapInfoSO")]
    public class MapInfo //: ScriptableObject
    {
        [Header("UI")]
        public Vector2 UIMapSize = new Vector2(8000,8000); // 8000,8000
        public Vector2 UIMapCenterPos => new Vector2(UIMapSize.x * 0.5f, UIMapSize.y * 0.5f); // UI 중심 좌표 

        [Header("실제 맵")]
        public Transform minScenePos;
        public Transform maxScenePos;

        public Vector2 MinScenePos
        {
            get
            {
                if (minScenePos == null)
                    return new Vector2(-500, -500);
                return new Vector2(minScenePos.position.x,minScenePos.position.z);
            }
        }
        public Vector2 MaxScenePos
        {
            get
            {
                if (maxScenePos == null)
                    return new Vector2(500, 500);
                return new Vector2(maxScenePos.position.x, maxScenePos.position.z);
            }
        }
        private Vector2 sceneSize = Vector2.zero; 
        //[HideInInspector]
        public Vector2 SceneSize
        {
            get
            {
                if(sceneSize == Vector2.zero)
                {
                    sceneSize = new Vector2
                        (
                            MaxScenePos.x - MinScenePos.x, 
                            MaxScenePos.y - MinScenePos.y
                        );
                }
                return sceneSize; 
            }
        }

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
            _uiPos.x = Mathf.Clamp((_worldPos.x /*+ sceneSize.x * 0.5f*/) / SceneSize.x * UIMapSize.x,
                                                    -UIMapSize.x * 0.5f, UIMapSize.x * 0.5f);
            _uiPos.y = Mathf.Clamp(-(_worldPos.z/* + sceneSize.y * 0.5f*/) / SceneSize.y * UIMapSize.y,
                                                    -UIMapSize.y * 0.5f, UIMapSize.y * 0.5f);

            return _uiPos;
        }
    }
}


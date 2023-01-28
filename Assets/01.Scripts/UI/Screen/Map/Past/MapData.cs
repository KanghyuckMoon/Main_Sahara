using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace UI
{
    [Serializable]
    public class MapData
    {
        [Header("월드 관련")]
        // 월드 관련 
        public Transform sceneTrm; // 오브젝트 부모 

        public Transform sceneMinPoint;
        public Transform sceneMaxPoint;

        [HideInInspector]
        public Vector3 sceneMapPoint; // 씬 로컬 좌표 
        [HideInInspector]
        public Vector3 sceneSize;

        [Header("맵UI 관련")]
        // 맵 관련 '
        public Transform mapTrm; // 맵 지점 
        public Transform selectTrm; // 선택 지점 
        public Transform mapUITrm; // 맵 스프라이트가 있는 트랜스폼 (나중에 get으로

        public Transform mapMinPoint;
        public Transform mapMaxPoint;

        [HideInInspector]
        public Vector2 mapSize;
    }
}

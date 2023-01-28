using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UI
{
    /// <summary>
    /// �ʿ� ���� ����( ����� ������, UI�� ������ �� )
    /// </summary>
    [Serializable]
    public class MapInfo
    {
        [Header("UI")]
        // �ʱ�ȭ ���� ��
        //[HideInInspector]
        public Vector2 minUIMapPos;
        //[HideInInspector]
        public Vector2 maxUIMapPos;
        //[HideInInspector]
        public Vector2 UIMapSize;
        //[HideInInspector]
        public Vector2 UIMapCenterPos; // UI �߽� ��ǥ 
        [Header("���� ��")]

        public Transform minScenePos;
        public Transform maxScenePos;
        //[HideInInspector]
        public Vector2 sceneSize;

        // ����� �󿡼� ������Ʈ�� ǥ�õɰ�� 
        public Transform markerParent;

        /// <summary>
        /// ���� ���������� UI ����������( absolute ����) 
        /// </summary>
        /// <param name="_worldPos"></param>
        /// <returns></returns>
        public Vector2 WorldToUIPos(Vector3 _worldPos)
        {
            // uxml�� width /2 , height / 2�� ��������� 
            Vector2 _uiPos;
            _uiPos.x = Mathf.Clamp((_worldPos.x /*+ sceneSize.x * 0.5f*/) / sceneSize.x * UIMapSize.x,
                                                    -UIMapSize.x * 0.5f, UIMapSize.x * 0.5f);
            _uiPos.y = Mathf.Clamp(-(_worldPos.z/* + sceneSize.y * 0.5f*/) / sceneSize.y * UIMapSize.y,
                                                    -UIMapSize.y * 0.5f, UIMapSize.y * 0.5f);

            return _uiPos;
        }
    }
}


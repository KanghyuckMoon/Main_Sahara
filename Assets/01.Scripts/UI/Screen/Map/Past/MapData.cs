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
        [Header("���� ����")]
        // ���� ���� 
        public Transform sceneTrm; // ������Ʈ �θ� 

        public Transform sceneMinPoint;
        public Transform sceneMaxPoint;

        [HideInInspector]
        public Vector3 sceneMapPoint; // �� ���� ��ǥ 
        [HideInInspector]
        public Vector3 sceneSize;

        [Header("��UI ����")]
        // �� ���� '
        public Transform mapTrm; // �� ���� 
        public Transform selectTrm; // ���� ���� 
        public Transform mapUITrm; // �� ��������Ʈ�� �ִ� Ʈ������ (���߿� get����

        public Transform mapMinPoint;
        public Transform mapMaxPoint;

        [HideInInspector]
        public Vector2 mapSize;
    }
}

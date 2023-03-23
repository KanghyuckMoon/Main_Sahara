using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using UI; 

namespace PathMode
{
    public class PathModeManager : MonoSingleton<PathModeManager>
    {
        public Transform Player
        {
            get
            {
                if (player is null)
                {
                    player = GameObject.FindGameObjectWithTag("Player")?.transform;
                }

                return player;
            }
            set
            {
                player = value;
            }
        }
        private Transform player;

        private Vector3 lastPos = Vector3.zero;

        private MapInfo mapInfo = new MapInfo();

        private const float updateDistance = 16f; //4*4, 4m마다 업데이트

        public PathSave pathSave = new PathSave();

        private void Update()
        {
            Vector3 _lastPos = lastPos;
            _lastPos.y = 0;

            Vector3 _playerPos = Player.position;
            _playerPos.y = 0;

            if ((_lastPos - _playerPos).sqrMagnitude > updateDistance)
            {
                AddPath(_playerPos);
                lastPos = Player.position;
            }
        }

        private void AddPath(Vector3 pos)
        {
            Vector2 _pathPos = WorldToUIPos(pos);
            pathSave.pathList.Add(_pathPos);
            if(pathSave.pathList.Count > 200)
			{
                pathSave.pathList.RemoveAt(0);
            }
        }

        /// <summary>
        /// 월드 포지션으로 UI 포지션으로( absolute 기준) 
        /// </summary>
        /// <param name="_worldPos"></param>
        /// <returns></returns>
        public Vector2 WorldToUIPos(Vector3 _worldPos)
        {
            return mapInfo.WorldToUIPos(_worldPos);
        }

        public List<Vector2> GetPathList()
        {
            return pathSave.pathList;
        }

        public void SetPathList()
        {
            lastPos = Player.position;
        }
    }

    [System.Serializable]
    public class PathSave
    {
        public List<Vector2> pathList = new List<Vector2>();
    }

}
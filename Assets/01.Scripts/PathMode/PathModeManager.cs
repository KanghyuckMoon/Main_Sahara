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
            
        public PathSave pathSave = new PathSave();

        private void Update()
        {
            if((lastPos - Player.position).sqrMagnitude > 4f)
            {
                AddPath(Player.position);
                lastPos = Player.position;
            }
        }

        private void AddPath(Vector3 pos)
        {
            Vector2 _pathPos = WorldToUIPos(pos);
            pathSave.pathList.Add(_pathPos);
        }

        /// <summary>
        /// ���� ���������� UI ����������( absolute ����) 
        /// </summary>
        /// <param name="_worldPos"></param>
        /// <returns></returns>
        public Vector2 WorldToUIPos(Vector3 _worldPos)
        {
            //Vector2 _uiPos;
            //_uiPos.x = Mathf.Clamp((_worldPos.x /*+ sceneSize.x * 0.5f*/) / mapInfo.SceneSize.x * mapInfo.UIMapSize.x,
            //                                        -mapInfo.UIMapSize.x * 0.5f, mapInfo.UIMapSize.x * 0.5f);
            //_uiPos.y = Mathf.Clamp(-(_worldPos.z/* + sceneSize.y * 0.5f*/) / mapInfo.SceneSize.y * mapInfo.UIMapSize.y,
            //                                        -mapInfo.UIMapSize.y * 0.5f, mapInfo.UIMapSize.y * 0.5f);

            //return _uiPos;

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
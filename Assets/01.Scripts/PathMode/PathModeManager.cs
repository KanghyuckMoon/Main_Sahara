using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

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

        public PathSave pathSave = new PathSave();

        private void Update()
        {
            if((lastPos - Player.position).sqrMagnitude < 4f)
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
        /// 월드 포지션으로 UI 포지션으로( absolute 기준) 
        /// </summary>
        /// <param name="_worldPos"></param>
        /// <returns></returns>
        public Vector2 WorldToUIPos(Vector3 _worldPos)
        {
            Vector2 _uiPos;
            _uiPos.x = Mathf.Clamp((_worldPos.x /*+ sceneSize.x * 0.5f*/) / 50 * 50,
                                                    -50 * 0.5f, 50 * 0.5f);
            _uiPos.y = Mathf.Clamp(-(_worldPos.z/* + sceneSize.y * 0.5f*/) / 50 * 50,
                                                    -50 * 0.5f, 50 * 0.5f);

            return _uiPos;
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
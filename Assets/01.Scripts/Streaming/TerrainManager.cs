using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Utill.Pattern;

namespace Streaming
{
    public class TerrainManager : MonoSingleton<TerrainManager>
    {
        private Dictionary<string, Terrain> terrainActiveDic = new Dictionary<string, Terrain>() ;

        public void EnableTerrain(Terrain _terrain, string _name)
        {
            Vector3 _origin = StreamingUtill.StringToVector3(_name);
            if (terrainActiveDic.ContainsKey(_name))
            {
                terrainActiveDic[_name] = _terrain;
            }
            else
            {
                terrainActiveDic.Add(_name, _terrain);
            }

            SetConnect();
            
            Debug.Log("Terrain Enalbe : " + _name);
        }

        public void SetConnect()
        {
            foreach (var _terrain in terrainActiveDic)
            {
                if (_terrain.Value == null)
                {
                    continue;
                }
                
                Vector3 _origin = StreamingUtill.StringToVector3(_terrain.Key);
                string _left = StreamingUtill.Vector3ToString(_origin + new Vector3(-1, 0 ,0));
                string _right = StreamingUtill.Vector3ToString(_origin + new Vector3(1, 0 ,0));
                string _up = StreamingUtill.Vector3ToString(_origin + new Vector3(0, 1 ,0));
                string _down = StreamingUtill.Vector3ToString(_origin + new Vector3(0, -1, 0));
                
                _terrain.Value.SetNeighbors(GetTerrain( _left), GetTerrain( _up), GetTerrain( _right), GetTerrain( _down));
            }
        }

        private Terrain GetTerrain(string key)
        {
            if (terrainActiveDic.ContainsKey(key))
            {
                return terrainActiveDic[key];
            }

            return null;
        }
    
        public void DisableTerrain(string _name)
        {
            if (terrainActiveDic.ContainsKey(_name))
            {
                terrainActiveDic[_name] = null;
            }
            else
            {
                terrainActiveDic.Add(_name, null);
            }
            SetConnect();
        }

        public bool CheckTerrain(string _name)
        {
            if (!terrainActiveDic.ContainsKey(_name))
            {
                return false;
            }
            else
            {
                if (terrainActiveDic[_name] == null)
                {
                    return false;
                }
                return true;
            }
        }
    }
}

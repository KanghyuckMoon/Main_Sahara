using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

namespace Streaming
{
    public class TerrainManager : MonoSingleton<TerrainManager>
    {
        private Dictionary<string, bool> terrainActiveDic = new Dictionary<string, bool>() ;

        public void EnableTerrain(string _name)
        {
            if (terrainActiveDic.ContainsKey(_name))
            {
                terrainActiveDic[_name] = true;
            }
            else
            {
                terrainActiveDic.Add(_name, true);
            }
        }
    
        public void DisableTerrain(string _name)
        {
            if (terrainActiveDic.ContainsKey(_name))
            {
                terrainActiveDic[_name] = false;
            }
            else
            {
                terrainActiveDic.Add(_name, false);
            }
        }

        public bool CheckTerrain(string _name)
        {
            if (!terrainActiveDic.ContainsKey(_name))
            {
                return false;
            }
            else
            {
                return terrainActiveDic[_name];
            }
        }
    }
}

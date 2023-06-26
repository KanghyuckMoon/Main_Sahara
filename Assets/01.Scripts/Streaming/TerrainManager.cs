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

            //SetConnect();
            
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
                string _up = StreamingUtill.Vector3ToString(_origin + new Vector3(0, 0 ,1));
                string _down = StreamingUtill.Vector3ToString(_origin + new Vector3(0, 0, -1));

                Terrain _leftTerrain = GetTerrain(_left);
                Terrain _upTerrain = GetTerrain(_up);
                Terrain _rightTerrain = GetTerrain(_right);
                Terrain _downTerrain = GetTerrain(_down);
                
                _terrain.Value.SetNeighbors(_leftTerrain, _upTerrain, _rightTerrain, _downTerrain);
                
                if (_downTerrain != null)
                {
                    StitchTerrainsDown(_terrain.Value, _downTerrain);   
                }
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
        
        public void StitchTerrainsDown(Terrain terrain1, Terrain terrain2)
        {
            TerrainData data1 = terrain1.terrainData;
            TerrainData data2 = terrain2.terrainData;

            int resolution = data1.heightmapResolution;

            // Get the heightmap data from the bottom row of terrain1
            float[,] edgeValues1 = data1.GetHeights(0, resolution - 1, resolution, 1);

            // Get the heightmap data from the top row of terrain2
            float[,] edgeValues2 = data2.GetHeights(0, 0, resolution, 1);

            // Average the heights of the overlapping edge
            //for (int x = 0; x < resolution; x++)
            //{
            //    edgeValues1[x, 0] = (edgeValues1[x, 0] + edgeValues2[x, 0]) / 2f;
            //}

            // Set the modified edge values back to the terrains
            data1.SetHeights(0, resolution - 1, edgeValues1);
            data2.SetHeights(0, 0, edgeValues1);
        }
        public void StitchTerrainsLeft(Terrain terrain1, Terrain terrain2)
        {
            TerrainData data1 = terrain1.terrainData;
            TerrainData data2 = terrain2.terrainData;

            int resolution = data1.heightmapResolution;

            // Get the heightmap data from the leftmost column of terrain1
            float[,] edgeValues1 = data1.GetHeights(0, 0, 1, resolution);

            // Get the heightmap data from the rightmost column of terrain2
            float[,] edgeValues2 = data2.GetHeights(resolution - 1, 0, 1, resolution);

            // Average the heights of the overlapping edge
            for (int y = 0; y < resolution; y++)
            {
                edgeValues1[0, y] = (edgeValues1[0, y] + edgeValues2[0, y]) / 2f;
            }

            // Set the modified edge values back to the terrains
            data1.SetHeights(0, 0, edgeValues1);
            data2.SetHeights(resolution - 1, 0, edgeValues1);
        }
    }
}

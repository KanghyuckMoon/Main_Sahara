using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Utill.Pattern;

namespace Streaming
{
    public class TerrainManager : MonoSingleton<TerrainManager>
    {
        private Dictionary<string, Terrain> terrainActiveDic = new Dictionary<string, Terrain>();

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

        [ContextMenu("SetConnect")]
        public void SetConnect()
        {
            foreach (var _terrain in terrainActiveDic)
            {
                if (_terrain.Value == null)
                {
                    continue;
                }

                Vector3 _origin = StreamingUtill.StringToVector3(_terrain.Key);
                string _left = StreamingUtill.Vector3ToString(_origin + new Vector3(-1, 0, 0));
                string _right = StreamingUtill.Vector3ToString(_origin + new Vector3(1, 0, 0));
                string _up = StreamingUtill.Vector3ToString(_origin + new Vector3(0, 0, 1));
                string _down = StreamingUtill.Vector3ToString(_origin + new Vector3(0, 0, -1));

                Terrain _leftTerrain = GetTerrain(_left);
                Terrain _upTerrain = GetTerrain(_up);
                Terrain _rightTerrain = GetTerrain(_right);
                Terrain _downTerrain = GetTerrain(_down);

                //_terrain.Value.SetNeighbors(_leftTerrain, _upTerrain, _rightTerrain, _downTerrain);
                
                //SetEdgeHeight(_terrain.Value, 4500);
                Debug.Log(_terrain.Key);
                //if (_leftTerrain != null)
                //{
                //    SetEdgeHeightLeft(_terrain.Value, _leftTerrain);
                //}  
                //if (_rightTerrain != null)
                //{
                //    SetEdgeHeightRight(_terrain.Value, _rightTerrain);
                //}
                if (_upTerrain != null)
                {
                    SetEdgeHeightUp(_terrain.Value, _upTerrain);
                }  
                //if (_downTerrain != null)
                //{
                //    SetEdgeHeightDown(_terrain.Value, _downTerrain);
                //}  
            }
        }

        public void SetEdgeHeight(Terrain terrain, float height)
        {
            TerrainData data = terrain.terrainData;
            int resolution = data.heightmapResolution;

            float[,] heightmap = data.GetHeights(0, 0, resolution, resolution);

            // Set the height of the left and right edges
            for (int y = 0; y < resolution; y++)
            {
                heightmap[y, 0] = height;
                heightmap[y, resolution - 1] = height;
            }

            // Set the height of the top and bottom edges
            for (int x = 0; x < resolution; x++)
            {
                heightmap[0, x] = height;
                heightmap[resolution - 1, x] = height;
            }

            // Set the modified heightmap back to the terrain
            data.SetHeights(0, 0, heightmap);
        }
        
        public void SetEdgeHeightUp(Terrain terrain, Terrain upTerrain)
        {
            TerrainData data = terrain.terrainData;
            TerrainData data2 = upTerrain.terrainData;
            int resolution1 = data.heightmapResolution;
            int resolution2 = data2.heightmapResolution;

            float[,] heightmap = data.GetHeights(0, 0, resolution1, resolution1);
            float[,] heightmap2 = data2.GetHeights(0, 0, resolution2, resolution2);

            for (int x = 0; x < resolution1; x++)
            {
                heightmap[0, x] = heightmap[4, x];
                heightmap[1, x] = heightmap[4, x];

                heightmap2[resolution2 - 1, x] = heightmap[4, x];
                heightmap2[resolution2 - 2, x] = heightmap[4, x];
            }
            
            // Set the modified heightmap back to the terrain
            data.SetHeights(0, 0, heightmap);
            data2.SetHeights(0, 0, heightmap2);
        }
        
        public void SetEdgeHeightDown(Terrain terrain, Terrain downTerrain)
        {
            TerrainData data = terrain.terrainData;
            TerrainData data2 = downTerrain.terrainData;
            int resolution1 = data.heightmapResolution;
            int resolution2 = data2.heightmapResolution;

            float[,] heightmap = data.GetHeights(0, 0, resolution1, resolution1);
            float[,] heightmap2 = data.GetHeights(0, 0, resolution2, resolution2);

            for (int x = 0; x < resolution1; x++)
            {
                
                heightmap[resolution1 - 1, x] = Mathf.Lerp(heightmap[resolution1 - 5, x],heightmap2[4, x], 0.9f);
                heightmap[resolution1 - 2, x] = Mathf.Lerp(heightmap[resolution1 - 5, x],heightmap2[4, x], 0.7f);
                heightmap[resolution1 - 3, x] = Mathf.Lerp(heightmap[resolution1 - 5, x],heightmap2[4, x], 0.5f);
                heightmap[resolution1 - 4, x] = Mathf.Lerp(heightmap[resolution1 - 5, x],heightmap2[4, x], 0.3f);
            }
            
            // Set the modified heightmap back to the terrain
            data.SetHeights(0, 0, heightmap);
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

        public void SetRightEdgeHeightFromLeftEdge(Terrain terrain1, Terrain terrain2)
        {
            TerrainData data1 = terrain1.terrainData;
            TerrainData data2 = terrain2.terrainData;

            int resolution = data1.heightmapResolution;

            float[,] heightmap1 = data1.GetHeights(0, 0, resolution, resolution);
            float[,] heightmap2 = data2.GetHeights(0, 0, resolution, resolution);

            int edgeX1 = resolution - 1; // terrain1의 오른쪽 가장자리 X 좌표
            int edgeX2 = 0; // terrain2의 왼쪽 가장자리 X 좌표에서 한 픽셀 떨어진 곳의 X 좌표

            // terrain2의 왼쪽 가장자리에서 한 픽셀 떨어진 곳의 높이값을 terrain1의 오른쪽 가장자리 높이값들에 넣음
            for (int y = 0; y < resolution; y++)
            {
                float height = heightmap2[y, edgeX2];
                heightmap1[y, edgeX1] = height;
            }

            // Set the modified heightmap back to terrain1
            data1.SetHeights(0, 0, heightmap1);
        }
    }
}

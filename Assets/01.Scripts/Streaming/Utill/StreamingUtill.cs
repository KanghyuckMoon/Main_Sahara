using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Streaming
{
	public static class StreamingUtill
    {
        ///// <summary>
        /// Return scene Name from scene build index
        /// </summary>
        /// <param name="BuildIndex"></param>
        /// <returns></returns>
        //public static string NameFromIndex(int BuildIndex)
        //{
        //    string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        //    int slash = path.LastIndexOf('/');
        //    string name = path.Substring(slash + 1);
        //    int dot = name.LastIndexOf('.');
        //    return name.Substring(0, dot);
        //}

		/// <summary>
		/// Interger Position To Scene Name
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="_z"></param>
		/// <returns></returns>
		public static string NameFromPosition(int _x, int _y, int _z)
		{
			return $"Map({_x},{_y},{_z})";
		}

        ///// <summary>
        /// Return scene build index from scene name
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        //public static int GetSceneIndex(string sceneName)
        //{
        //    for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        //    {
        //        if (sceneName == NameFromIndex(i))
        //        {
        //            return i;
        //        }
        //    }
        //    return -1;
        //}

        /// <summary>
        /// Check Currently Acthive Scene from Build Index
        /// </summary>
        /// <param name="buildIndex"></param>
        /// <returns></returns>
        public static bool CheckCurrentlyActhive(string _sceneName)
		{
            for(int i = 0; i < SceneManager.sceneCount; ++i)
			{
                if(SceneManager.GetSceneAt(i).name == _sceneName)
				{
                    return true;
				}
            }
            return false;
        }

		/// <summary>
		/// 빌드세팅에 포함되어 있는 맵인지 체크
		/// </summary>
		/// <param name="_x"></param>
		/// <param name="_y"></param>
		/// <param name="chunkSize"></param>
		public static bool IsContainsMap(int _x, int _y, int _z, out string _name)
		{
			string sceneName = $"Map({_x},{_y},{_z})";
			_name = sceneName;
			for (int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i)
			{
				string path = SceneUtility.GetScenePathByBuildIndex(i);
				string scenebuildName = System.IO.Path.GetFileNameWithoutExtension(path);

				if (sceneName == scenebuildName)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Scene Name to Vector3
		/// </summary>
		/// <param name="_name"></param>
		/// <returns></returns>
		public static Vector3 StringToVector3(string _name)
		{
			Vector3 result = Vector3.zero;
			string c1 = _name.Split('(')[1];
			string xPos = c1.Split(',')[0];
			var c2 = c1.Split(',');
			string yPos = c2[1];
			var c3 = c2[2];
			string zPos = c3.Split(')')[0];

			result.x = float.Parse(xPos);
			result.y = float.Parse(yPos);
			result.z = float.Parse(zPos);

			return result;
		}

		/// <summary>
		/// Scene Name to Vector3
		/// </summary>
		/// <param name="_name"></param>
		/// <returns></returns>
		public static Vector3 StringToVector3AndScale(string _name)
		{
			return StringToVector3(_name) * 100;
		}
	}

}
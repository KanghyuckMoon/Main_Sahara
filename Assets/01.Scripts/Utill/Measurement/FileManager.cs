using System;
using System.IO;
using UnityEngine;

namespace Utill.Measurement
{
    public static class FileManager
    {
        /// <summary>
        /// 파일을 저장한다
        /// </summary>
        /// <param name="a_FileName"></param>
        /// <param name="a_FileContents"></param>
        /// <returns></returns>
        public static bool WriteToFile(string a_FileName, string a_FileContents)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);
            try
            {
                File.WriteAllText(fullPath, a_FileContents);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to write to {fullPath} with exception {e}");
                return false;
            }
        }

        /// <summary>
        /// 파일을 string으로 불러온다
        /// </summary>
        /// <param name="a_FileName"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool LoadFromFile(string a_FileName, out string result)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);

            try
            {
                result = File.ReadAllText(fullPath);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to read from {fullPath} with exception {e}");
                result = "";
                return false;
            }
        }
    }
}
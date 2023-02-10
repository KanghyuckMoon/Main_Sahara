using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Json
{
    public static class StaticSave
	{
		private static string _dataPath = Application.persistentDataPath + "/Save/";

		/// <summary>
		/// 유저 데이터 저장
		/// </summary>
		public static void Save<T>(ref T userSaveData, string _dataName = "")
		{
			string path = _dataPath + typeof(T).FullName + _dataName + ".txt";

			if (!File.Exists(path))
			{
				Directory.CreateDirectory($"{Application.persistentDataPath}/Save");
			}
			string jsonData = JsonUtility.ToJson(userSaveData, true);
            jsonData = Encrypt(jsonData, "종점");
            File.WriteAllText(path, jsonData);
		}

		/// <summary>
		/// 유저 데이터 불러오기
		/// </summary>
		public static void Load<T>(ref T userSaveData, string _dataName = "")
		{
			string path = _dataPath + typeof(T).FullName + _dataName + ".txt";

			if (File.Exists(path))
			{
				string jsonData = File.ReadAllText(path);
                jsonData = Decrypt(jsonData, "종점");
                T saveData = JsonUtility.FromJson<T>(jsonData);
				userSaveData = saveData;
			}
		}

		/// <summary>
		/// 세이브한 적이 있는지 체크
		/// </summary>
		/// <returns></returns>
		public static bool GetCheckBool()
		{
			return File.Exists(_dataPath);
		}

        public static string Decrypt(string textToDecrypt, string key)

        {

            RijndaelManaged rijndaelCipher = new RijndaelManaged();

            rijndaelCipher.Mode = CipherMode.CBC;

            rijndaelCipher.Padding = PaddingMode.PKCS7;



            rijndaelCipher.KeySize = 128;

            rijndaelCipher.BlockSize = 128;

            byte[] encryptedData = Convert.FromBase64String(textToDecrypt);

            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);

            byte[] keyBytes = new byte[16];

            int len = pwdBytes.Length;

            if (len > keyBytes.Length)

            {

                len = keyBytes.Length;

            }

            Array.Copy(pwdBytes, keyBytes, len);

            rijndaelCipher.Key = keyBytes;

            rijndaelCipher.IV = keyBytes;

            byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);

            return Encoding.UTF8.GetString(plainText);

        }



        public static string Encrypt(string textToEncrypt, string key)

        {

            RijndaelManaged rijndaelCipher = new RijndaelManaged();

            rijndaelCipher.Mode = CipherMode.CBC;

            rijndaelCipher.Padding = PaddingMode.PKCS7;



            rijndaelCipher.KeySize = 128;

            rijndaelCipher.BlockSize = 128;

            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);

            byte[] keyBytes = new byte[16];

            int len = pwdBytes.Length;

            if (len > keyBytes.Length)

            {

                len = keyBytes.Length;

            }

            Array.Copy(pwdBytes, keyBytes, len);

            rijndaelCipher.Key = keyBytes;

            rijndaelCipher.IV = keyBytes;

            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();

            byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);

            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));

        }
    }
}

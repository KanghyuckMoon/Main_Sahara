using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

namespace Utill.Pattern
{
    /// <summary>
    /// ������Ʈ�� �̱���ȭ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        // Destroy ���� Ȯ�ο�
        private static bool _ShuttingDown = false;
        private static object _Lock = new object();
        private static T _Instance;

        public static T Instance
        {
            get
            {
                // ���� ���� �� Object ���� �̱����� OnDestroy �� ���� ���� �� ���� �ִ�. 
                // �ش� �̱����� gameObject.Ondestory() ������ ������� �ʰų� ����Ѵٸ� null üũ�� ������
                if (_ShuttingDown)
                {
                    Debug.Log("[Singleton] Instance '" + typeof(T) + "' already destroyed. Returning null.");
                    return null;
                }

                lock (_Lock)    //Thread Safe
                {
                    if (_Instance is null)
                    {
                        // �ν��Ͻ� ���� ���� Ȯ��
                        _Instance = (T)FindObjectOfType(typeof(T));

						// ���� �������� �ʾҴٸ� �ν��Ͻ� ����
						if (_Instance is null)
						{
							// ���ο� ���ӿ�����Ʈ�� ���� �̱��� Attach
							var singletonObject = new GameObject();
							_Instance = singletonObject.AddComponent<T>();
							singletonObject.name = typeof(T).ToString() + " (Singleton)";

							// Make instance persistent.
							DontDestroyOnLoad(singletonObject);
						}
                        else
                        {
                            DontDestroyOnLoad(_Instance.gameObject);
                        }
                    }
                    return _Instance;
                }
            }
        }

        private void OnApplicationQuit()
        {
            if (Instance == this)
            {
                _ShuttingDown = true;
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
			{
                _ShuttingDown = true;
			}
        }

        public virtual void Awake()
		{
            if (Instance != this)
			{
                Destroy(gameObject);
			}
		}
	}

}
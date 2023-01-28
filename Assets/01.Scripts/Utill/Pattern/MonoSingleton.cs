using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

namespace Utill.Pattern
{
    /// <summary>
    /// ø¿∫Í¡ß∆Æ∏¶ ΩÃ±€≈Ê»≠
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private static readonly Lazy<T> instance =
            new Lazy<T>(() =>
            {
                T instance = FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).FullName);
                    instance = obj.AddComponent(typeof(T)) as T;

                    DontDestroyOnLoad(obj);
                }
                DontDestroyOnLoad(instance);

                return instance;
            });
        
        public virtual void Awake()
		{
            if (Instance != this)
			{
                Destroy(gameObject);
			}
		}
	}

}
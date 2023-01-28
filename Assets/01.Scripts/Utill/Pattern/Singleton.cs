using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

namespace Utill.Pattern
{
    /// <summary>
    /// ≈¨∑°Ω∫∏¶ ΩÃ±€≈Ê»≠
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class Singleton<T> where T : class, new()
    {
        public static T Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private static readonly Lazy<T> instance = new Lazy<T>(() => new T());

    }

}
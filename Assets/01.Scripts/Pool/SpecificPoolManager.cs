using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using Utill.Addressable;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace Pool
{
    
    public abstract class SpecificPoolManager<T> : MonoSingleton<SpecificPoolManager<T>> where T : MonoBehaviour
    {
        private Queue<T> gameObjectQueue = new Queue<T>();
        private const string key = "HitBox";
        public void Clear()
		{
            gameObjectQueue.Clear();
        }

        public int GetAllCount()
		{
            return gameObjectQueue.Count;
		}

        public T GetObject()
		{
            if (gameObjectQueue.Count > 0)
            {
                return gameObjectQueue.Dequeue();
            }
            else
            {
                CreateObject();
                return gameObjectQueue.Dequeue();
            }
        }

        public void RegisterObject(T _obj)
        {
            gameObjectQueue.Enqueue(_obj);
        }

        private void CreateObject(int count = 1)
        {
            GameObject prefeb = PrefebManager.Instance.GetPrefebDic<GameObject>(key);
            for (int i = 0; i < count; ++i)
            {
                GameObject obj = GameObject.Instantiate(prefeb, null);
                obj.name = prefeb.name;
                obj.SetActive(false);
                gameObjectQueue.Enqueue(obj.GetComponent<T>());
            }
        }
    }
}

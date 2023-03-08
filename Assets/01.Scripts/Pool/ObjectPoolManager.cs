using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using Utill.Addressable;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Pool
{
    public class ObjectPoolManager : Singleton<ObjectPoolManager>
    {
        private Dictionary<string, Queue<GameObject>> gameObjectQueueDic = new Dictionary<string, Queue<GameObject>>();

        public void Clear()
		{
            gameObjectQueueDic.Clear();
            PoolParentManager.Instance.Clear();
        }

        public int GetAllCount()
		{
            int count = 0;
            foreach (var a in gameObjectQueueDic)
			{
                count += a.Value.Count;
			}
            return count;
		}

        public GameObject GetObject(string key)
		{
            Queue<GameObject> queue;
            if(gameObjectQueueDic.TryGetValue(key, out queue))
			{
                if (queue.Count > 0)
                {
                    return queue.Dequeue();
                }
                else
                {
                    CreateObject(key);
                    return queue.Dequeue();
				}
            }
            else
			{
                queue = MakeQueueGet(key);
                return queue.Dequeue();
            }
        }
        public void GetObjectAsync(string key, System.Action<GameObject> _action)
        {
            Queue<GameObject> queue;
            if (gameObjectQueueDic.TryGetValue(key, out queue) && queue.Count > 0)
            {
                GameObject obj = queue.Dequeue();
                PoolParentManager.Instance.SetUseParent(key, obj);
                if (_action is not null)
                {
                    _action(obj);
                }
            }
            else
            {
                MakeQueueGetAsync(key, _action);
            }
        }
        public void GetObjectAsyncParameter<J>(string key, System.Action<GameObject, J> _action, J _parameter)
        {
            Queue<GameObject> queue;
            if (gameObjectQueueDic.TryGetValue(key, out queue) && queue.Count > 0)
            {
                GameObject obj = queue.Dequeue();
                PoolParentManager.Instance.SetUseParent(key, obj);
                _action(obj, _parameter);
            }
            else
            {
                MakeQueueWithParameterGetAsync<J>(key, _action, _parameter);
            }
        }

        public void RegisterObject(string key, GameObject gameObject)
        {
            Queue<GameObject> queue;
            if (gameObjectQueueDic.TryGetValue(key, out queue))
            {
                queue.Enqueue(gameObject);
            }
            else
            {
                queue = MakeQueueRegister(key);
                queue.Enqueue(gameObject);
            }
            PoolParentManager.Instance.SetPoolParent(key, gameObject);
        }

        public void RegisterObjectAsync(string key, GameObject gameObject)
        {
            Queue<GameObject> queue;
            if (gameObjectQueueDic.TryGetValue(key, out queue))
            {
                PoolParentManager.Instance.SetPoolParent(key, gameObject);
                queue.Enqueue(gameObject);
            }
            else
            {
                var handle = MakeQueueRegisterAsync(key);
                handle.Completed += (x) => 
                {
                    queue = gameObjectQueueDic[key];
                    PoolParentManager.Instance.SetPoolParent(key, gameObject);
                    queue.Enqueue(gameObject);
                };
            }
        }

        private void CreateObject(string key, int count = 1)
        {
            GameObject prefeb = PrefebManager.Instance.GetPrefebDic<GameObject>(key);
            for (int i = 0; i < count; ++i)
            {
                GameObject obj = GameObject.Instantiate(prefeb, null);
                obj.SetActive(false);
                Queue<GameObject> queue;
                queue = MakeQueueRegister(key);
                queue.Enqueue(obj);
            }
        }

        //Å¥ »ý¼º

        //Get
        private Queue<GameObject> MakeQueueGet(string key)
        {
            Queue<GameObject> queue;
            if (!gameObjectQueueDic.TryGetValue(key, out queue))
            {
                queue = new Queue<GameObject>();
                gameObjectQueueDic.Add(key, queue);
                CreateObject(key);
            }
            return queue;
        }

        private Queue<GameObject> MakeQueueGet(string key, GameObject gameObject)
        {
            GameObject obj = GameObject.Instantiate(gameObject, null);
            Queue<GameObject> queue;
            if (gameObjectQueueDic.TryGetValue(key, out queue))
            {
                queue.Enqueue(obj);
            }
            else
            {
                queue = new Queue<GameObject>();
                queue.Enqueue(obj);
                gameObjectQueueDic.Add(key, queue);
            }
            return queue;
        }

        private void MakeQueueGetAsync(string key, System.Action<GameObject> _action)
        {
            var handle = AddressablesManager.Instance.GetResourceAsync<GameObject>(key);
            handle.Completed += (_x) =>
            {
                PrefebManager.Instance.AddPrefeb(key, _x.Result);
                MakeQueueGet(key, _x.Result);
                GameObject obj = GetObject(key);
                PoolParentManager.Instance.SetUseParent(key, obj);
                _action?.Invoke(obj);
            };
        }
        private void MakeQueueWithParameterGetAsync<J>(string key, System.Action<GameObject, J> _action, J _parameter)
        {
            var handle = AddressablesManager.Instance.GetResourceAsync<GameObject>(key);
            handle.Completed += (_x) =>
            {
                PrefebManager.Instance.AddPrefeb(key, _x.Result);
                MakeQueueGet(key, _x.Result);
                GameObject obj = GetObject(key);
                PoolParentManager.Instance.SetUseParent(key, obj);
                _action.Invoke(obj, _parameter);
            };
        }
        //Register
        private AsyncOperationHandle<GameObject> MakeQueueRegisterAsync(string key)
        {
            var handle = AddressablesManager.Instance.GetResourceAsync<GameObject>(key);
            handle.Completed += (_x) =>
            {
                PrefebManager.Instance.AddPrefeb(key, _x.Result);
                MakeQueueRegister(key);
            };
            return handle;
        }
        private Queue<GameObject> MakeQueueRegister(string key)
        {
            Queue<GameObject> queue;
            if (!gameObjectQueueDic.TryGetValue(key, out queue))
            {
                queue = new Queue<GameObject>();
                gameObjectQueueDic.Add(key, queue);
            }
            return queue;
        }

    }

}
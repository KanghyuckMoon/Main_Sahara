using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

namespace Pool
{
    public class ClassPoolManager : Singleton<ClassPoolManager>
    {
        public Dictionary<string, Queue<object>> ClassQueueDic
        {
            get
			{
                return classQueueDic;

            }
        }
		private Dictionary<string, Queue<object>> classQueueDic = new Dictionary<string, Queue<object>>();

        public int GetAllCount()
		{
            int count = 0;
            foreach (var a in classQueueDic)
			{
                count += a.Value.Count;
            }
            return count;
		}

        public T GetClass<T>(string key) where T : class
        {
            Queue<object> queue;
            if (classQueueDic.TryGetValue(key, out queue) && queue.Count > 0)
            {
                return (T)queue.Dequeue();
            }
            else
            {
                queue = MakeQueue(key);
                return null;
            }
        }
        public void RegisterObject<T>(string key, T cl) where T : class
        {
            Queue<object> queue;
            if (classQueueDic.TryGetValue(key, out queue))
            {
                queue.Enqueue(cl);
            }
            else
            {
                queue = MakeQueue(key);
                queue.Enqueue(cl);
            }
        }
        private Queue<object> MakeQueue(string key)
        {
            Queue<object> queue;
            if (classQueueDic.TryGetValue(key, out queue))
            {

            }
            else
            {
                queue = new Queue<object>();
                classQueueDic.Add(key, queue);
            }
            return queue;
        }

    }
}
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

        public void Clear()
		{
            classQueueDic.Clear();
        }

        public int GetAllCount()
		{
            int count = 0;
            foreach (var a in classQueueDic)
			{
                count += a.Value.Count;
            }
            return count;
		}

        public T GetClass<T>() where T : class
        {
            Queue<object> queue;
            if (classQueueDic.TryGetValue(nameof(T), out queue) && queue.Count > 0)
            {
                return (T)queue.Dequeue();
            }
            else
            {
                queue = MakeQueue(nameof(T));
                return null;
            }
        }
        public void RegisterObject<T>(T cl) where T : class
        {
            Queue<object> queue;
            if (classQueueDic.TryGetValue(nameof(T), out queue))
            {
                queue.Enqueue(cl);
            }
            else
            {
                queue = MakeQueue(nameof(T));
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
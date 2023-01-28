using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using Utill.Addressable;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Pool
{
    public class PrefebManager : Singleton<PrefebManager>
    {
        private Dictionary<string, object> prefebDic = new Dictionary<string, object>();

        public T GetPrefebDic<T>(string key)
		{
            if (prefebDic.TryGetValue(key, out var prefeb))
			{
                return (T)prefeb;
			}
            else
			{
                var obj = AddressablesManager.Instance.GetResource<T>(key);
                AddPrefeb(key, obj);
                return obj;
            }
		}

        public void GetPrefebDicAsync<T>(string key, System.Action<T> action)
        {
            if (prefebDic.TryGetValue(key, out var prefeb))
            {
                action.Invoke((T)prefeb);
            }
            else
            {
                var obj = AddressablesManager.Instance.GetResourceAsync<T>(key, action);
                AddPrefeb(key, obj);
            }
        }
        public void GetPrefebDicWithParameterAsync<T, J>(string key, System.Action<T, J> _action, J _parameter)
        {
            if (prefebDic.TryGetValue(key, out var prefeb))
            {
                _action.Invoke((T)prefeb, _parameter);
            }
            else
            {
                var obj = AddressablesManager.Instance.GetResourceAsync<T, J>(key, _action, _parameter);
                obj.Completed += (x) =>
                {
                    AddPrefeb(key, obj.Result);
                };
            }
        }

        public void AddPrefeb(string key, object obj)
        {
            if (prefebDic.ContainsKey(key))
			{
                return;
			}
            prefebDic.Add(key, obj);
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using Pool;

namespace Module
{
    public class UIModule : AbBaseModule, Obserble
	{
		public List<Observer> Observers
		{
			get
			{
				return observers;
			}
		}

		public bool IsRender
		{
			get
			{
				return	isRender;
			}
			set
			{
				isRender = value;
				Send();
			}

		}
		private string address = null;
        private List<Observer> observers = new List<Observer>();
		private bool isRender;

        public UIModule(AbMainModule _mainModule, string _address = null) : base(_mainModule)
        {
            address = _address;
        }

		public void AddObserver(Observer _observer)
		{
			Observers.Add(_observer);
		}

		public void RemoveObserver(Observer _observer)
		{
			Observers.Remove(_observer);
		}

		public void Send()
		{
			foreach (Observer observer in Observers)
			{
				observer.Receive();
			}
		}
		public override void Start()
        {
            if (address is null)
			{
                return;
			}

            //UI 동적 생성
            GameObject _hudUI = ObjectPoolManager.Instance.GetObject(address);
            _hudUI.transform.SetParent(mainModule.transform);
            _hudUI.SetActive(true);
        }
    }
}
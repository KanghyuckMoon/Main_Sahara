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
                return isRender;
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
        private GameObject hudObject;

        public UIModule(AbMainModule _mainModule, string _address = null) : base(_mainModule)
        {
            address = _address;
        }

        public void AddObserver(Observer _observer)
        {
            Observers.Add(_observer);
            _observer.Receive();
        }

        public void RemoveObserver(Observer _observer)
        {
            Observers.Remove(_observer);
            _observer.Receive();
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
                IsRender = true;
                return;
            }

            //UI 동적 생성
            hudObject = ObjectPoolManager.Instance.GetObject(address);
            hudObject.transform.SetParent(mainModule.transform);
            hudObject.SetActive(true);
        }

		public override void OnDisable()
		{
			base.OnDisable();

            ObjectPoolManager.Instance.RegisterObject(address, hudObject);
            hudObject.SetActive(false);

        }
	}
}
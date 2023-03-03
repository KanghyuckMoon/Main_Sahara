using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using Utill.Coroutine;
using UnityEngine.SceneManagement;
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
            Init(_mainModule, _address);
        }
        public UIModule()
        {

        }

		public override void Init(AbMainModule _mainModule, params string[] _parameters)
		{
			base.Init(_mainModule, _parameters);
            if (_parameters is not null)
			{
                if(_parameters.Length > 0)
				{
                    address = _parameters[0];
				}
			}

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
            //hudObject = ObjectPoolManager.Instance.GetObject(address);
            //hudObject.transform.SetParent(mainModule.transform);
            //hudObject.SetActive(true);
        }

		public override void OnDestroy()
		{
			base.OnDestroy();
            //RegisterUI();
            hudObject = null;
        }

		public override void OnEnable()
		{
			base.OnEnable();
            StaticCoroutineManager.Instance.InstanceDoCoroutine(GetUI());
        }

		public override void OnDisable()
		{
			base.OnDisable();
            StaticCoroutineManager.Instance.InstanceDoCoroutine(IRegisterUI());
            ClassPoolManager.Instance.RegisterObject<UIModule>("UIModule", this);
        }

        private IEnumerator IRegisterUI()
        {
            yield return new WaitForSeconds(0.1f);
            if (hudObject is null)
			{
                yield break;
			}
            RegisterUI();
        }

        private void RegisterUI()
		{

			if (hudObject is null)
			{
                return;
			}
			try
            {
                hudObject.SetActive(false);
                ObjectPoolManager.Instance.RegisterObject(address, hudObject);
            }
			catch
			{

			}
            hudObject = null;
        }

        private IEnumerator GetUI()
        {

            yield return new WaitForSeconds(0.1f);
            if (address is null)
            {
                IsRender = true;
                yield break;
            }
            hudObject = ObjectPoolManager.Instance.GetObject(address);
            hudObject.transform.SetParent(null);
            SceneManager.MoveGameObjectToScene(hudObject, mainModule.gameObject.scene);
            hudObject.transform.SetParent(mainModule.transform);
            hudObject.SetActive(true);
        }
    }
}
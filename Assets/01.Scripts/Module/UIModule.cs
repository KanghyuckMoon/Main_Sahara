using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using Pool;

namespace Module
{
    public class UIModule : AbBaseModule
    {
        private string address = null;

        public UIModule(AbMainModule _mainModule, string _address = null) : base(_mainModule)
        {
            address = _address;
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
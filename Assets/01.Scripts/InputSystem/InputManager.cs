using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using Utill.SeralizableDictionary;
using Utill.Addressable;

namespace InputSystem
{
    public class InputManager : MonoSingleton<InputManager>
    {
		private InputSO inputSO;
		private Dictionary<string, bool> keyInputDic = new Dictionary<string, bool>();

		public void Start()
		{
			inputSO = AddressablesManager.Instance.GetResource<InputSO>("InputSO");
			foreach(var _key in inputSO.keyCodeDic)
			{
				keyInputDic.Add(_key.Key, false);
			}
		}

		public bool CheckKey(string _str)
		{
			return keyInputDic[_str];
		}

		public void ChangeKey(string _str, KeyCode _keyCode)
		{
			inputSO.keyCodeDic[_str].keyCode = _keyCode;
		}

		public void Update()
		{
			foreach(var _key in inputSO.keyCodeDic)
			{
				switch (_key.Value.inputType)
				{
					case InputType.Down:
						if (Input.GetKeyDown(_key.Value.keyCode))
						{
							keyInputDic[_key.Key] = true;
						}
						else
						{
							keyInputDic[_key.Key] = false;
						}
						break;
					case InputType.Ing:
						if (Input.GetKey(_key.Value.keyCode))
						{
							keyInputDic[_key.Key] = true;
						}
						else
						{
							keyInputDic[_key.Key] = false;
						}
						break;
					case InputType.Up:
						if (Input.GetKeyUp(_key.Value.keyCode))
						{
							keyInputDic[_key.Key] = true;
						}
						else
						{
							keyInputDic[_key.Key] = false;
						}
						break;
					case InputType.None:
						if (!Input.GetKey(_key.Value.keyCode))
						{
							keyInputDic[_key.Key] = true;
						}
						else
						{
							keyInputDic[_key.Key] = false;
						}
						break;
				}
			}
		}
	}
}

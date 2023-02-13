using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utill;
using Pool;
using Utill.Addressable;
using Utill.Pattern;

namespace Effect
{
    /// <summary>
    /// ����Ʈ ���� �Ŵ���
    /// </summary>
    public class EffectManager : MonoSingleton<EffectManager>
    {
        private bool _isInit = false;

        public void Start()
        {
            if (!_isInit)
            {
                if (Instance == this)
                {
                    Init();
                }
                else
                {
                    Instance.Init();
                }
            }
        }

        /// <summary>
        /// �ʱ�ȭ
        /// </summary>
        private void Init()
        {
            if (_isInit)
            {
                return;
            }
            _isInit = true;
        }

        /// <summary>
        /// ����Ʈ ��ġ
        /// </summary>
        /// <param name="pos"></param>
        public void SetEffectDefault(string _adress, Vector3 _pos, Quaternion _quaternion)
        {
            if (!_isInit)
            {
                Init();
            }

            GameObject effect = ObjectPoolManager.Instance.GetObject(_adress);
            effect.transform.position = _pos;
			effect.transform.rotation = _quaternion;
			effect.transform.SetParent(null);
			effect.gameObject.SetActive(true);
		}
	}

}
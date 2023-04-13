using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using Module;
using Utill.Random;
using Pool;

namespace Detect
{
public class CreatureDetectItem : MonoBehaviour, IDetectItem
{
        public DetectItemType DetectItemType
        {
            get
            {
                return detectItemType;
            }
            set
            {
                detectItemType = value;
            }
        }

        public bool IsGetOut
        {
            get
            {
                return isGetOut;
            }
            set
            {
                isGetOut = value;
            }
        }

        private bool isGetOut = false;

        [SerializeField]
        protected DetectItemType detectItemType;

        [SerializeField] 
        private Transform spawnTrm;

        [SerializeField] 
        private float upPower = 20f;
        
        [SerializeField] 
        private float xzPower = 5f;
        
        [SerializeField] 
        private string key;

        [SerializeField] 
        private bool isEnemy;

        public List<Observer> Observers
        {
            get
            {
                return observers;
            }
        }

        private List<Observer> observers = new List<Observer>();
        
        public void GetOut()
        {
            ItemDrop(); 
        }
        
        
        private void ItemDrop()
        {
            GameObject _dropObj = ObjectPoolManager.Instance.GetObject(key);
            _dropObj.transform.position = spawnTrm.position;
            _dropObj.SetActive(true);
            float _powerRight = Random.Range(-1f, 1f) * xzPower;
            float _powerForward = Random.Range(-1f, 1f) * xzPower;
            Vector3 _vec = Vector3.up * upPower + Vector3.right * _powerRight + Vector3.forward * _powerForward;

            if (isEnemy)
            {
                var _mainModule = _dropObj.GetComponent<AbMainModule>();
                _mainModule.attackedTime = 0f;
                _mainModule.knockBackPower = upPower;
                _mainModule.KnockBackVector = _vec.normalized;
            }
            else
            {
                _dropObj.GetComponentInChildren<Rigidbody>().AddForce(_vec, ForceMode.Impulse);   
            }
            gameObject.SetActive(false);
            isGetOut = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using Utill.Random;
using Pool;

namespace Detect
{
    public class ObjectDetectItem : MonoBehaviour, IDetectItem
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

        [SerializeField]
        protected DetectItemType detectItemType;

        [SerializeField] 
        private int count = 10;

        [SerializeField] 
        private int minApperCount = 1;
        
        [SerializeField] 
        private int maxApperCount = 3;

        [SerializeField] 
        private Transform spawnTrm;

        [SerializeField] 
        private float upPower = 20f;
        
        [SerializeField] 
        private float xzPower = 5f;
        
        [SerializeField]
        private DropItemListSO dropItemListSO;
        
        public List<Observer> Observers
        {
            get
            {
                return observers;
            }
        }

        private List<Observer> observers = new List<Observer>();
        
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
        
        public void GetOut()
        {
            if (count > 0)
            {
                int randomCount = Random.Range(minApperCount, maxApperCount);
                if (randomCount > count)
                {
                    randomCount = count;
                }
                count -= randomCount;
                for (int i = 0; i < randomCount; ++i)
                {
                    int _index = StaticRandom.Choose(dropItemListSO.randomPercentArr);
                    if (dropItemListSO.dropItemKeyArr[_index] is null || dropItemListSO.dropItemKeyArr[_index] is "")
                    {
                        continue;
                    }

                    //Vector3 _spawnPos = Vector3.Lerp(other.ClosestPoint(transform.position), other.transform.position, 0.9f);
                
                    ItemDrop(dropItemListSO.dropItemKeyArr[_index]); 
                }
            }
            else
            {
                isGetOut = true;
            }
        }
        
        
        private void ItemDrop(string _key)
        {
            if (_key is null || _key == "")
            {
                return;
            }
            GameObject _dropObj = ObjectPoolManager.Instance.GetObject(_key);
            _dropObj.transform.position = spawnTrm.position;
            _dropObj.SetActive(true);
            float _powerRight = Random.Range(-1f, 1f) * xzPower;
            float _powerForward = Random.Range(-1f, 1f) * xzPower;
            Vector3 _vec = Vector3.up * upPower + Vector3.right * _powerRight + Vector3.forward * _powerForward;
            _dropObj.GetComponent<Rigidbody>().AddForce(_vec, ForceMode.Impulse);
        }
    }
}

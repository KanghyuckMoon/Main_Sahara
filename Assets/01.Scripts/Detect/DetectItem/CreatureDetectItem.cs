using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Inventory;
using Module;
using Utill.Random;
using Pool;
using Effect;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;

#endif

namespace Detect
{
    public class CreatureDetectItem : MonoBehaviour, IDetectItem
    {
    private static Dictionary<string, bool> isSpawnDic = new Dictionary<string, bool>();
    private static int nameKey;
    [SerializeField] 
    private string key;
    
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
        
        [FormerlySerializedAs("key")] [SerializeField] 
        private string objkey;

        [SerializeField] 
        private bool isEnemy;
        
        [SerializeField] 
        private string effectAddress;

		[SerializeField]
		protected UnityEvent setEvent;

		[SerializeField]
        protected UnityEvent getoutEventAfter;
        
        [SerializeField]
        protected UnityEvent getoutEvent;

        [SerializeField] 
        protected bool isUseAlreadyCreature;

        
        [SerializeField] 
        protected GameObject creature;

        public List<Observer> Observers
        {
            get
            {
                return observers;
            }
        }

        private List<Observer> observers = new List<Observer>();
#if UNITY_EDITOR
        [ContextMenu("RandomName")]
        public void RandomName()
        {
            var _prefeb = UnityEditor.PrefabUtility.GetCorrespondingObjectFromSource(gameObject);
            //gameObject.name = _prefeb.name + nameKey++;
            key = _prefeb.name + nameKey++;

            EditorSceneManager.MarkSceneDirty(gameObject.scene);
        }
			
#endif

        private void Start()
        {
            if (isSpawnDic.ContainsKey(key))
            {
                isGetOut = true;
                gameObject.SetActive(false);

			}
            else
            {
                setEvent?.Invoke();
            }
		}

        public void GetOut()
        {
            if (!isGetOut)
            {
                ItemDrop();
            }
        }
        
        
        private void ItemDrop()
        {
            getoutEventAfter?.Invoke();
            GameObject _dropObj = null;
            if (isUseAlreadyCreature)
            {
                _dropObj = creature;
            }
            else
            {
                _dropObj = ObjectPoolManager.Instance.GetObject(objkey);
            }

            if(_dropObj == null)
			{
				_dropObj.transform.position = spawnTrm.position;
				_dropObj.SetActive(true);
				float _powerRight = Random.Range(-0.2f, 0.2f) * xzPower;
				float _powerForward = Random.Range(-0.2f, 0.2f) * xzPower;
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
			}
            EffectManager.Instance.SetEffectDefault(effectAddress, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            isGetOut = true;
            isSpawnDic.Add(key, isGetOut);
            getoutEvent?.Invoke();
        }
    }
}

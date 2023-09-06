using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pool;
using EquipmentSystem;
using Utill.Addressable;
using Data;
using Pool;

namespace Module {
    public class EquipmentModule : AbBaseModule
    {
        //[SerializeField]
        //private EquipmentItem[] equipmentItem = new EquipmentItem[4];//기본 아이템 == 기본 스킨

        //private BoneItem[] boneItem = new BoneItem[4]; //아이템 종류들
        //private CharacterEquipment characterEquipment;

        //private StatData statData => mainModule.StatData;

        private Dictionary<ItemType, GameObject> EquipPositions
        {
            get
            {
                return equipPositions;
            }
        }

        private Dictionary<ItemType, GameObject> equipPositions = new Dictionary<ItemType, GameObject>(); //<<----- 위치 저장용
        private Dictionary<ItemType, GameObject> equipItem = new Dictionary<ItemType, GameObject>(); //<<---- 실제 아이템

        private string pastItemString;

        public EquipmentModule(AbMainModule _mainModule) : base(_mainModule)
        {
            //characterEquipment = new CharacterEquipment(mainModule.VisualObject);
        }
        public EquipmentModule() : base()
        {

        }

        public override void OnEnable()
        {
            EquipPosition[] _equipPositions = mainModule.VisualObject.GetComponentsInChildren<EquipPosition>();


            foreach(EquipPosition _equipPosition in _equipPositions)
            {
                equipPositions.Add(_equipPosition.itemType, _equipPosition.gameObject);
                equipItem.Add(_equipPosition.itemType, null);
            }
            //equipPositions.Add(mainModule.VisualObject.GetComponentsInChildren<EquipPosition>();
        }

        /*
        private EquipPosition[] setMeshObj(EquipPosition[] _equipPositions)
        {
            List<Transform> retMeshObjs = new List<Transform>();
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                if (meshRenderer.transform.parent != null)
                {
                    Transform parentBone = baseBoneInfos[meshRenderer.transform.parent.name.GetHashCode()];
                    GameObject itemObj = GameObject.Instantiate(meshRenderer.gameObject, parentBone);
                    retMeshObjs.Add(itemObj.transform);
                }
            }
            return retMeshObjs.ToArray();
        }*/

        public void OnEquipItem(string _itemString)
        {
            //   여기서 가지고 와야함
            GameObject _item = AddressablesManager.Instance.GetResource<GameObject>(_itemString);
            EquipingItem _equipingItem = _item?.GetComponent<EquipingItem>();

            switch (_equipingItem.itemType)
            {
                case ItemType.HELMET:
                case ItemType.EAR:
                case ItemType.BACK:
                case ItemType.WRIST:
                case ItemType.SHOULDER:
                case ItemType.NONE:
                    {
                        if (equipItem[_equipingItem.itemType] is not null)
                            TakeOffItem(_equipingItem.itemType);
                        TakeOnItem(_itemString, _equipingItem);
                        break;
                    }
                default:
                    break;
            }

            pastItemString = _itemString;
        }

        public void TakeOffItem(ItemType _itemType)
        {
            try
            {
                EquipingItem _equipingItem = equipItem[_itemType].GetComponent<EquipingItem>();
                equipItem[_itemType].SetActive(false);
                equipItem[_itemType] = null;

                ApplyItemStats(_equipingItem, -1);

                ObjectPoolManager.Instance.RegisterObject(pastItemString, _equipingItem.gameObject);
            }
            catch
            {
                
            }
            //equipPositions[_itemType] = equipPositions[_itemType].transform.parent.gameObject;
        }

        public void TakeOffItem(int _num)
        {
            switch (_num)
            {
                case 0:
                    if (equipItem.TryGetValue(ItemType.HELMET, out var _obj))
                    {
                        if(equipItem[ItemType.HELMET] == null) return; 
                        EquipingItem _equipingItem = equipItem[ItemType.HELMET].GetComponent<EquipingItem>();
                        equipItem[ItemType.HELMET].SetActive(false);
                        equipItem[ItemType.HELMET] = null;
                        ApplyItemStats(_equipingItem, -1);
                        ObjectPoolManager.Instance.RegisterObject(pastItemString, _equipingItem.gameObject);
                    }
                    else
                    {
                        if(equipItem[ItemType.EAR] == null) return; 
                        EquipingItem _equipingItem = equipItem[ItemType.EAR].GetComponent<EquipingItem>();
                        equipItem[ItemType.EAR].SetActive(false);
                        equipItem[ItemType.EAR] = null;
                        ApplyItemStats(_equipingItem, -1);
                        ObjectPoolManager.Instance.RegisterObject(pastItemString, _equipingItem.gameObject);
                    }
                    break;
                
                case 1:
                    if(equipItem[ItemType.SHOULDER] == null) return; 
                    EquipingItem _equipingItem2 = equipItem[ItemType.SHOULDER].GetComponent<EquipingItem>();
                    equipItem[ItemType.SHOULDER].SetActive(false);
                    equipItem[ItemType.SHOULDER] = null;
                    ApplyItemStats(_equipingItem2, -1);
                    ObjectPoolManager.Instance.RegisterObject(pastItemString, _equipingItem2.gameObject);
                    break;
                case 2:
                    if(equipItem[ItemType.WRIST] == null) return; 
                    EquipingItem _equipingItem3 = equipItem[ItemType.WRIST].GetComponent<EquipingItem>();
                    equipItem[ItemType.WRIST].SetActive(false);
                    equipItem[ItemType.WRIST] = null;
                    ApplyItemStats(_equipingItem3, -1);
                    ObjectPoolManager.Instance.RegisterObject(pastItemString, _equipingItem3.gameObject);
                    break;
                case 3:
                    if(equipItem[ItemType.BACK] == null) return; 
                    EquipingItem _equipingItem4 = equipItem[ItemType.BACK].GetComponent<EquipingItem>();
                    equipItem[ItemType.BACK].SetActive(false);
                    equipItem[ItemType.BACK] = null;
                    ApplyItemStats(_equipingItem4, -1);
                    ObjectPoolManager.Instance.RegisterObject(pastItemString, _equipingItem4.gameObject);
                    break;
            
            }
        }

        private void TakeOnItem(string _itemString, EquipingItem _equipingItem)
        {
            if (_itemString is not null)
            {
                GameObject _item = ObjectPoolManager.Instance.GetObject(_itemString);

                _item.SetActive(true);
                _item.transform.SetParent(EquipPositions[_equipingItem.itemType].transform);
                _item.transform.localPosition = _equipingItem.setPos;
                _item.transform.localRotation = _equipingItem.setRot;
                _item.transform.localScale = _equipingItem.scale;

                equipItem[_equipingItem.itemType] = _item;

                ApplyItemStats(_equipingItem, 1);
            }
        }

        private void ApplyItemStats(EquipingItem _equipingItem, int _multiplyValue)
        {
            mainModule.StatData.MeleeAttack += (_equipingItem.itemDataSO.atk * _multiplyValue);
            mainModule.StatData.MagicAttack += (_equipingItem.itemDataSO.magic * _multiplyValue);
            mainModule.StatData.MaxHp += (_equipingItem.itemDataSO.hp * _multiplyValue);
            mainModule.StatData.CurrentHp += _equipingItem.itemDataSO.hp * Mathf.Max(_multiplyValue, 0);
            mainModule.StatData.PhysicalResistance += _equipingItem.itemDataSO.atkDef * _multiplyValue;
            mainModule.StatData.MagicResistance += _equipingItem.itemDataSO.magicDef * _multiplyValue;
            //mainModule.StatData.MaxHp = _equipingItem.itemDataSO.hp;
            //mainModule.StatData.MaxHp = _equipingItem.itemDataSO.hp;
        }

        public override void OnDisable()
        {
            foreach (var _itemType in equipItem.Keys.ToList())
            {
                TakeOffItem(_itemType);
            }
            equipPositions.Clear();
            equipItem.Clear();
            base.OnDisable();
            ClassPoolManager.Instance.RegisterObject<EquipmentModule>(this);
        }

        public override void OnDestroy()
        {
            foreach (var _itemType in equipItem.Keys.ToList())
            {
                TakeOffItem(_itemType);
            }
            equipPositions.Clear();
            equipItem.Clear();
            base.OnDestroy();
            ClassPoolManager.Instance.RegisterObject<EquipmentModule>(this);
        }

        #region Trash
        #endregion
    }
}
using System.Collections;
using System.Collections.Generic;
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

        public override void Start()
        {
            EquipPosition[] _equipPositions = mainModule.VisualObject.GetComponentsInChildren<EquipPosition>();


            foreach(EquipPosition _equipPosition in _equipPositions)
            {
                equipPositions.Add(_equipPosition.itemType, _equipPosition.gameObject);
                equipItem.Add(_equipPosition.itemType, null);
            }
            //equipPositions.Add(mainModule.VisualObject.GetComponentsInChildren<EquipPosition>();
        }

        public override void Update()
        {
            if (!mainModule.player) return;
            if (Input.GetKeyDown(KeyCode.J))
                OnEquipItem("RomanHelmat");
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

        private void TakeOffItem(ItemType _itemType)
        {
            EquipingItem _equipingItem = equipItem[_itemType].GetComponent<EquipingItem>();
            equipItem[_itemType].SetActive(false);
            equipItem[_itemType] = null;

            ApplyItemStats(_equipingItem, -1);

            ObjectPoolManager.Instance.RegisterObject(pastItemString, _equipingItem.gameObject);
            //equipPositions[_itemType] = equipPositions[_itemType].transform.parent.gameObject;
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
            base.OnDisable();
            ClassPoolManager.Instance.RegisterObject<EquipmentModule>("EquipmentModule", this);
        }
        #region Trash
        //private BoneItem equipItemSkinned(EquipmentItem itemObj)
        //{
        //    if (itemObj == null)
        //    {
        //        return null;
        //    }

        //    Transform itemInfo = characterEquipment.setBoneItem(itemObj.objModelPrefab, itemObj.boneNameLists);

        //    BoneItem boneItem = itemInfo.gameObject.AddComponent<BoneItem>();
        //    if (boneItem != null)
        //    {
        //        boneItem.itemLists.Add(itemInfo);
        //    }

        //    return boneItem;
        //}

        //private BoneItem equipItemMesh(EquipmentItem itemObj)
        //{
        //    if (itemObj == null)
        //    {
        //        return null;
        //    }
        //    Transform[] itemInfos = characterEquipment.setMeshItem(itemObj.objModelPrefab);
        //    if (itemInfos.Length > 0)
        //    {
        //        BoneItem boneItem = new GameObject().AddComponent<BoneItem>();
        //        foreach (Transform t in itemInfos)
        //        {
        //            boneItem.itemLists.Add(t);
        //        }

        //        boneItem.transform.parent = mainModule.VisualObject.transform;

        //        return boneItem;
        //    }

        //    return null;
        //}

        #endregion
    }
}
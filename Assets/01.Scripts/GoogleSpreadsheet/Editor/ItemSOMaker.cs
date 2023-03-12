#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using Inventory;
using System.Linq;
using GoogleSpreadSheet;
using System;
using Module;

public class ItemSOMaker : MonoBehaviour
{
    public AllItemDataSO allItemDataSO;
    public AllItemDataSO allDropItemDataSO;
    public AllItemUpgradeDataSO allItemUpgradeDataSO;

    [ContextMenu("MakeItemSO")]
    public void MakeItemSO()
    {
        StartCoroutine(GetText());
    }

    private IEnumerator GetText()
    {
        UnityWebRequest wwwItemSO = UnityWebRequest.Get(URL.ITEMSO);
        yield return wwwItemSO.SendWebRequest();
        SetSOItem(wwwItemSO.downloadHandler.text);

        UnityWebRequest wwwDropItemSO = UnityWebRequest.Get(URL.DROPITEMSO);
        yield return wwwDropItemSO.SendWebRequest();
        SetSODropItem(wwwDropItemSO.downloadHandler.text);

        UnityWebRequest wwwUpgradeItemSO = UnityWebRequest.Get(URL.UPGRADEITEMSO);
        yield return wwwUpgradeItemSO.SendWebRequest();
        SetSOUpgradeItem(wwwUpgradeItemSO.downloadHandler.text);
    }

    private void SetSOItem(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;

        for (int i = 1; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');

            string _key = column[0];
            string _count = column[1];
            string _price = column[2];
            string _nameKey = column[3];
            string _explationKey = column[4];
            string _spritekey = column[5];
            string _stackble = column[6];
            string _stackMax = column[7];
            string _itemType = column[8];
            string _consumptionType = column[9];
            string _prefebKey = column[10];
            string _animationLayer = column[11];
            string _dropItemKey = column[12];
            string _accessoriesItemType = column[13];
            string _equipmentType = column[14];
            string _isSlot = column[15];

            ItemDataSO _asset = null;
            _asset = allItemDataSO.itemDataSOList.Find(x => x.key == _key);
            //이미 있는지
            if (_asset == null)
            {
                _asset = ScriptableObject.CreateInstance<ItemDataSO>();

                AssetDatabase.CreateAsset(_asset, $"Assets/02.ScriptableObject/InventorySO/MakeItemDataSO/{_key}SO.asset");
                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();

                Selection.activeObject = _asset;

                allItemDataSO.itemDataSOList.Add(_asset);
            }
            _asset.key = _key;
            _asset.count = int.Parse(_count);
            _asset.price = int.Parse(_price);
            _asset.nameKey = _nameKey;
            _asset.explanationKey = _explationKey;
            _asset.spriteKey = _spritekey;
            _asset.stackble = Convert.ToBoolean(_stackble);
            _asset.stackMax = int.Parse(_stackMax);
            _asset.itemType = (ItemType)Enum.Parse(typeof(ItemType), _itemType);
            _asset.consumptionType = (ConsumptionType)Enum.Parse(typeof(ConsumptionType), _consumptionType);
            _asset.prefebkey = _prefebKey;
            _asset.animationLayer = _animationLayer;
            _asset.dropItemPrefebKey = _dropItemKey;
            _asset.accessoriesItemType = (AccessoriesItemType)Enum.Parse(typeof(AccessoriesItemType), _accessoriesItemType);
            _asset.equipmentType = (EquipmentType)Enum.Parse(typeof(EquipmentType), _equipmentType);
            _asset.isSlot = Convert.ToBoolean(_isSlot);

            allItemDataSO.ItemDataGenerate();

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = _asset;
        }
    }

    private void SetSODropItem(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;

        for (int i = 1; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');

            string _key = column[0];
            string _count = column[1];
            string _price = column[2];
            string _nameKey = column[3];
            string _explationKey = column[4];
            string _spritekey = column[5];
            string _stackble = column[6];
            string _stackMax = column[7];
            string _itemType = column[8];
            string _consumptionType = column[9];
            string _prefebKey = column[10];
            string _animationLayer = column[11];
            string _dropItemKey = column[12];
            string _accessoriesItemType = column[13];
            string _equipmentType = column[14];
            string _isSlot = column[15];

            ItemDataSO _asset = null;
            _asset = allDropItemDataSO.itemDataSOList.Find(x => x.key == _key);
            //이미 있는지
            if (_asset == null)
            {
                _asset = ScriptableObject.CreateInstance<ItemDataSO>();

                AssetDatabase.CreateAsset(_asset, $"Assets/02.ScriptableObject/DropItemSO/{_key}_DROPSO.asset");
                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();

                Selection.activeObject = _asset;

                allDropItemDataSO.itemDataSOList.Add(_asset);
            }
            _asset.key = _key;
            _asset.count = int.Parse(_count);
            _asset.price = int.Parse(_price);
            _asset.nameKey = _nameKey;
            _asset.explanationKey = _explationKey;
            _asset.spriteKey = _spritekey;
            _asset.stackble = Convert.ToBoolean(_stackble);
            _asset.stackMax = int.Parse(_stackMax);
            _asset.itemType = (ItemType)Enum.Parse(typeof(ItemType), _itemType);
            _asset.consumptionType = (ConsumptionType)Enum.Parse(typeof(ConsumptionType), _consumptionType);
            _asset.prefebkey = _prefebKey;
            _asset.animationLayer = _animationLayer;
            _asset.dropItemPrefebKey = _dropItemKey;
            _asset.accessoriesItemType = (AccessoriesItemType)Enum.Parse(typeof(AccessoriesItemType), _accessoriesItemType);
            _asset.equipmentType = (EquipmentType)Enum.Parse(typeof(EquipmentType), _equipmentType);
            _asset.isSlot = Convert.ToBoolean(_isSlot);

            allDropItemDataSO.ItemDataGenerate();

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = _asset;
        }
    }

    private void SetSOUpgradeItem(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;

        for (int i = 1; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');

            string _key = column[0];
            string _count = column[1];
            string[] _needItemKey = column[2].Split(',');
            string[] _needItemCount = column[3].Split(',');

            ItemUpgradeDataSO _asset = null;
            _asset = allItemUpgradeDataSO.itemUpgradeDataList.Find(x => x.key == _key);
            //이미 있는지
            if (_asset == null)
            {
                _asset = ScriptableObject.CreateInstance<ItemUpgradeDataSO>();

                AssetDatabase.CreateAsset(_asset, $"Assets/02.ScriptableObject/ItemUpgradeSO/{_key}_UpgradeSO.asset");
                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();

                Selection.activeObject = _asset;

                allItemUpgradeDataSO.itemUpgradeDataList.Add(_asset);
            }
            _asset.key = _key;
            _asset.count = int.Parse(_count);
            for(int j = 0; j < _needItemKey.Length; ++j)
			{
                string _nkey = _needItemKey[j];
                string _nCount = _needItemCount[j];
                ItemData _itemData = ItemData.CopyItemDataSO(allItemDataSO.itemDataSOList.Find(x => x.key == _nkey));
                _itemData.count = int.Parse(_nCount);
                _asset.needItemDataList.Add(_itemData);
            }

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = _asset;
        }
    }
}

#endif
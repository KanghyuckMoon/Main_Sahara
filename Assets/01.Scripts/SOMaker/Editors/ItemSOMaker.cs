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
    public AllItemUpgradeDataSO allItemUpgradeDataSO;
    public AllDropItemListSO allDropItemListSO;

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

        UnityWebRequest wwwDropItemListSO = UnityWebRequest.Get(URL.DROPITEMLISTSO);
        yield return wwwDropItemListSO.SendWebRequest();
        SetSODropItemList(wwwDropItemListSO.downloadHandler.text);
        
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
            string _iconKey = column[16];
            string _modelKey = column[17];

            ItemDataSO _asset = null;
            _asset = allItemDataSO.itemDataSOList.Find(x => x.key == _key);
            //�̹� �ִ���
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
            _asset.iconKey = _iconKey;
            _asset.modelkey = _modelKey;

            allItemDataSO.ItemDataGenerate();

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            
            UnityEditor.EditorUtility.SetDirty(_asset);

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
            //�̹� �ִ���
            if (_asset == null)
            {
                _asset = ScriptableObject.CreateInstance<ItemUpgradeDataSO>();

                AssetDatabase.CreateAsset(_asset, $"Assets/02.ScriptableObject/ItemUpgradeSO/{_key}_UpgradeSO.asset");
                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();
                UnityEditor.EditorUtility.SetDirty(_asset);

                Selection.activeObject = _asset;

                allItemUpgradeDataSO.itemUpgradeDataList.Add(_asset);
            }
            _asset.needItemDataList.Clear();
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
            UnityEditor.EditorUtility.SetDirty(_asset);

            Selection.activeObject = _asset;
        }
    }

    private void SetSODropItemList(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;

        for (int i = 1; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');

            string _name = column[0];
            string _dropCount = column[1];
            string[] _dropPercentArr = column[2].Split(',');
            string[] _dropItemKeyArr = column[3].Split(',');

            DropItemListSO _asset = null;
            _asset = allDropItemListSO.dropItemListSOList.Find(x => x.listName == _name);
            //�̹� �ִ���
            if (_asset == null)
            {
                _asset = ScriptableObject.CreateInstance<DropItemListSO>();

                AssetDatabase.CreateAsset(_asset, $"Assets/02.ScriptableObject/EnemyDropItemListSO/{_name}_DropListSO.asset");
                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();

                Selection.activeObject = _asset;

                allDropItemListSO.dropItemListSOList.Add(_asset);
            }
            _asset.listName = _name;
            _asset.dropCount = int.Parse(_dropCount);

            float[] _randomPercentArr = new float[_dropPercentArr.Length];
            string[] _randomDropItemKeyArr = new string[_dropItemKeyArr.Length];

            for (int j = 0; j < _randomPercentArr.Length; ++j)
            {
                string _nPercent = _dropPercentArr[j];
                string _nItemKey = _dropItemKeyArr[j];
                _randomPercentArr[j] = float.Parse(_nPercent);
                _randomDropItemKeyArr[j] = _nItemKey;
            }

            _asset.randomPercentArr = _randomPercentArr;
            _asset.dropItemKeyArr = _randomDropItemKeyArr;

            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            UnityEditor.EditorUtility.SetDirty(_asset);

            Selection.activeObject = _asset;
        }
    }
}

#endif
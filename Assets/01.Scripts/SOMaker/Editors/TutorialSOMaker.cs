#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GoogleSpreadSheet;
using UI.Popup;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class TutorialSOMaker : MonoBehaviour
{
    public AllPopupTutorialDataSO allPopupTutorialDataSO;
    
    [ContextMenu("MakeTutorialSO")]
    public void MakeItemSO()
    {
        StartCoroutine(GetText());
    }
    
    private IEnumerator GetText()
    {
        UnityWebRequest wwwItemSO = UnityWebRequest.Get(URL.TUTORIALSO);
        yield return wwwItemSO.SendWebRequest();
        SetSOPopupTutorialList(wwwItemSO.downloadHandler.text);
    }

    private void SetSOPopupTutorialList(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;

        for (int i = 1; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');

            string _key = column[0]; 
            string _title = column[1];
            string _page = column[2];
            string[] _imageId = column[3].Split(',');
            string[] _detail = column[4].Split(','); 

            PopupTutorialDataSO _asset = null;
            _asset = allPopupTutorialDataSO.popupTutorialDataSoList.Find(x => x.key == _key);

            if (_asset == null)
            {
                _asset = ScriptableObject.CreateInstance<PopupTutorialDataSO>();

                AssetDatabase.CreateAsset(_asset, $"Assets/02.ScriptableObject/TutorialSO/{_key}_TutorialSO.asset");
                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();

                Selection.activeObject = _asset;

                allPopupTutorialDataSO.popupTutorialDataSoList.Add(_asset);
            }
            _asset.key = _key;
            _asset.titleAddress = _title;
            _asset.page = int.Parse(_page);
            _asset.detailAddressList = _detail.ToList(); 
            _asset.detailImageAddressList = _imageId.ToList(); 
            //_asset.detailImageAddress = _imageId;
            //_asset.detailAddress = _detail;
            
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            UnityEditor.EditorUtility.SetDirty(_asset);

            Selection.activeObject = _asset;
        }
    }
}
#endif

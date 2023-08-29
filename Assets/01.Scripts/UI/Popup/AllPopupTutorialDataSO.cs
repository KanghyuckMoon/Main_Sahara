using System.Collections;
using System.Collections.Generic;
using UI.Popup;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Maker/AllPopupTutorialDataSO")]
public class AllPopupTutorialDataSO : ScriptableObject
{
    public List<PopupTutorialDataSO> popupTutorialDataSoList = new List<PopupTutorialDataSO>(); 
}

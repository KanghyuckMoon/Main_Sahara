using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;

namespace UI.Canvas
{
    [CreateAssetMenu(menuName = "SO/UI/CanvasScreenDataSO")]
    public class CanvasScreenDataSO : ScriptableObject
    {
        [Header("현재 있는 스크린 데이터SO")]
        public List<ScreenType> canvasScreenList = new List<ScreenType>(); 
    }    
}


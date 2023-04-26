using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  UI.Map
{
    public class MarkerTest : MonoBehaviour
    {
        [SerializeField]
        private AllMarkerDataSO _allMarkerDataSo;

        [ContextMenu("AA")]
        public void TestInit()
        {
            _allMarkerDataSo.markeDataSOList.ForEach((x) =>
            {
                x.markerData.count = 10;
            });
        }
    }    
}


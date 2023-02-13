using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
namespace UI
{
    public class TestCo
    {

    }


    public class TestMap : MonoBehaviour
    {
        private MapView mapView;
        public void Init(MapView _mapView)
        {
            this.mapView = _mapView;
        }

        IEnumerator TestCo(Action a = null,Action b = null)
        {
            a?.Invoke();
            mapView.Test2();
            yield return null; 
            mapView.Test();
            b?.Invoke();
        }

        public void Test(Action a,Action b)
        {
            StartCoroutine(TestCo(a,b));

        }
    }

}

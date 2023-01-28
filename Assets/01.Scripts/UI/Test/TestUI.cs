using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class TestUI : MonoBehaviour
    {
        class T1
        {
            public int n = 0;
            public T1(int a)
            {
                n = a;
            }
        }

        [ContextMenu("Test")]
        public void Test()
        {
            T1 t1 = default(T1);
            Debug.Log(t1.n);

            t1 = new T1(2);
            Debug.Log(t1.n);
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Measurement;

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
            Logging.Log(t1.n);

            t1 = new T1(2);
            Logging.Log(t1.n);
        }

        [ContextMenu("stringFormatTest")]
        public void StringFormatTest()
        {
            string fmt = "O00000000.##";
            int i = 1;
            string str = i.ToString(fmt);
            Logging.Log(str);
            Logging.Log(str.IndexOf('O'));
            Logging.Log(str.IndexOf('0'));
            Logging.Log(string.Format("{0,22:D8} {1,22:X8}", i.ToString(), i.ToString()));

            int v = 1234;
            int a = v.ToString("D").Length + 4; 
        }
    }

}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;
using Utill.ExtraStruct;

namespace Utill.ExtraStruct.Test
{
    public class TestPriorityDelegate : MonoBehaviour
    {
        void Start()
        {
            PriorityDelegate<float> del1 = new PriorityDelegate<float>();
            del1.AddEvent(new Tuple<int, PriorityDelegate<float>.priorityDel>(2, Add));
            del1.AddEvent(new Tuple<int, PriorityDelegate<float>.priorityDel>(1, Multiple));
            float damage1 = 10;
            Debug.Log(del1.ReturnValue(ref damage1));
        }


        void Add(ref float add)
        {
            add += 10;
        }

        void Multiple(ref float mul)
        {
            mul *= 10;
        }


    }

}
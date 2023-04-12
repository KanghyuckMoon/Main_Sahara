using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

namespace Detect
{
    public enum DetectItemType
    {
        None,
        Metal,
        Structure,
    }
    
    public interface IDetectItem : IObserble
    {
        public DetectItemType DetectItemType
        {
            get;
            set;
        }

        public bool IsGetOut
        {
            get;
            set;
        }

        public void GetOut();
    }
}

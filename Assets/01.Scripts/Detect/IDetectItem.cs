using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Detect
{
    public enum DetectItemType
    {
        None,
        Metal,
        Structure,
    }
    
    public interface IDetectItem
    {
        public DetectItemType DetectItemType
        {
            get;
            set;
        }

        public void GetOut();
    }
}

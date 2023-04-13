using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

namespace Detect
{
    [Flags]
    public enum DetectItemType
    {
        None = 0,
        Metal = 1 << 0,
        Creture = 1 << 1,
        Structure = 1 << 2,
        Key = 1 << 3,
        ShovelOnly = 1 << 4,
        FishigOnly = 1 << 5,
        NetOnly = 1 << 6,
        MagicOnly = 1 << 7,
        All = ~0,
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

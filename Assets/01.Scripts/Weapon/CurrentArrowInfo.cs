using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class CurrentArrowInfo
    {

        public CurrentArrowInfo(string _arrowAddress)
        {
            arrowAddress = _arrowAddress;
        }

        public string arrowAddress;
        public Action action;
    }
}
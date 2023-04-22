using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Talk
{
    public class PathHarver : MonoBehaviour
    {
        [SerializeField]
        private CinemachineSmoothPath[] smoothPathArr;

        public CinemachineSmoothPath GetPath(int index)
        {
            if (smoothPathArr.Length > index)
            {
                return smoothPathArr[index];
            }
            #if UNITY_EDITOR
            Debug.LogError("ÀÎµ¦½º¸¦ ¹þ¾î³³´Ï´Ù");
            #endif
            return null;
        }
    }   
}

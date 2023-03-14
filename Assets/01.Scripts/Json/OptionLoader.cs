using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Json
{
    public class OptionLoader : MonoBehaviour
    {
        private void Start()
        {
            SaveManager.Instance.OptionLoad();
        }
    
    }   
}

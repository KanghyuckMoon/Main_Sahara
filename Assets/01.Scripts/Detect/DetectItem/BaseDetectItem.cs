using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Detect
{
    public class BaseDetectItem : MonoBehaviour, IDetectItem
    {
        public DetectItemType DetectItemType
        {
            get
            {
                return detectItemType;
            }

            set
            {
                detectItemType = value;
            }
        }

        [SerializeField]
        protected DetectItemType detectItemType;
        
        public virtual void GetOut()
        {
                
        }
    }   
}

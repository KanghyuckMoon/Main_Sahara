using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CutScene
{
    public class SetPlayerTransform : MonoBehaviour
    {
        [SerializeField] private Transform targetTrm;
    
        public void PlayerTransformTarget()
        {
            PlayerObj.Player.transform.SetParent(targetTrm);
        }
        
        
        public void PlayerTransformNull()
        {
            PlayerObj.Player.transform.SetParent(null);
        }
    }
   
}
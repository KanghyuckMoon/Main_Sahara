using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;
using Effect;

namespace Detect
{   
     public class BoxingDigItem : BaseDigItem
     {
          [SerializeField]
          private string effectAddress;

          [SerializeField] 
          private Vector3 vectorPos;
          
          public override void Dig()
          {
            GetNearObject();
            if(targetItem is not null)
            {
                Debug.Log("GetOut");
                targetItem.GetOut();
                Vector3 addtionPos = transform.right * vectorPos.x + transform.up * vectorPos.y + transform.forward * vectorPos.z; 
                
                EffectManager.Instance.SetEffectDefault(effectAddress,transform.position + addtionPos,Quaternion.identity);
            }
          }
     }
}


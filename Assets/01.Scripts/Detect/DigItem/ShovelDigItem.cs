using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Measurement;

namespace Detect
{
public class ShovelDigItem : BaseDigItem
{
    protected List<IDetectItem> targetItemList = new List<IDetectItem>();
    
    protected override void GetNearObject()
    {
        GameObject obj = null;
        Collider[] targets = Physics.OverlapSphere(detectTrm.position, radius,targetLayerMask);
        foreach (Collider col in targets)
        {
            Vector3 dir = col.transform.position - detectTrm.position;
            
            var component = col.gameObject.GetComponent<IDetectItem>();
            if ((detectItemType & component.DetectItemType) != 0)
            {
                targetItem = component;
                targetItemList.Add(targetItem);   
            }
        }

        if (targets.Length == 0)
        {
            targetItemList.Clear();
        }
    }

    public override void Dig()
    {
        GetNearObject();
        foreach (var _targetObj in targetItemList)
        {
            if(_targetObj is not null)
            {
                Logging.Log("GetOut");
                _targetObj.GetOut();
            }   
        }
        targetItemList.Clear();
    }
}
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using DG.Tweening;
using Effect;

namespace Detect
{
    public class SlideDetectItem : BaseDetectItem
    {
        public virtual void GetOut()
        {
            //if (isGetOut)
            //{
            //    return;
            //}
            //targetModel.gameObject.SetActive(true);
            //isGetOut = true;
            //Vector3 _movePos = upPos;
            //var _effectObj = EffectManager.Instance.SetAndGetEffectDefault( effectAddress, targetEffectTrm.position, Quaternion.identity);
            //targetModel.DOMove(_movePos,  heightUpTime);
            //targetTransform.DOShakePosition(heightUpTime, new Vector3(1,0,1) * shakeStrength).OnComplete(() =>
            //{
            //    _effectObj.Pool();
            //    gameObject.SetActive(false);
            //    isGetOut = true;
            //});
        }
    }   
}

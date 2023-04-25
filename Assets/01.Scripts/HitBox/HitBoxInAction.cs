using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HitBox
{
    public class HitBoxInAction : MonoBehaviour
    {
        public HitBoxAction HitBoxAction
        {
            get
            {
                hitBoxAction ??= new HitBoxAction();
                return hitBoxAction;
            }
        }
        
        private HitBoxAction hitBoxAction;
    }
}
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
                return hitBoxAction;
            }
        }
        
        private HitBoxAction hitBoxAction;
    }
}
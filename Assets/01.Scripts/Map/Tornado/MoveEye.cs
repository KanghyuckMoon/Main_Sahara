using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Video;

namespace Map
{
    public class MoveEye : MonoBehaviour
    {
        [SerializeField] private Transform moveTransform;
        
        [SerializeField]
        private Vector3 point;
        
        [SerializeField]
        private bool isSpeed;

        [SerializeField]
        private float timeOrSpeed;

        public void MovePoint()
        {
            if (isSpeed)
            {
                float _time = Vector3.Distance(moveTransform.position, point) / timeOrSpeed;
                moveTransform.DOMove(point, _time);   
            }
            else
            {
                moveTransform.DOMove(point, timeOrSpeed);
            }
        }
        
    }   
}

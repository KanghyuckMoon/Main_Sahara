using System;
using System.Collections;
using System.Collections.Generic;
using Detect;
using DG.Tweening;
using UnityEngine;
using Utill.Measurement;

namespace Arena
{
    public class MoveStoneItem : MonoBehaviour, IDetectItem
    {
        [Header("움직일 대상"), SerializeField] private Transform moveTarget;
        [SerializeField] private string targetTag = "MoveStoneFinish";
        [SerializeField] private DetectItemType detectItemType = DetectItemType.Slide;
        [SerializeField] private int pushPower = 5;
        [SerializeField] private int id;

        private List<Observer> observerList = new List<Observer>();
        public List<Observer> Observers => observerList;

        public DetectItemType DetectItemType
        {
            get => detectItemType;
            set => detectItemType = value;
        }

        public bool IsGetOut { get; set; }

        private Transform player;
        private bool isComplete = false;
        private BoxRaycaster boxRaycaster;

        public bool IsComplete => isComplete;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player").transform;
            boxRaycaster = new BoxRaycaster(transform);
        }

        private void Update()
        {
            CheckCurPos();
        }

        private void OnDrawGizmos()
        {
            if (boxRaycaster == null) return; 
            Vector3 size =  boxRaycaster.TriggerCollider.size;
            var lossyScale = boxRaycaster.TriggerCollider.transform.lossyScale;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(
                boxRaycaster.TriggerCollider.center + boxRaycaster.TriggerCollider.transform.localPosition+ transform.position, 
                boxRaycaster.TriggerCollider.size * boxRaycaster.TriggerCollider.transform.lossyScale.x);
            boxRaycaster.OnDrawGizmos();
        }

        public void GetOut()
        {
            // 이동 
            Vector3 _dir = (transform.position - player.position).normalized;
            _dir = new Vector3(_dir.x, 0, _dir.z);
            moveTarget.DOMove(transform.position + _dir * pushPower, 0.5f);
        }

        private void CheckCurPos()
        {
            if (boxRaycaster == null) return; 
            bool isTargetCol = false;
            var _cols = boxRaycaster.MyCollisions();
            for (int i = 0; i < _cols.Length; i++)
            {
                if (_cols[i].CompareTag(targetTag))
                {
                    // 도착점 
                    MoveStoneFinish _finish = _cols[i].GetComponent<MoveStoneFinish>();
                    if (_finish != null && _finish.id == id)
                    {
                        isTargetCol = true;
                        isComplete = true;
                        Send();
                    }
                }
            }

            if (isTargetCol == false && isComplete == true)
            {
                isComplete = false;
            }
        }

        public void AddObserver(Observer _observer)
        {
            Observers.Add(_observer);
            _observer.Receive();
        }

        public void RemoveObserver(Observer _observer)
        {
            Observers.Remove(_observer);
        }

        public void Send()
        {
            foreach (Observer observer in Observers)
            {
                observer.Receive();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player_Weapon"))
            {
                Logging.Log("플레이어 웨폰 충돌" + other.name);
            }
        }
    }
}
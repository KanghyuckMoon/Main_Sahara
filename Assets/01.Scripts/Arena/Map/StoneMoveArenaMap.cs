using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utill.Measurement;

namespace Arena
{
    public class StoneMoveArenaMap : ArenaMap
    {
        private List<MoveStoneItem> stoneList = new List<MoveStoneItem>();    

        protected override void Awake()
        {
            base.Awake();
            stoneList = transform.GetComponentsInChildren<MoveStoneItem>().ToList(); 
        }

        protected override void Start()
        {
            base.Start();
            InitStoneList();
        }
        private void InitStoneList()
        {
            foreach (var stone in stoneList)
            {
                stone.AddObserver(this);
            }
        }

        public override void Receive()
        {
            bool isComplete = true; 
            foreach (var stone in stoneList)
            {
                if (stone.IsComplete == false)
                {
                    isComplete = false;
                    return; 
                }
            }
            // ���� Ȯ�� �� ��� �ڸ��� ������ Ŭ���� 
            GetEndTriggerList().First().inactiveTriggerEvent?.Invoke();
            Logging.Log("@@@@@@@@@@@@@@Ŭ����! ");
        }
        //private  void Create
    }
}

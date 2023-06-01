using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;

namespace Arena
{
    public class ArenaStage : MonoBehaviour
    {
        
        [Header("SO �ּ�"), SerializeField] private string arenaStageDataSOAdress; 
        [SerializeField] private ArenaStageDataSO arenaStageDataSO;

        [SerializeField] private List<GameObject> arenaList = new List<GameObject>(); 
        [SerializeField] private Dictionary<int,GameObject> arenaDic = new Dictionary<int, GameObject>();

        private void Awake()
        {
            arenaStageDataSO ??= AddressablesManager.Instance.GetResource<ArenaStageDataSO>(arenaStageDataSOAdress);
        }

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            for (int i = 0; i < arenaStageDataSO.arenaStageList.Count; i++)
            {
                var _arenaPrefab = arenaStageDataSO.arenaStageList[i].arenaPrefab; 
                var _level = arenaStageDataSO.arenaStageList[i].level; 

                GameObject _arenaObj = Instantiate(_arenaPrefab,Vector3.zero ,Quaternion.identity, transform);
                arenaDic.Add(_level, _arenaObj);
                _arenaObj.SetActive(false);
            }

            ActiveCurArena(); 
        }

        public void CompleteArena()
        {
            int _nextLevel = arenaStageDataSO.curLevel + 1;
            if (_nextLevel > arenaStageDataSO.maxLevel)
            {
                InactiveAll();
                return; 
                // �� ��Ȱ��ȭ 
            }

            ActiveCurArena();
            //arenaStageDataSO.arenaStageList[arenaStageDataSO.curLevel - 1]
        }
        
        /// <summary>
        /// ���� ������ Ȱ��ȭ
        /// </summary>
        private void ActiveCurArena()
        {
            InactiveAll();
            arenaDic[arenaStageDataSO.curLevel].SetActive(true);
        }

        private void InactiveAll()
        {
            foreach (var _arena in  arenaDic)
            {
                _arena.Value.SetActive(false);
            }
        }
    }
    
}

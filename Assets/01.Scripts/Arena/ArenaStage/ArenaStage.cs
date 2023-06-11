using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;

namespace Arena
{
    public class ArenaStage : MonoBehaviour
    {
        
        [Header("SO 주소"), SerializeField] private string arenaStageDataSOAdress; 
        [SerializeField] private ArenaStageDataSO arenaStageDataSO;

        [SerializeField] private List<GameObject> arenaList = new List<GameObject>(); 
        [SerializeField] private Dictionary<int,ArenaMap> arenaDic = new Dictionary<int, ArenaMap>();

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

                ArenaMap _arenaObj = Instantiate(_arenaPrefab,Vector3.zero ,Quaternion.identity, transform).GetComponent<ArenaMap>();
                arenaDic.Add(_level, _arenaObj);
                _arenaObj.transform.localPosition = Vector3.zero;
                _arenaObj.GetEndTriggerList().ForEach((x) => x.inactiveTriggerEvent.AddListener(CompleteArena));; ;
                _arenaObj.gameObject.SetActive(false);
            }

            /*var _a = transform.GetComponentsInChildren<ArenaMap>();
            for (int i = 0; i < _a.Length; i++)
            {
                GameObject _arenaObj = _a[i].gameObject;
                arenaDic.Add(i+1, _arenaObj);
                _arenaObj.SetActive(false);
            }*/
            ActiveCurArena(); 
        }

        /// <summary>
        /// 아레나 클리어시 
        /// </summary>
        public void CompleteArena()
        {
            int _nextLevel = ++arenaStageDataSO.curLevel;
            if (_nextLevel > arenaStageDataSO.maxLevel)
            {
                InactiveAll();
                arenaStageDataSO.isClear = true; 
                return; 
                        // 다 비활성화 
            }

            ActiveCurArena();
            //arenaStageDataSO.arenaStageList[arenaStageDataSO.curLevel - 1]
        }
        
        /// <summary>
        /// 현재 투기장 활성화
        /// </summary>
        private void ActiveCurArena()
        {
            InactiveAll();
            arenaDic[arenaStageDataSO.curLevel].Active(true);
        }

        private void InactiveAll()
        {
            foreach (var _arena in  arenaDic)
            {
                _arena.Value.Active(false);
            }
        }
    }
    
}

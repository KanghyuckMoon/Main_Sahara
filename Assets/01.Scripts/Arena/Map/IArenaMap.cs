using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public interface IArenaMap
    {
        public bool IsActive { get;  }
        
        // 투기장 활성화 
        public void StartArena();
        // 투기장 비활성화 
        public void CompleteArena(); 
        
        // 조건 체크 
        public bool CheckCondition();
        // 조건 완료 
    }

}

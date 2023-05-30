using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public interface IArenaMap
    {
        public bool IsActive { get;  }
        
        // ������ Ȱ��ȭ 
        public void StartArena();
        // ������ ��Ȱ��ȭ 
        public void CompleteArena(); 
        
        // ���� üũ 
        public bool CheckCondition();
        // ���� �Ϸ� 
    }

}

using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using Utill.Pattern;

namespace UI.ActiveManager
{
    public class UIActiveManager : MonoSingleton<UIActiveManager>, IUIManager
    {
        private List<IUIManaged> uiManagedList = new List<IUIManaged>(); 
        private List<IUIManaged> uiIgnoredList = new List<IUIManaged>(); 
        // 프로퍼티 
        public List<IUIManaged> UIManagedList { get => uiManagedList; }
        public List<IUIManaged> UIIgnoredList { get => uiIgnoredList; }
        
        /// <summary>
        /// 재시작시 초기화 
        /// </summary>
        public void Init()
        {
            UIManagedList.Clear();
            UIIgnoredList.Clear();
        }
    }
    
}

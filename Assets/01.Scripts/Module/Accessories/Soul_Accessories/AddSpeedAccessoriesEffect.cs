using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using TimeManager;
using DG.Tweening;
using UnityEngine.Rendering;

namespace PassiveItem
{
    public class AddSpeedAccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        private MoveModule moveModule;
        
        public AddSpeedAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
            moveModule = mainModule.GetModuleComponent<MoveModule>(ModuleType.Move);
        }
        
        public void ApplyPassiveEffect()
        {
            
        }
        
        public void UpdateEffect()
        {
            if (IsMoving())
            {
                moveModule.passiveSpeed = Mathf.Min(4, moveModule.passiveSpeed + (Time.deltaTime * 0.2f));
            }
            else
            {
                moveModule.passiveSpeed = 0;
            }
        }

        bool IsMoving()
        {
            var _x = Input.GetAxis("Horizontal");
            var _y = Input.GetAxis("Vertical");

            Vector3 _vec = new Vector3(_x, _y, 0);

            return _vec != Vector3.zero;
        }

        public void ClearPassiveEffect()
        {
            
        }

        public void UpgradeEffect()
        {
            
        }
    }
}
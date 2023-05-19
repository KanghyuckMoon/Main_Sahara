using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using TimeManager;
using DG.Tweening;
using Pool;
using UnityEngine.Rendering;
using Weapon;

namespace PassiveItem
{
    public class CrawlingAccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        private MoveModule moveModule;
        
        public CrawlingAccessoriesEffect(AbMainModule _mainModule)
        {
            mainModule = _mainModule;
            moveModule = mainModule.GetModuleComponent<MoveModule>(ModuleType.Move);
        }
        
        public void ApplyPassiveEffect()
        {
            mainModule.CharacterController.slopeLimit = 120;
        }

        public void UpdateEffect()
        {
            /*Ray _ray = new Ray(mainModule.CharacterController.center, Vector3.forward);
            RaycastHit _raycastHit;
            mainModule.CharacterController.Raycast(_ray, out _raycastHit, 0.5f);

            if (_raycastHit.collider.CompareTag("Ground"))
            {
                moveModule.isCrawling = true;
                return;
            }
            moveModule.isCrawling = false;*/
        }

        public void ClearPassiveEffect()
        {
            //moveModule.isCrawling = false;
            mainModule.CharacterController.slopeLimit = 45;
        }

        public void UpgradeEffect()
        {
            
        }
    }
}
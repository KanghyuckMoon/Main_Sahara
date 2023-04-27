using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using Utill.Pattern;
using Module;

namespace PassiveItem
{
    public class Fire_AccessoriesEffect : IPassive
    {
        private AbMainModule mainModule;
        private GameObject effect;

        public Fire_AccessoriesEffect(AbMainModule _mainModule)
		{
            mainModule = _mainModule;
		}

        public void ApplyPassiveEffect()
        {
            AddFire();
        }
        public void UpdateEffect()
        {
            //throw new System.NotImplementedException();
        }

        public void ClearPassiveEffect()
        {
            RemoveFire();
        }

        public void UpgradeEffect()
        {
            
        }

        public void AddFire()
		{
            //���� ��⿡ �����ؼ� ���̾� ������Ʈ ���� �� ������ ����
            effect = ObjectPoolManager.Instance.GetObject("FireEffect");
            WeaponModule _weaponModule = mainModule.GetModuleComponent<WeaponModule>(ModuleType.Weapon);
            effect.transform.SetParent(_weaponModule.BaseWeapon.transform);
            effect.SetActive(true);
        }

        public void RemoveFire()
        {
            //������ ����Ǿ��ִ� ���̾� ������Ʈ�� ����
            ObjectPoolManager.Instance.RegisterObject("FireEffect", effect);
            effect.SetActive(false);
            effect = null;
        }

    }
}
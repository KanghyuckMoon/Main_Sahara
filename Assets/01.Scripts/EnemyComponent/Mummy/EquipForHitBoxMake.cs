using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Module;
using Pool;
using Random = UnityEngine.Random;
using Effect;

public class EquipForHitBoxMake : MonoBehaviour
{
	[SerializeField] private string weapon;

	[SerializeField]
	private AbMainModule mainModule;

	private void OnEnable()
	{
		Invoke("Equip", 0.1f);
	}

	[ContextMenu("Equip")]
	private void Equip()
	{
		var _weaponModule = mainModule.GetModuleComponent<WeaponModule>(ModuleType.Weapon);
		_weaponModule.ChangeWeapon(weapon, null);
	}

}

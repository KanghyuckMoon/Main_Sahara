using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using Utill.SeralizableDictionary;
using Weapon;
#if UNITY_EDITOR
using UnityEditor;

	[CustomPropertyDrawer(typeof(StringListProjectilePositionData))]
	[CustomPropertyDrawer(typeof(StringListWeaponPositionData))]
public class PositionPropertyDrawer : SerializableDictionaryPropertyDrawer
{
}
#endif